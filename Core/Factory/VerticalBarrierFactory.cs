using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Core.Interface;
using Core.Model;

namespace Core.Factory
{
    public class VerticalBarrierFactory:BarrierFactory, IElementRepository
    {
        public VerticalBarrierFactory(Cabinet cabinet)
        {
            _cabinet = cabinet;
            elements = new List<ElementModel>();
        }

        public List<ElementModel> NewBarrier(BarrierParameter barrierParameter)
        {
            Number = barrierParameter.Number;
            
            back = barrierParameter.Back;

            return Recalculate();
        }

        public List<ElementModel> ReCount()
        {
            return Recalculate();
        }

        public List<ElementModel> Add(int element)
        {
            Number = elements.Count + element;

            return Recalculate();
        }

        public List<ElementModel> Delete(int delete)
        {
            Number = elements.Count - delete;
            if (Number < 0)
                Number = 0;
            return Recalculate();
        }

        public List<ElementModel> Delete(ElementModel delete)
        {
            elements.Remove(delete);
            return elements;
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
        
        private List<ElementModel> Recalculate()
        {
            elements = new List<ElementModel>();

            tempHeight = _cabinet.Height() - _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Bottom).EWidth - _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Top).EWidth;

            tempWidth = _cabinet.SizeElement();

            tempDepth = _cabinet.Depth() - back;

            tempEy = _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Bottom).EWidth;

            tempEx = (_cabinet.Width() - _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Leftside).EWidth -
                      _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Rightside).EWidth - Number * _cabinet.SizeElement()) / (Number + 1);

            for (var i = 0; i < Number; i++)
            {
                var element = new ElementModel
                {
                    EHeight = tempHeight,
                    EWidth = tempWidth,
                    EDepth = tempDepth,
                    Ex = _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Leftside).EWidth + tempEx * (i + 1) + _cabinet.SizeElement() * i,
                    Ey = tempEy,
                    EName = EnumCabinetElement.VerticalBarrier,
                    Description="Pion",
                    Horizontal=false
                };
                elements.Add(element);
            }

            return elements;
        }


        
    }
}