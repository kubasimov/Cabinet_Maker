using CoreS.Enum;
using CoreS.Interface;
using CoreS.Model;
using System.Collections.Generic;
using System.Linq;

namespace CoreS.Factory
{
    public class VerticalBarrierFactory:BarrierFactory, IElementRepository
    {
        public VerticalBarrierFactory(Cabinet cabinet)
        {
            _cabinet = cabinet;
            elements = new List<ElementModelDTO>();
        }

        public List<ElementModelDTO> NewBarrier(BarrierParameter barrierParameter)
        {
            Number = barrierParameter.Number;
            
            back = barrierParameter.Back;

            return Recalculate();
        }

        public List<ElementModelDTO> ReCount()
        {
            return Recalculate();
        }

        public List<ElementModelDTO> Add(int element)
        {
            Number = elements.Count + element;

            return Recalculate();
        }

        public List<ElementModelDTO> Delete(int delete)
        {
            Number = elements.Count - delete;
            if (Number < 0)
                Number = 0;
            return Recalculate();
        }

        public List<ElementModelDTO> Delete(ElementModelDTO delete)
        {
            elements.Remove(delete);
            return elements;
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
        
        private List<ElementModelDTO> Recalculate()
        {
            elements = new List<ElementModelDTO>();

            tempHeight = _cabinet.Height() - _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Bottom).Width - _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Top).Width;

            tempWidth = _cabinet.SizeElement();

            tempDepth = _cabinet.Depth() - back;

            tempEy = _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Bottom).Width;

            tempEx = (_cabinet.Width() - _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Leftside).Width -
                      _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Rightside).Width - Number * _cabinet.SizeElement()) / (Number + 1);

            for (var i = 0; i < Number; i++)
            {
                var element = new ElementModelDTO(
                    description: "Pion",
                    height: tempHeight,
                    width: tempWidth,
                    depth: tempDepth,
                    x: _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Leftside).Width + tempEx * (i + 1) + _cabinet.SizeElement() * i,
                    y: tempEy,
                    z: 0,
                    enumCabinet: EnumCabinetElement.VerticalBarrier,
                    horizontal: false);
                
                elements.Add(element);
            }

            return elements;
        }


        
    }
}