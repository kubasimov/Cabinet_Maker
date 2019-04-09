using System;
using System.Collections.Generic;

namespace Core
{
    public class Front
    {
        private Cabinet _cabinet;
        private List<Element> frontList;

        public Front(Cabinet cabinet)
        {
            _cabinet = cabinet;
            frontList=new List<Element>();
        }

        public List<Element> AddFront(int number,Slots slots,EnumFront enumFront)
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
                    Ex = enumFront==EnumFront.Pionowo ? slots.left + (width+slots.betweenVertically)*i : slots.left,
                    Ey = enumFront==EnumFront.Poziomo ? slots.bottom + (height+slots.betweenHorizontally)*i : slots.right,
                    Ez = _cabinet.Depth+slots.betweenCabinet
                };

                frontList.Add(front);
            }

            return frontList;


        }

        public List<Element> AddFront(List<Element> frontListT)
        {
            foreach (var element in frontListT)
            {
                frontList.Add(element);
            }

            return frontList;
        }
    }
}