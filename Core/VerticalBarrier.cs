using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class VerticalBarrier
    {
        private Cabinet _cabinet;

        public VerticalBarrier(Cabinet cabinet)
        {
            _cabinet = cabinet;
        }

        public void AddBarrier(int number, int barrier = 0, int back = 0, List<int> height = default(List<int>))
        {
            int tempHeight = _cabinet.Height - _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Bottom).EHeight - _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Top).EHeight;

            int tempWidth = _cabinet.SizeElement;

            int tempDepth = _cabinet.Depth - back;

            int tempEy = _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Bottom).EHeight;

            int tempEx = (_cabinet.Width - _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Leftside).EWidth -
                          _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Rightside).EWidth - number * _cabinet.SizeElement) / (number + 1);
            for (int i = 0; i < number; i++)
            {
                var element = new Element
                {
                    EHeight = tempHeight,
                    EWidth = tempWidth,
                    EDepth = tempDepth,
                    Ex = _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Leftside).EWidth + tempEx * (i + 1) + _cabinet.SizeElement * i,
                    Ey = tempEy,
                    EName = EnumCabinetElement.VerticalBarrier
                };
                _cabinet.VerticalBarrier.Add(element);
            }
        }
    }
}