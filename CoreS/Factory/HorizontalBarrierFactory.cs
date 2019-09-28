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

        private Dictionary<int, List< ElementModel>> _elem;

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

            if (Height!=null && Height.Last()-Number*_cabinet.SizeElement()<0)
            {
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
                Number = elements.Count + element;

                return Recalculate(Permutation.Get(_cabinet.VerticalBarrier.Count));
            }
            catch (ArgumentException e)
            {
                Logger.Error(e, "Blad dzielenia przez zero - minusowa ilosc polek"); ;
                throw new ArgumentException();
            }
            
        }

        public List<ElementModel> Delete(int delete)
        {
            try
            {
                Number = elements.Count - delete;
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

            try
            {
                tempHeight = _cabinet.SizeElement();
                

                tempDepth = _cabinet.Depth() - back;

                TempHeight = TempEy();

                for (var i = 0; i <= _cabinet.VerticalBarrier.Count; i++)
                {
                    if (barrier == null || barrier.Count == 0)
                    {
                        tempWidth = TempWidth(i);
                        tempEx = i == 0
                            ? _cabinet.CabinetElements.First((x => x.EName == EnumCabinetElement.Leftside)).EWidth
                            : _cabinet.VerticalBarrier[i - 1].Ex + _cabinet.VerticalBarrier[i - 1].EWidth;

                        AddElement();
                        _elem.Add(i, elements);
                    }
                    else
                    {
                        if (barrier.Contains(i))
                        {
                            tempWidth = TempWidth(i);
                            tempEx = i == 0
                                ? _cabinet.CabinetElements.First((x => x.EName == EnumCabinetElement.Leftside)).EWidth
                                : _cabinet.VerticalBarrier[i - 1].Ex + _cabinet.VerticalBarrier[i - 1].EWidth;

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

        private int TempWidth(int column)
        {
            if (_cabinet.VerticalBarrier.Count > 0)
            {
                if (column == 0)
                {
                    tempWidth = _cabinet.VerticalBarrier[column].Ex - _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Leftside).EWidth;
                }
                else if (column == _cabinet.VerticalBarrier.Count)
                {
                    tempWidth = _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Rightside).Ex - _cabinet.VerticalBarrier[column - 1].Ex - _cabinet.VerticalBarrier[column - 1].EWidth;
                }
                else
                {
                    tempWidth = _cabinet.VerticalBarrier[column].Ex - _cabinet.VerticalBarrier[column - 1].Ex - _cabinet.VerticalBarrier[column - 1].EWidth;
                }
            }
            else
            {
                tempWidth = _cabinet.Width() - 2 * _cabinet.SizeElement();
            }

            return tempWidth;
        }

        private void AddElement()
        {
            for (var i = 0; i < Number; i++)
            {
                var element = new ElementModel
                {
                    EHeight = tempWidth,
                    EWidth = tempHeight,
                    EDepth = tempDepth,
                    Ex = tempEx,
                    Ey = TempHeight[i],
                    EName = EnumCabinetElement.HorizontalBarrier,
                    Description = "Poziom",
                    Horizontal=true
                };

                elements.Add(element);
            }
        }

        private List<int> TempEy()
        {
            var list = new List<int>();

            if (Height != null && Height.Count > 0)
            {
                if (Number<=Height.Count)
                {
                    for (int i = 0; i < Number; i++)
                    {
                        list.Add(_cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Bottom).EWidth + Height[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < Height.Count; i++)
                    {
                        list.Add(_cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Bottom).EWidth + Height[i]);
                    }

                    var tempheight= _cabinet.Height() - list.Last() - _cabinet.SizeElement() - _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Top).EWidth;

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
                          _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Bottom).EWidth -
                          _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Top).EWidth
                          - Number * _cabinet.SizeElement()) / (Number + 1);

                for (var i = 0; i < Number; i++)
                {
                    list.Add(_cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Bottom).EWidth + tempEy * (i + 1) + _cabinet.SizeElement() * i);
                }
            }

            return list;
        }
    }
}