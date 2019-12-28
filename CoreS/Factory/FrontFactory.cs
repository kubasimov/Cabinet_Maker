using System;
using System.Collections.Generic;
using CoreS.Enum;
using CoreS.Model;
using NLog;

namespace CoreS.Factory
{
    public class FrontFactory
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();


        private readonly Cabinet _cabinet;
        private List<ElementModel> _frontList;
        private int number;
        private EnumFront enumFront;
        private SlotsModel slots;

        public FrontFactory(Cabinet cabinet)
        {
            _cabinet = cabinet;
            _frontList=new List<ElementModel>();
            slots = new SlotsModel
            {
                Top = 3,
                Bottom = 3,
                Left = 3,
                Right = 3,
                BetweenVertically = 3,
                BetweenHorizontally = 3,
                BetweenCabinet = 2
            };
            enumFront = EnumFront.Nakladany | EnumFront.Pionowo;
        }

        public List<ElementModel> NewFront(int Number, SlotsModel Slots, EnumFront EnumFront)
        {
            number = Number;
            slots = Slots;
            enumFront = EnumFront;

            return Recalculate();
        }
        
        public List<ElementModel> Add(int element)
        {
            try
            {
                if (element < 0)
                    throw new ArgumentException();
                number += element;
                
                return Recalculate();
            }
            catch(ArgumentException e)
            {
                Logger.Error(e, "Minusowa ilosc polek"); ;
                throw new ArgumentException();
            }
        }

        
        public List<ElementModel> Delete(int delete)
        {
            throw new NotImplementedException();
        }

        public List<ElementModel> Delete(ElementModel element)
        {
            throw new NotImplementedException();
        }

        public List<ElementModel> DeleteAll()
        {
            throw new NotImplementedException();
        }

        public List<ElementModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<ElementModel> ReCount()
        {
            throw new NotImplementedException();
        }

        public List<ElementModel> Update(ElementModel element)
        {
            throw new NotImplementedException();
        }

        private List<ElementModel> Recalculate()
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