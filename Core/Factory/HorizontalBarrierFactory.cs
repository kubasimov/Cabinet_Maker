using System.Collections.Generic;
using System.Linq;
using Core.Helpers;
using Core.Interface;
using Core.Model;

namespace Core.Factory
{
    public class HorizontalBarrierFactory:BarrierFactory, IElementRepository
    {
        private Dictionary<int, List< ElementModel>> _elem;

        public HorizontalBarrierFactory(Cabinet cabinet)
        {
            _cabinet = cabinet;
            elements=new List<ElementModel>();
            _elem = new Dictionary<int, List<ElementModel>>();
        }
        
        public override List<ElementModel> NewBarrier(BarrierParameter barrierParameter)
        {
            Number = barrierParameter.Number;

            back = barrierParameter.Back;

            return Recalculate(barrierParameter.GetBarrier());
        }

        public List<ElementModel> ReCount()
        {
            return Recalculate(Permutation.Get(_cabinet.VerticalBarrier.Count));
        }

        public List<ElementModel> Add(int element)
        {
            Number = elements.Count + element;

            return Recalculate(Permutation.Get(_cabinet.VerticalBarrier.Count));
        }

        public List<ElementModel> Delete()
        {
            Number = elements.Count - 1;
            if (Number < 0)
                Number = 0;
            return Recalculate(Permutation.Get(_cabinet.VerticalBarrier.Count));

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
            throw new System.NotImplementedException();
        }



        private List<ElementModel> Recalculate(List<int> barrier)
        {
            elements = new List<ElementModel>();
            
            _elem = new Dictionary<int, List<ElementModel>>();


            tempHeight = _cabinet.SizeElement; ;

            tempDepth = _cabinet.Depth - back;

            tempEy = (_cabinet.Height - _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Bottom).EHeight - _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Top).EHeight
                      - Number * _cabinet.SizeElement) / (Number + 1);



            for (var i = 0; i <= _cabinet.VerticalBarrier.Count; i++)
            {
                if (barrier == null || barrier.Count == 0)
                {
                    tempWidth = TempWidth(i);
                    tempEx = i == 0 ? _cabinet.CabinetElements.First((x => x.EName == EnumCabinetElement.Leftside)).EWidth : _cabinet.VerticalBarrier[i - 1].Ex + _cabinet.VerticalBarrier[i - 1].EWidth;

                    AddElement();
                    _elem.Add(i, elements);
                }
                else
                {
                    if (barrier.Contains(i))
                    {
                        tempWidth = TempWidth(i);
                        tempEx = i == 0 ? _cabinet.CabinetElements.First((x => x.EName == EnumCabinetElement.Leftside)).EWidth : _cabinet.VerticalBarrier[i - 1].Ex + _cabinet.VerticalBarrier[i - 1].EWidth;

                        AddElement();
                        _elem.Add(i, elements);
                    }
                }





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
                tempWidth = _cabinet.Width - 2 * _cabinet.SizeElement;
            }

            return tempWidth;
        }

        private void AddElement()
        {
            for (var i = 0; i < Number; i++)
            {
                var element = new ElementModel
                {
                    EHeight = tempHeight,
                    EWidth = tempWidth,
                    EDepth = tempDepth,
                    Ex = tempEx,
                    Ey = _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Bottom).EHeight + tempEy * (i + 1) + _cabinet.SizeElement * i,
                    EName = EnumCabinetElement.HorizontalBarrier,
                    Description = "Poziom"
                };

                elements.Add(element);


            }
        }
    }
}