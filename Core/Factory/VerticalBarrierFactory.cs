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

        public List<ElementModel> AddBarrier(BarrierParameter barrierParameter)
        {
            //var number = elements.Count() >= barrierParameter.Number ? barrierParameter.Number + elements.Count : barrierParameter.Number;
            number = barrierParameter.Number;

            back = barrierParameter.Back;

            var height = barrierParameter.Height;

            int barrier = 0;//barrierParameter.Barrier.FirstOrDefault();
            
            //TABLICA NOWYCH PRZEGROD
            elements = new List<ElementModel>();

            tempHeight = _cabinet.Height - _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Bottom).EHeight - _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Top).EHeight;

            tempWidth = _cabinet.SizeElement;

            tempDepth = _cabinet.Depth - back;

            tempEy = _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Bottom).EHeight;

            tempEx = (_cabinet.Width - _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Leftside).EWidth -
                          _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Rightside).EWidth - number * _cabinet.SizeElement) / (number + 1);
            for (int i = 0; i < number; i++)
            {
                var element = new ElementModel
                {
                    EHeight = tempHeight,
                    EWidth = tempWidth,
                    EDepth = tempDepth,
                    Ex = _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Leftside).EWidth + tempEx * (i + 1) + _cabinet.SizeElement * i,
                    Ey = tempEy,
                    EName = EnumCabinetElement.VerticalBarrier
                };
                elements.Add(element);
            }

            return elements;
        }
    }
}