using System;
using System.Collections.Generic;
using System.Linq;
using Core.Model;
using CoreS.Enum;
using CoreS.Helpers;
using CoreS.Interface;
using CoreS.Model;
using NLog;

namespace CoreS.Factory
{
    public class HorizontalBarrierFactory:BarrierFactory, IElementRepository
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private Dictionary<int, List< ElementModel>> _elem;  //tablica poziomow - Dictionary<poziom,elementy_na_danym_poziomie>
       
        public HorizontalBarrierFactory(Cabinet cabinet)
        {
            _cabinet = cabinet;
            elements=new List<ElementModel>();
            _elem = new Dictionary<int, List<ElementModel>>();
                       
        }
        
        public List<ElementModel> NewBarrier(BarrierParameter barrierParameter)
        {
            Number = barrierParameter.Number;

            back = barrierParameter.Back;

            Height = barrierParameter.Height;

            if (Height!=null && Height.Count>0)
            {
                if(Height.Last() - Number * _cabinet.SizeElement() < 0)
                    throw new ArgumentOutOfRangeException();
            }
            
            return Recalculate(barrierParameter.GetBarrier());
        }

        public List<ElementModel> ReCount()
        {
            return Recalculate(Permutation.Get(_cabinet.VerticalBarrier.Count));
        }

        public List<ElementModel> Add(int element)
        {
            try
            {
                if (element < 0)
                    throw new ArgumentException();
                Number = Number + element;
                
                return Recalculate(Permutation.Get(_cabinet.VerticalBarrier.Count));
            }
            catch (ArgumentException e)
            {
                Logger.Error(e, "Minusowa ilosc polek"); ;
                throw new ArgumentException();
            }
            
        }

        public List<ElementModel> Delete(int delete)
        {
            try
            {
                Number = Number - delete;
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

        public List<ElementModel> Delete(ElementModel element)
        {
            throw new System.NotImplementedException();
        }

        public List<ElementModel> DeleteAll()
        {
            Number = 0;
            elements = new List<ElementModel>();
            return elements;
        }

        public List<ElementModel> GetAll()
        {
            return elements;
        }



        private List<ElementModel> Recalculate(List<int> barrier)
        {
            elements = new List<ElementModel>();
            
            _elem = new Dictionary<int, List<ElementModel>>();
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
                            ? _cabinet.CabinetElements.First((x => x.GetEnumName() == EnumCabinetElement.Leftside)).GetWidth()
                            : _cabinet.VerticalBarrier[i - 1].GetX() + _cabinet.VerticalBarrier[i - 1].GetWidth();

                        AddElement();
                        _elem.Add(i, elements);
                    }
                    else
                    {
                        if (barrier.Contains(i))
                        {
                            tempWidth = TempWidth(i);
                            tempEx = i == 0
                                ? _cabinet.CabinetElements.First((x => x.GetEnumName() == EnumCabinetElement.Leftside)).GetWidth()
                                : _cabinet.VerticalBarrier[i - 1].GetX() + _cabinet.VerticalBarrier[i - 1].GetWidth();

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

        //Wylicanie szerokosci poziomu dla danej kolumny
        private int TempWidth(int column)
        {
            if (_cabinet.VerticalBarrier.Count > 0)
            {
                //skrajna lewa kolumna
                if (column == 0)
                {
                    tempWidth = _cabinet.VerticalBarrier[column].GetX() - _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Leftside).GetWidth();
                }
                //skrajna prawa kolumna
                else if (column == _cabinet.VerticalBarrier.Count)
                {
                    tempWidth = _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Rightside).GetX() - _cabinet.VerticalBarrier[column - 1].GetX() - _cabinet.VerticalBarrier[column - 1].GetWidth();
                }
                //wewnetrzne kolumny
                else
                {
                    tempWidth = _cabinet.VerticalBarrier[column].GetX() - _cabinet.VerticalBarrier[column - 1].GetX() - _cabinet.VerticalBarrier[column - 1].GetWidth();
                }
            }
            //brak kolumn
            else
            {
                tempWidth = _cabinet.Width() - 2 * _cabinet.SizeElement();
            }

            return tempWidth;
        }

        //Dodawanie elementów w danej kolumnie
        private void AddElement()
        {
            for (var i = 0; i < Number; i++)
            {
                var element = new ElementModel("poziom", tempWidth, tempHeight, tempDepth, tempEx, TempHeight[i], 0, EnumCabinetElement.HorizontalBarrier, true);
                //{
                //    EHeight = tempWidth,
                //    GetWidth() = tempHeight,
                //    EDepth = tempDepth,
                //    Ex = tempEx,
                //    Ey = TempHeight[i],
                //    GetEnumName() = EnumCabinetElement.HorizontalBarrier,
                //    Description = "Poziom",
                //    Horizontal=true
                //};

                elements.Add(element);
            }
        }

        //Wyliczanie wysokosci poziomów
        private List<int> List_of_Horizontal_Height()
        {
            var list = new List<int>();

            if (Height != null && Height.Count > 0)
            {
                if (Number<=Height.Count)
                {
                    for (int i = 0; i < Number; i++)
                    {
                        list.Add(_cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Bottom).GetWidth() + Height[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < Height.Count; i++)
                    {
                        list.Add(_cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Bottom).GetWidth() + Height[i]);
                    }

                    var tempheight= _cabinet.Height() - list.Last() - _cabinet.SizeElement() - _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Top).GetWidth();

                    var z = (tempheight - (Number - Height.Count) * _cabinet.SizeElement()) / ((Number - Height.Count) + 1);


                    var last = list.Last();

                    for (int i = 0; i < Number-Height.Count; i++)
                    {
                        list.Add(last + z*(i+1)+ _cabinet.SizeElement() * (i+1));
                    }

                }

            }
            else
            {
                tempEy = (_cabinet.Height() -
                          _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Bottom).GetWidth() -
                          _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Top).GetWidth()
                          - Number * _cabinet.SizeElement()) / (Number + 1);

                for (var i = 0; i < Number; i++)
                {
                    list.Add(_cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Bottom).GetWidth() + tempEy * (i + 1) + _cabinet.SizeElement() * i);
                }
            }

            return list;
        }
    }
}