using System.Collections.Generic;
using System.Linq;
using Core.Model;

namespace Core.Factory
{
    public class VerticalBarrierFactory:BarrierFactory
    {
        public VerticalBarrierFactory(Cabinet cabinet)
        {
            _cabinet = cabinet;
            elements = new List<ElementModel>();
        }

        public override List<ElementModel> NewBarrier(BarrierParameter barrierParameter)
        {
            Number = barrierParameter.Number;
            
            back = barrierParameter.Back;

            return Recalculate();
        }

        public override List<ElementModel> Add(int element)
        {
            Number = elements.Count() + element;
            
            return Recalculate();

        }

        public override List<ElementModel> Delete(ElementModel delete)
        {
            elements.Remove(delete);
            return elements;
        }

        public override List<ElementModel> GetAll()
        {
            return elements;
        }

        public override ElementModel Get(int element)
        {
            return elements[element];
        }

        private List<ElementModel> Recalculate()
        {
            elements = new List<ElementModel>();

            tempHeight = _cabinet.Height - _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Bottom).EHeight - _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Top).EHeight;

            tempWidth = _cabinet.SizeElement;

            tempDepth = _cabinet.Depth - back;

            tempEy = _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Bottom).EHeight;

            tempEx = (_cabinet.Width - _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Leftside).EWidth -
                      _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Rightside).EWidth - Number * _cabinet.SizeElement) / (Number + 1);

            for (var i = 0; i < Number; i++)
            {
                var element = new ElementModel
                {
                    EHeight = tempHeight,
                    EWidth = tempWidth,
                    EDepth = tempDepth,
                    Ex = _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Leftside).EWidth + tempEx * (i + 1) + _cabinet.SizeElement * i,
                    Ey = tempEy,
                    EName = EnumCabinetElement.VerticalBarrier,
                    Description="Poziom"
                };
                elements.Add(element);
            }

            return elements;
        }
    }
}