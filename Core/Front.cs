using System;

namespace Core
{
    public class Front
    {
        private Cabinet _cabinet;

        public Front(Cabinet cabinet)
        {
            _cabinet = cabinet;
        }

        public void AddFront(int number,Slots slots,EnumFront enumFront)
        {

            var width = enumFront==EnumFront.Pionowo ? 
                (_cabinet.Width - slots.left - slots.right - slots.betweenVertically * (number - 1)) / number :
                _cabinet.Width - slots.left - slots.right;

            var height = enumFront == EnumFront.Poziomo
                ? (_cabinet.Height - slots.top - slots.bottom - slots.betweenHorizontally * (number - 1)) / number
                : _cabinet.Height - slots.top - slots.bottom;
            

            for (var i = 0; i < number; i++)
            {
                var front = new Element
                {
                    EName = EnumCabinetElement.Front,
                    EDepth = _cabinet.SizeElement,
                    EHeight = height,
                    EWidth = width,
                    Ex = slots.left + (width+slots.betweenVertically)*(i),
                    Ey = slots.right,
                    Ez = _cabinet.Depth+slots.betweenCabinet
                };

                _cabinet.FrontList.Add(front);
            }

            
            
            
        }
    }
}