using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Factory;
using Core.Model;
using Newtonsoft.Json;
using NLog;

namespace Core
{
    public class Cabinet:CabinetModel
    {
        private ElementModel _leftSide;
        private ElementModel _rightSide;
        private ElementModel _bottom;
        private ElementModel _top;

        /// <summary>
        /// Constructor z domyslnymi parametrami
        /// ustawia podstawowe wartości szafki, alokuje pamięć dla tablic i wywoluje metode ustawiania podstawowych elementów
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="depth"></param>
        /// <param name="sizeElement"></param>
        /// <param name="back"></param>
        /// <param name="backSize"></param>
        /// <param name="cabinetType"></param>
        /// <param name="name"></param>
        public Cabinet( int height = 720, int width = 600, int depth = 510, int sizeElement = 18, EnumBack back=EnumBack.Brak, int backSize = 3, EnumCabinetType cabinetType = EnumCabinetType.Standard, string name="")
        {
            HorizontalBarrier = new List<ElementModel>();
            VerticalBarrier = new List<ElementModel>();
            CabinetElements = new List<ElementModel>();
            FrontList = new List<ElementModel>();

            CabinetType = cabinetType;

            Height = height;

            Width = width;

            Depth = depth;

            SizeElement = sizeElement;

            BackSize = backSize;

            Back = back;

            Name = name;

            GlobalCabinetElement();

            ChangeBack();
            HorizontalBarrierFactory = new HorizontalBarrierFactory(this);
            VerticalBarrierFactory = new VerticalBarrierFactory(this);
            Front = new Front(this);
        }

        public void AddHorizontalBarrier(BarrierParameter barrierParameter)
        {
            HorizontalBarrierParameter = barrierParameter;
            HorizontalBarrier = HorizontalBarrierFactory.AddBarrier(barrierParameter);
        }

        private void GlobalCabinetElement()
        {
            //leftSide=new ElementModel();
            //var rightSide = new ElementModel();
            //var bottom = new ElementModel();
            //var top = new ElementModel();
            
            switch (CabinetType)
            {
                case EnumCabinetType.Standard:

                    _leftSide = new ElementModel
                    {
                        EName = EnumCabinetElement.Leftside,
                        Description = "Bok Lewy",
                        EHeight = Height,
                        EWidth = SizeElement,
                        EDepth = Depth,
                        Ex = 0,
                        Ey = 0,
                        Ez = SwitchBack.ValueAxisZbyBackTypeAndSize(this)
                    };


                    _rightSide = new ElementModel
                    {
                        EName = EnumCabinetElement.Rightside,
                        Description = "Bok Prawy",
                        EHeight = Height,
                        EWidth = SizeElement,
                        EDepth = Depth,
                        Ex = Width - SizeElement,
                        Ez = SwitchBack.ValueAxisZbyBackTypeAndSize(this)
                    };


                    _bottom = new ElementModel
                    {
                        EName = EnumCabinetElement.Bottom,
                        Description = "Spód",
                        EHeight = SizeElement,
                        EWidth = Width - 2 * SizeElement,
                        EDepth = Depth,
                        Ex = SizeElement,
                        Ey = 0,
                        Ez = SwitchBack.ValueAxisZbyBackTypeAndSize(this)

                    };


                    _top = new ElementModel
                    {
                        EName = EnumCabinetElement.Top,
                        Description = "Góra",
                        EHeight = SizeElement,
                        EWidth = Width - 2 * SizeElement,
                        EDepth = Depth,
                        Ex = SizeElement,
                        Ey = Height - SizeElement,
                        Ez = SwitchBack.ValueAxisZbyBackTypeAndSize(this)
                    };


                    break;

                case EnumCabinetType.odwrotna:
                    break;
                case EnumCabinetType.duzy_spod:
                    break;
                case EnumCabinetType.duza_gora:
                    break;

                default:

                    _leftSide = new ElementModel
                    {
                        EName = EnumCabinetElement.Leftside,
                        Description = "Bok Lewy",
                        EHeight = Height,
                        EWidth = SizeElement,
                        EDepth = Depth,
                        Ex = 0,
                        Ey = 0,
                        Ez = SwitchBack.ValueAxisZbyBackTypeAndSize(this)
                    };


                    _rightSide = new ElementModel
                    {
                        EName = EnumCabinetElement.Rightside,
                        Description = "Bok Prawy",
                        EHeight = Height,
                        EWidth = SizeElement,
                        EDepth = Depth,
                        Ex = Width - SizeElement,
                        Ez = SwitchBack.ValueAxisZbyBackTypeAndSize(this)
                    };


                    _bottom = new ElementModel
                    {
                        EName = EnumCabinetElement.Bottom,
                        Description = "Spód",
                        EHeight = SizeElement,
                        EWidth = Width - 2 * SizeElement,
                        EDepth = Depth,
                        Ex = SizeElement,
                        Ey = 0,
                        Ez = SwitchBack.ValueAxisZbyBackTypeAndSize(this)

                    };


                    _top = new ElementModel
                    {
                        EName = EnumCabinetElement.Top,
                        Description = "Góra",
                        EHeight = SizeElement,
                        EWidth = Width - 2 * SizeElement,
                        EDepth = Depth,
                        Ex = SizeElement,
                        Ey = Height - SizeElement,
                        Ez = SwitchBack.ValueAxisZbyBackTypeAndSize(this)
                    };

                    break;

            }

            ChangeCabinetElement(EnumCabinetElement.Leftside,_leftSide);
            ChangeCabinetElement(EnumCabinetElement.Rightside,_rightSide);
            ChangeCabinetElement(EnumCabinetElement.Bottom,_bottom);
            ChangeCabinetElement(EnumCabinetElement.Top,_top);
        }

        private void ChangeBack()
        {
            ElementModel elementBack;

            if (Back == EnumBack.Nakladane)
            {
                elementBack = new ElementModel { EName = EnumCabinetElement.Back, Description = "Plecy", EHeight = Height, EWidth = Width, EDepth = BackSize, Ex = 0, Ey = 0, Ez = 0 };

                Depth = SwitchBack.SwitchDepthByBackType(Depth, Back, BackSize);

                ChangeCabinetElement(EnumCabinetElement.Back,elementBack);
                
            }
            
        }

        public void AddBack(EnumBack back=EnumBack.Nakladane)
        {
            Back = back;
            ChangeBack();
        }

        //public void AddVerticalBarrier(int number, int barrier = 0, int back = 0)
        //{
        //    _verticalBarrier.AddBarrier(number,barrier,back);
        //}

        public void AddVerticalBarrier(BarrierParameter barrierParameter)
        {
            if (barrierParameter != null)
            {
                VerticalBarrierParameter = barrierParameter;
                VerticalBarrier = VerticalBarrierFactory.AddBarrier(barrierParameter);
            }
            
            if(HorizontalBarrierParameter!=null)
                AddHorizontalBarrier(HorizontalBarrierParameter);

        }

        private void ChangeCabinetElement(EnumCabinetElement enumCabinetElement, ElementModel element)
        {
            if (CabinetElements.Exists(c => c.EName == enumCabinetElement))
            {
                var index = CabinetElements.FindIndex(c => c.EName == enumCabinetElement);
                CabinetElements[index].Description = element.Description;
                CabinetElements[index].EName = element.EName;
                CabinetElements[index].EHeight = element.EHeight;
                CabinetElements[index].EWidth = element.EWidth;
                CabinetElements[index].EDepth = element.EDepth;
                CabinetElements[index].Ex = element.Ex;
                CabinetElements[index].Ey = element.Ey;
                CabinetElements[index].Ez = element.Ez;
                
                //CabinetElements[index] = element;
            }
            else
            {
                CabinetElements.Add(element);
            }
        }

        public void AddFront(int number, EnumFront enumFront)
        {
            var slots = new Slots { BetweenVertically = 3, BetweenHorizontally = 3 };
            FrontList=Front.AddFront(number, slots,enumFront);
        }

        public void AddFront(int numberVertically=0)
        {
            var slots = new Slots {BetweenVertically = 3, BetweenHorizontally = 3 };

            AddFront(slots,numberVertically);
        }

        public void AddFront(Slots slots,int number=0 )
        {
            FrontList = Front.AddFront(number, slots,EnumFront.Pionowo);
        }

        public void AddFront(Slots slots, int number, EnumFront enumFront)
        {
            FrontList = Front.AddFront(number,slots,enumFront);
        }

        public void AddFront(List<ElementModel> frontList)
        {
            FrontList = Front.AddFront(frontList);
        }

        public void UpdateFront(ElementModel front)
        {
            if (!FrontList.Exists(x => x.Guid == front.Guid)) return;
            {
                var T = FrontList.FirstOrDefault(x => x.Guid == front.Guid);
                T.EWidth = front.EWidth;
                T.EHeight = front.EHeight;
                T.EDepth = front.EDepth;
                T.Ex = front.Ex;
                T.Ey = front.Ey;
                T.Ez = front.Ez;
                T.Description = front.Description;
                T.EName = front.EName;
            }
        }

        public ElementModel GetFront(int number)
        {
            if(FrontList.Count >= number)return FrontList[number];
            return new ElementModel();
        }

        public int GetFrontCount()
        {
            return FrontList.Count;
        }

        public List<ElementModel> GetFrontList()
        {
            return FrontList;
        }

        public void DeleteFront(ElementModel front)
        {
            if (!FrontList.Exists(x => x.Guid == front.Guid)) return;
            {
                FrontList.Remove(front);
            }
        }

        public void Serialize()
        {
            var path = Path.Combine(Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments), "Cabinet_Maker",Name + ".json");

            File.WriteAllText(path, JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        public void Serialize(string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
        //var filename = @"D:\Hasla\settings.json";
        //_settings = File.Exists(filename)
        //    ? JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(filename))
        //: new Dictionary<string, string>();

        public void Redraw()
        {
            GlobalCabinetElement();
            AddVerticalBarrier(VerticalBarrierParameter);
        }
    }
}
     
