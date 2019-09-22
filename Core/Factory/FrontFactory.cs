using System;
using System.Collections.Generic;
using Core.Model;

namespace Core.Factory
{
    public class FrontFactory
    {
        private readonly Cabinet _cabinet;
        private List<ElementModel> _frontList;

        public FrontFactory(Cabinet cabinet)
        {
            _cabinet = cabinet;
            _frontList=new List<ElementModel>();
        }

        public List<ElementModel> AddFront(int number,SlotsModel slots,EnumFront enumFront)
        {
            if (number==0) throw new ArgumentException("Wartosc musi byc wieksza niz 0 lub null");
                
            _frontList = new List<ElementModel>();

            var width = enumFront.HasFlag(EnumFront.Pionowo) ? 
                (_cabinet.Width() - slots.Left - slots.Right - slots.BetweenVertically * (number - 1)) / number :
                _cabinet.Width() - slots.Left - slots.Right;

            var height = enumFront.HasFlag(EnumFront.Poziomo)?
                (_cabinet.Height() - slots.Top - slots.Bottom - slots.BetweenHorizontally * (number - 1)) / number
                : _cabinet.Height() - slots.Top - slots.Bottom;
            

            for (var i = 0; i < number; i++)
            {
                var front = new ElementModel
                {
                    EName = EnumCabinetElement.Front,
                    EDepth = _cabinet.SizeElement(),
                    EHeight = height,
                    EWidth = width,
                    Ex = enumFront.HasFlag(EnumFront.Pionowo) ? slots.Left + (width+slots.BetweenVertically)*i : slots.Left,
                    Ey = enumFront.HasFlag(EnumFront.Poziomo) ? slots.Bottom + (height+slots.BetweenHorizontally)*i : slots.Right,
                    Ez = _cabinet.Depth()+slots.BetweenCabinet
                };

                _frontList.Add(front);
            }

            return _frontList;


        }

        public List<ElementModel> AddFront(List<ElementModel> frontListT)
        {
            foreach (var element in frontListT)
            {
                _frontList.Add(element);
            }

            return _frontList;
        }
    }
}