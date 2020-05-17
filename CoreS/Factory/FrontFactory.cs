using Config;
using Core.Model;
using CoreS.Enum;
using CoreS.Helpers;
using CoreS.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CoreS.Factory
{
    public class FrontFactory:MyLogger
    {
        private readonly Cabinet _cabinet;
        private List<ElementModelDTO> _frontList;
        private int number;
        private EnumFront enumFront;
        private SlotsModel slots;
        private IConfig _config;

        public FrontFactory(Cabinet cabinet,IConfig config)
        {
            Logger.Info("FrontFactory Constructor");
            _config = config;
            _cabinet = cabinet;
            _frontList = new List<ElementModelDTO>();
            slots = new SlotsModel(_config);

            enumFront = EnumFront.Nakladany | EnumFront.Pionowo;
        }

        public List<ElementModelDTO> NewFront(int Number, SlotsModel Slots, EnumFront EnumFront)
        {
            Logger.Info("NewFront(int Number, SlotsModel Slots, EnumFront EnumFront) in FrontFactory");
            Logger.Debug("Number: {0} Slots: {1} EnumFront: {2}", Number,Slots,EnumFront);
            number = Number;
            slots = Slots;
            enumFront = EnumFront;

            return Recalculate();
        }

        public List<ElementModelDTO> Add(int element)
        {
            Logger.Info("Add(int element) in FrontFactory");
            Logger.Debug("Element: {0}", element);
            try
            {
                if (element < 0)
                    throw new ArgumentException();
                number += element;

                return Recalculate();
            }
            catch (ArgumentException e)
            {
                Logger.Error(e, "Error Add"); ;
                throw new ArgumentException();
            }
        }

        public List<ElementModelDTO> Add(List<ElementModelDTO> list)
        {
            Logger.Info("Add(List<ElementModelDTO> list) in FrontFactory");
            Logger.Debug("list: {0} ", list);
            _frontList = new List<ElementModelDTO>();
            foreach (var item in list)
            {
                _frontList.Add(item);
            }
            number = _frontList.Count();
            return _frontList;
        }

        public List<ElementModelDTO> Delete(int delete)
        {
            Logger.Info("Delete(int delete) in FrontFactory");
            Logger.Debug("Delete: {0}", delete); 
            try
            {
                number -= delete;
                if (number <= 0)
                {
                    number = 0;
                    return _frontList= new List<ElementModelDTO>();
                }
                return Recalculate();
            }
            catch (ArgumentException e)
            {
                Logger.Error(e, "Error Add Delete"); ;
                throw e;
            }
        }

        public List<ElementModelDTO> DeleteElement(ElementModelDTO element)
        {
            Logger.Info("DeleteElement(ElementModelDTO element) in FrontFactory");
            Logger.Debug("Element: {0}", element); 
            var findElement = _frontList.Find(x => x.GetGuid() == element.GetGuid());
            if (findElement != null)
            {
                _frontList.Remove(findElement);
                number -= 1;
            }

            return _frontList;
        }

        public List<ElementModelDTO> Remove()
        {
            Logger.Info("Remove() in FrontFactory");
            number = 0;
            _frontList = new List<ElementModelDTO>();
            return _frontList;
        }

        public List<ElementModelDTO> GetAll()
        {
            Logger.Info("GetAll() in FrontFactory");
            return _frontList;
        }

        public List<ElementModelDTO> ReCount()
        {
            Logger.Info("ReCount() FrontFactory");
            return Recalculate();
        }

        // TDOD to modification Result<List<ElementModelDTO>> 
        // TODO new Result<List<ElementModelDTO>> { Value = _frontList, IsValid = false, Errors = new List<Error> { new Error { ErrorMessage = "Obiekt nie znaleziomy" } } };
        
        //public List<ElementModelDTO> Update(ElementModelDTO front) 
        //{
        //    Logger.Info("Update(ElementModelDTO front) in FrontFactory");
        //    if (!_frontList.Exists(x => x.GetGuid() == front.GetGuid())) return _frontList;
        //    {
        //        var T = _frontList.FirstOrDefault(x => x.GetGuid() == front.GetGuid());
        //        T.SetWidth(front.GetWidth());
        //        T.SetHeight(front.GetHeight());
        //        T.SetDepth(front.GetDepth());
        //        T.SetX(front.GetX());
        //        T.SetY(front.GetY());
        //        T.SetZ(front.GetZ());
        //        T.SetDescription(front.GetDescription());
        //    }

        //    //var result = new Result<List<ElementModelDTO>>
        //    //{
        //    //    Value = _frontList,
        //    //    IsValid = true
        //    //};

        //    return _frontList;
        //}

        private List<ElementModelDTO> Recalculate()
        {
            Logger.Info("Recalculate() in FrontFactory");
            if (number == 0) return _frontList;

            _frontList = new List<ElementModelDTO>();

            var width = enumFront.HasFlag(EnumFront.Pionowo) ?
                (_cabinet.Width() - slots.Left - slots.Right - slots.BetweenVertically * (number - 1)) / number :
                _cabinet.Width() - slots.Left - slots.Right;

            var height = enumFront.HasFlag(EnumFront.Poziomo) ?
                (_cabinet.Height() - slots.Top - slots.Bottom - slots.BetweenHorizontally * (number - 1)) / number
                : _cabinet.Height() - slots.Top - slots.Bottom;

            for (var i = 0; i < number; i++)
            {
                var front = new ElementModelDTO(

                    description: "Front",
                    height: height,
                    width: width,
                    depth: _cabinet.SizeElement(),
                    x: enumFront.HasFlag(EnumFront.Pionowo) ? slots.Left + (width + slots.BetweenVertically) * i : slots.Left,
                    y: enumFront.HasFlag(EnumFront.Poziomo) ? slots.Bottom + (height + slots.BetweenHorizontally) * i : slots.Right,
                    z: _cabinet.Depth() + slots.BetweenCabinet,
                    enumCabinet: EnumCabinetElement.Front,
                    horizontal: false);

                _frontList.Add(front);
            }

            return _frontList;
        }

        public void Recal()
        {
            Logger.Info("Recal() in FrontFactory");
            if (number == 0) return;

            //var list = new List<ElementModelDTO>();

            List<int> x11 = new List<int>();
            List<int> x12 = new List<int>();

            foreach (var item in _frontList)
            {
                if (item.ChangeWidth)
                {
                    x11.Add(item.GetX());
                    x12.Add(item.GetX() + item.GetWidth());
                                        
                }
            }




            //switch (enumFront)
            //{
            //    case EnumFront.Nakladany | EnumFront.Pionowo:

            //        ////width
            //        //int x1; //pczatek zmian
            //        //int x2; //koniec zmian

            //        //int i = 0; //licznik elementu

            //        //if (_frontList[i].ChangeWidth) //jesli pierwszy ma zmieniona szerokosc
            //        //{
            //        //    x1 = _frontList[i].GetX();

            //        //    if (_frontList[i + 1].ChangeWidth)//jesli kolejny - drugi ma zmieniona szerokosc
            //        //    {
            //        //        if (_frontList[i + 2].ChangeWidth) //jesli kolejny - trzeci ma zmieniona szerokosc
            //        //        {
            //        //            x2 = _frontList[i + 2].GetX() + _frontList[i + 2].GetWidth(); //trzeci ma zmieniona szerokosc                                
            //        //        }
            //        //        else
            //        //        {
            //        //            x2 = _frontList[i + 1].GetX() + _frontList[i + 1].GetWidth();  //trzeci ma nie zmieniona szerokosc tylko drugi 
            //        //        }
            //        //    }
            //        //    else
            //        //    {
            //        //        x2 = _frontList[i].GetX() + _frontList[i].GetWidth(); //drugi ma nie zmieniona wysokosc 
            //        //    }
            //        //}
            //        //else //pierwszy ma nie zmieniona szerokosc
            //        //{
            //        //    if (_frontList[i + 1].ChangeWidth) //jesli drugi ma zmieniona szerokosc
            //        //    {
            //        //        x1 = _frontList[i + 1].GetX();

            //        //        if (_frontList[i + 2].ChangeWidth)
            //        //        {
            //        //            x2 = _frontList[i + 2].GetX() + _frontList[i + 2].GetWidth(); //trzeci ma zmieniona szerokosc   
            //        //        }
            //        //        else
            //        //        {
            //        //            x2 = _frontList[i + 1].GetX() + _frontList[i + 1].GetWidth();  //trzeci ma nie zmieniona szerokosc tylko drugi 
            //        //        }
            //        //    }
            //        //    else
            //        //    {
            //        //        x1 = _frontList[i + 2].GetX();
            //        //        x2 = _frontList[i + 2].GetX() + _frontList[i + 2].GetWidth(); //trzeci ma zmieniona szerokosc   
            //        //    }
            //        //}

            //        //List<int> x11 = new List<int>();
            //        //List<int> x12 = new List<int>();

            //        //foreach (var item in _frontList)
            //        //{
            //        //    if (item.ChangeWidth)
            //        //    {
            //        //        x11.Add(item.GetX());
                            
            //        //        if (x11.Count > 2)
            //        //        {
            //        //            if (item.GetX() - x11[x11.Count - 1] == x12[x12.Count - 1])
            //        //            {
            //        //                x12.Add(item.GetY());
            //        //            }
            //        //        }
                            


            //        //    }
            //        //}



            //        var width = (_cabinet.Width() - slots.Left - slots.Right - slots.BetweenVertically * (number - 1)) / number;

            //        var height = _cabinet.Height() - slots.Top - slots.Bottom;

            //        break;

            //    case EnumFront.Nakladany | EnumFront.Poziomo:
            //        break;

            //    case EnumFront.Wpuszczany | EnumFront.Pionowo:
            //        break;

            //    case EnumFront.Wpuszczany | EnumFront.Poziomo:
            //        break;

            //    case EnumFront.Szuflada:
            //        break;

            //    default:
            //        break;
            //}
        }

        public List<ElementModelDTO> AddFront(List<ElementModelDTO> frontListT)
        {
            Logger.Info("AddFront(List<ElementModelDTO> frontListT) in FrontFactory");
            foreach (var element in frontListT)
            {
                _frontList.Add(element);
            }

            return _frontList;
        }

        public List<ElementModelDTO> Update(EnumElementParameter parameter, string text, int result, ElementModelDTO elementModelDTO)
        {
            Logger.Info("Update(EnumElementParameter parameter, string text, int result, ElementModelDTO elementModelDTO) in FrontFactory");
            Logger.Debug("Parameter: {0}, Text:, {1}, Result: {2}, ElemenModelDTO: {3}", parameter, text, result, elementModelDTO);
            
            if (!_frontList.Exists(x => x.GetGuid() == elementModelDTO.GetGuid())) return _frontList;

            var item = _frontList.Find(x => x.GetGuid() == elementModelDTO.GetGuid());
            
            switch (parameter)
            {
                case EnumElementParameter.Width:
                    item.SetWidth(result);
                    break;
                case EnumElementParameter.Height:
                    item.SetHeight(result);
                    break;
                case EnumElementParameter.Depth:
                    item.SetDepth(result);
                    break;
                case EnumElementParameter.Description:
                    item.SetDescription(text);
                    break;
                case EnumElementParameter.X:
                    item.SetX(result);
                    break;
                case EnumElementParameter.Y:
                    item.SetY(result);
                    break;
                case EnumElementParameter.Z:
                    item.SetZ(result);
                    break;
                default:
                    break;
            }

            return _frontList;
        }
    }
}