using CoreS.Enum;
using CoreS.Helpers;
using CoreS.Interface;
using CoreS.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreS.Factory
{
    public class HorizontalBarrierFactory:BarrierFactory, IElementRepository
    {
        private Dictionary<int, List< ElementModelDTO>> _elem;  //tablica poziomow - Dictionary<poziom,elementy_na_danym_poziomie>

        public HorizontalBarrierFactory(Cabinet cabinet)
        {
            Logger.Info("HorizontalBarrierFactory Constructor");
            _cabinet = cabinet;
            elements=new List<ElementModelDTO>();
            _elem = new Dictionary<int, List<ElementModelDTO>>();
                       
        }

        public List<ElementModelDTO> NewBarrier(BarrierParameter barrierParameter)
        {
            Logger.Info("NewBarrier(BarrierParameter barrierParameter) in HorizontalBarrierFactory");
            Logger.Debug("barrierParameter: {0} ", barrierParameter);
            Number = barrierParameter.Number;

            back = barrierParameter.Back;
            Height = barrierParameter.Height;

            try
            {
                if (Height != null && Height.Count > 0)
                {
                    if (Height.Last() - Number * _cabinet.SizeElement() < 0)
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error NewBarrier");
            }
            
            return Recalculate(barrierParameter.GetBarrier());
        }

        public List<ElementModelDTO> ReCount()
        {
            Logger.Info("ReCount() in HorizontalBarrierFactory");
            return Recalculate(Permutation.Get(_cabinet.VerticalBarrier.Count));
        }

        public List<ElementModelDTO> Add(int element)
        {
            Logger.Info("Add(int element) in HorizontalBarrierFactory");
            Logger.Debug("element: {0} ", element); 
            try
            {
                if (element < 0)
                    throw new ArgumentException();
                Number += element;
                
                return Recalculate(Permutation.Get(_cabinet.VerticalBarrier.Count));
            }
            catch (ArgumentException e)
            {
                Logger.Error(e, "Minusowa ilosc polek"); ;
                throw new ArgumentException();
            }
            
        }

        public List<ElementModelDTO> AddEvery(int size)
        {
            Logger.Info("AddEvery(int size) in HorizontalBarrierFactory");
            Logger.Debug("size: {0} ", size);
            try
            {
                if (size < 0)
                    throw new ArgumentException();

                var bok = _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Leftside).Height;
                var gora= _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Top).Width;
                var dol = _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Bottom).Width;

                var wyswew = bok - gora - dol;

                var IloscPrzestrzeni = wyswew / (size + _cabinet.SizeElement());

                Height = new List<int>();

                for (int i = 1; i <= IloscPrzestrzeni; i++)
                {

                    var t = _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Bottom).Width;


                    Height.Add( (t + size)*i);
                }
                Number = IloscPrzestrzeni;
                                                          
                var tt = Recalculate(Permutation.Get(_cabinet.VerticalBarrier.Count));
                return tt;
            }
            catch (ArgumentException e)
            {
                Logger.Error(e, "Minusowa ilosc polek"); ;
                throw new ArgumentException();
            }
        }

        public List<ElementModelDTO> Add(List<ElementModelDTO> list)
        {
            Logger.Info("Add(List<ElementModelDTO> list) in HorizontalBarrierFactory");
            Logger.Debug("list: {0} ", list);
            elements = new List<ElementModelDTO>();
            foreach (var item in list)
            {
                elements.Add(item);
            }
            Number = elements.Count();
            return elements;
        }

        public List<ElementModelDTO> Delete(int delete)
        {
            Logger.Info("Delete(int delete) in HorizontalBarrierFactory");
            Logger.Debug("delete: {0} ", delete);
            try
            {
                Number -= delete;
                if (Number < 0)
                    Number = 0;
                return Recalculate(Permutation.Get(_cabinet.VerticalBarrier.Count));
            }
            catch (ArgumentNullException e)
            {
                Logger.Error(e, "Delete HorizontalBarrierFactory");
                throw e;
            }
           

        }

        public List<ElementModelDTO> Delete(ElementModelDTO element)
        {
            Logger.Info("Delete(ElementModelDTO element) in HorizontalBarrierFactory");
            Logger.Debug("element: {0} ", element);
            var findElement = elements.Find(x => x.GetGuid() == element.GetGuid());
            if (findElement!=null)
            {
                elements.Remove(findElement);
                Number -= 1;
            }

            return elements;
        }

        public List<ElementModelDTO> Remove()
        {
            Logger.Info("Remove() in HorizontalBarrierFactory");
            Number = 0;
            elements = new List<ElementModelDTO>();
            return elements;
        }

        public List<ElementModelDTO> GetAll()
        {
            Logger.Info("GetAll() in HorizontalBarrierFactory");
            return elements;
        }

        public List<ElementModelDTO> Update(EnumElementParameter parameter, string text, int result, ElementModelDTO elementModelDTO)
        {
            Logger.Info("Update(EnumElementParameter parameter, string text, int result, ElementModelDTO elementModelDTO) in HorizontalBarrierFactory");
            Logger.Debug("Parameter: {0}, Text:, {1}, Result: {2}, ElemenModelDTO: {3}", parameter, text, result, elementModelDTO);
            if (!elements.Exists(x => x.GetGuid() == elementModelDTO.GetGuid())) return elements;

            var item = elements.Find(x => x.GetGuid() == elementModelDTO.GetGuid());

            switch (parameter)
            {
                case EnumElementParameter.Width:
                    item.SetWidth(result);
                    break;
                case EnumElementParameter.Height:
                    item.SetHeight(result);
                    break;
                case EnumElementParameter.Depth:
                    item.SetDepth(result);
                    break;
                case EnumElementParameter.Description:
                    item.SetDescription(text);
                    break;
                case EnumElementParameter.X:
                    item.SetX(result);
                    break;
                case EnumElementParameter.Y:
                    item.SetY(result);
                    break;
                case EnumElementParameter.Z:
                    item.SetZ(result);
                    break;
                default:
                    break;
            }

            return elements;
        }

        private List<ElementModelDTO> Recalculate(List<int> barrier)
        {
            Logger.Info("Recalculate(List<int> barrier) in HorizontalBarrierFactory");
            Logger.Debug("barrier: {0} ", barrier);
            elements = new List<ElementModelDTO>();
            
            _elem = new Dictionary<int, List<ElementModelDTO>>();
            //_elem1 = new List<List<ElementModel>>();

            try
            {
                //wysokosc eleemntu
                tempHeight = _cabinet.SizeElement();
                
                //glebokosc elementu
                tempDepth = _cabinet.Depth() - back;

                //wysokosci elementow
                TempHeight = List_of_Horizontal_Height();

                //wyliczenie po kolumnach
                for (var i = 0; i <= _cabinet.VerticalBarrier.Count; i++)
                {
                    if (barrier == null || barrier.Count == 0)
                    {
                        tempWidth = TempWidth(i);
                        tempEx = i == 0
                            ? _cabinet.CabinetElements.First((x => x.GetEnumName() == EnumCabinetElement.Leftside)).Width
                            : _cabinet.VerticalBarrier[i - 1].X + _cabinet.VerticalBarrier[i - 1].Width;

                        AddElement();
                        _elem.Add(i, elements);
                    }
                    else
                    {
                        if (barrier.Contains(i))
                        {
                            tempWidth = TempWidth(i);
                            tempEx = i == 0
                                ? _cabinet.CabinetElements.First((x => x.GetEnumName() == EnumCabinetElement.Leftside)).Width
                                : _cabinet.VerticalBarrier[i - 1].X + _cabinet.VerticalBarrier[i - 1].Width;

                            AddElement();
                            _elem.Add(i, elements);
                        }
                    }

                }
            }
            catch (DivideByZeroException e)
            {
                Logger.Error(e, "Blad dzielenia przez zero - minusowa ilosc polek");
                throw new DivideByZeroException();
            }
            catch (Exception e)
            {
                Logger.Error(e, "Blad - recalculate HorizontalBarrierFactory");
                throw;
            }

            return elements;
        }

        //Wyliczanie szerokosci poziomu dla danej kolumny
        private int TempWidth(int column)
        {
            Logger.Info("TempWidth(int column) in HorizontalBarrierFactory");
            Logger.Debug("column: {0} ", column);
            if (_cabinet.VerticalBarrier.Count > 0)
            {
                //skrajna lewa kolumna
                if (column == 0)
                {
                    tempWidth = _cabinet.VerticalBarrier[column].X - _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Leftside).Width;
                }
                //skrajna prawa kolumna
                else if (column == _cabinet.VerticalBarrier.Count)
                {
                    tempWidth = _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Rightside).X - _cabinet.VerticalBarrier[column - 1].X - _cabinet.VerticalBarrier[column - 1].Width;
                }
                //wewnetrzne kolumny
                else
                {
                    tempWidth = _cabinet.VerticalBarrier[column].X - _cabinet.VerticalBarrier[column - 1].X - _cabinet.VerticalBarrier[column - 1].Width;
                }
            }
            //brak kolumn
            else
            {
                tempWidth = _cabinet.CabinetElements.Find(x=>x.GetEnumName()==EnumCabinetElement.Top).Height;
            }

            return tempWidth;
        }

        //Dodawanie elementów w danej kolumnie
        private void AddElement()
        {
            Logger.Info("AddElement() in HorizontalBarrierFactory"); 
            for (var i = 0; i < Number; i++)
            {
                var element = new ElementModelDTO("Poziom", tempWidth, tempHeight, tempDepth, tempEx, TempHeight[i], 0, EnumCabinetElement.HorizontalBarrier, true);
                
                elements.Add(element);
            }
        }

        //Wyliczanie wysokosci poziomów
        private List<int> List_of_Horizontal_Height()
        {
            Logger.Info("List_of_Horizontal_Height() in HorizontalBarrierFactory");
            var list = new List<int>();

            if (Height != null && Height.Count > 0) // zadane wysokosci polek
            {
                if (Number<=Height.Count)
                {
                    for (int i = 0; i < Number; i++)
                    {
                        list.Add(_cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Bottom).Width + Height[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < Height.Count; i++)
                    {
                        list.Add(_cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Bottom).Width + Height[i]);
                    }

                    var tempheight= _cabinet.Height() - list.Last() - _cabinet.SizeElement() - _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Top).Width;

                    var z = (tempheight - (Number - Height.Count) * _cabinet.SizeElement()) / ((Number - Height.Count) + 1);


                    var last = list.Last();

                    for (int i = 0; i < Number-Height.Count; i++)
                    {
                        list.Add(last + z*(i+1)+ _cabinet.SizeElement() * (i+1));
                    }

                }

            }
            else //podzial na równe czesci
            {
                tempEy = (_cabinet.Height() -
                          _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Bottom).Width -
                          _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Top).Width
                          - Number * _cabinet.SizeElement()) / (Number + 1);

                for (var i = 0; i < Number; i++)
                {
                    list.Add(_cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Bottom).Width + tempEy * (i + 1) + _cabinet.SizeElement() * i);
                }
            }

            return list;
        }

    }
}