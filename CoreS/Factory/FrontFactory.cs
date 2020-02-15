using System;
using System.Collections.Generic;
using System.Linq;
using Core.Model;
using CoreS.Enum;
using CoreS.Model;
using NLog;

namespace CoreS.Factory
{
    public class FrontFactory
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();


        private readonly Cabinet _cabinet;
        private List<ElementModelDTO> _frontList;
        private int number;
        private EnumFront enumFront;
        private SlotsModel slots;

        public FrontFactory(Cabinet cabinet)
        {
            _cabinet = cabinet;
            _frontList=new List<ElementModelDTO>();
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

        public List<ElementModelDTO> NewFront(int Number, SlotsModel Slots, EnumFront EnumFront)
        {
            number = Number;
            slots = Slots;
            enumFront = EnumFront;

            return Recalculate();
        }
        
        public List<ElementModelDTO> Add(int element)
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

        
        public List<ElementModelDTO> Delete(int delete)
        {
            try
            {
                number -= delete;
                if (number <= 0)
                {
                    number = 0;
                    return new List<ElementModelDTO>();
                }
                return Recalculate();
            }
            catch (ArgumentException e)
            {
                Logger.Error(e, "Blad usuwania frontu"); ;
                throw e;
            }
        }

        public List<ElementModelDTO> Delete(ElementModelDTO element)
        {
            throw new NotImplementedException();
        }

        public List<ElementModelDTO> DeleteAll()
        {
            number = 0;
            return new List<ElementModelDTO>();
        }

        public List<ElementModelDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<ElementModelDTO> ReCount()
        {
            throw new NotImplementedException();
        }

        public Result<List<ElementModelDTO>> Update(ElementModelDTO front)
        {
            if (!_frontList.Exists(x => x.GetGuid() == front.GetGuid())) return new Result<List<ElementModelDTO>> { Value = _frontList, IsValid = false, Errors = new List<Error> { new Error { ErrorMessage = "Obiekt nie znaleziomy" } } } ;
            {
                var T = _frontList.FirstOrDefault(x => x.GetGuid() == front.GetGuid());
                T.SetWidth(front.GetWidth());
                T.SetHeight(front.GetHeight());
                T.SetDepth(front.GetDepth());
                T.SetX(front.GetX());
                T.SetY(front.GetY());
                T.SetZ(front.GetZ());
                T.SetDescription(front.GetDescription());          
            }

            var result = new Result<List<ElementModelDTO>>
            {
                Value = _frontList,
                IsValid = true
            };


            return result;
        }

        private List<ElementModelDTO> Recalculate()
        {
            if (number==0) throw new ArgumentException("Wartosc musi byc wieksza niz 0 lub null");
                
            _frontList = new List<ElementModelDTO>();

            var width = enumFront.HasFlag(EnumFront.Pionowo) ? 
                (_cabinet.Width() - slots.Left - slots.Right - slots.BetweenVertically * (number - 1)) / number :
                _cabinet.Width() - slots.Left - slots.Right;

            var height = enumFront.HasFlag(EnumFront.Poziomo)?
                (_cabinet.Height() - slots.Top - slots.Bottom - slots.BetweenHorizontally * (number - 1)) / number
                : _cabinet.Height() - slots.Top - slots.Bottom;
            

            for (var i = 0; i < number; i++)
            {
                var front = new ElementModelDTO(

                    description: "Front",
                    height: height,
                    width:width,
                    depth:_cabinet.SizeElement(),
                    x: enumFront.HasFlag(EnumFront.Pionowo) ? slots.Left + (width + slots.BetweenVertically) * i : slots.Left,
                    y: enumFront.HasFlag(EnumFront.Poziomo) ? slots.Bottom + (height + slots.BetweenHorizontally) * i : slots.Right,
                    z: _cabinet.Depth() + slots.BetweenCabinet,
                    enumCabinet: EnumCabinetElement.Front,
                    horizontal: false); 
                
                _frontList.Add(front);
            }

            return _frontList;


        }

        public List<ElementModelDTO> AddFront(List<ElementModelDTO> frontListT)
        {
            foreach (var element in frontListT)
            {
                _frontList.Add(element);
            }

            return _frontList;
        }
    }
}