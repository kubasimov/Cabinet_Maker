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
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private Dictionary<int, List< ElementModelDTO>> _elem;  //tablica poziomow - Dictionary<poziom,elementy_na_danym_poziomie>
       
        public HorizontalBarrierFactory(Cabinet cabinet)
        {
            _cabinet = cabinet;
            elements=new List<ElementModelDTO>();
            _elem = new Dictionary<int, List<ElementModelDTO>>();
                       
        }
        
        public List<ElementModelDTO> NewBarrier(BarrierParameter barrierParameter)
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

        public List<ElementModelDTO> ReCount()
        {
            return Recalculate(Permutation.Get(_cabinet.VerticalBarrier.Count));
        }

        public List<ElementModelDTO> Add(int element)
        {
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

        public List<ElementModelDTO> Delete(int delete)
        {
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
            throw new System.NotImplementedException();
        }

        public List<ElementModelDTO> DeleteAll()
        {
            Number = 0;
            elements = new List<ElementModelDTO>();
            return elements;
        }

        public List<ElementModelDTO> GetAll()
        {
            return elements;
        }
               
        private List<ElementModelDTO> Recalculate(List<int> barrier)
        {
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
            for (var i = 0; i < Number; i++)
            {
                var element = new ElementModelDTO("poziom", tempWidth, tempHeight, tempDepth, tempEx, TempHeight[i], 0, EnumCabinetElement.HorizontalBarrier, true);
                
                elements.Add(element);
            }
        }

        //Wyliczanie wysokosci poziomów
        private List<int> List_of_Horizontal_Height()
        {
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