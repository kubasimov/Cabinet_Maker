using System;
using System.Collections.Generic;
using System.Linq;
using NLog;

namespace Core
{
    public class Cabinet
    {
        public EnumCabinetType CabinetType;

        public int Height;
        public int Width;
        public int Depth;
        public int SizeElement;
        public int BackSize;
        public EnumBack Back;
        public List<Element> CabinetElements;
        public List<Element> HorizontalBarrier;
        public List<Element> VerticalBarrier;
        private List<Element> FrontList;

        private readonly Back _switchBack = new Back();

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly HorizontalBarrier _horizontalBarrier;
        private readonly VerticalBarrier _verticalBarrier;
        private readonly Front _front;

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
        public Cabinet( int height = 720, int width = 600, int depth = 510, int sizeElement = 18, EnumBack back=EnumBack.Brak, int backSize = 3, EnumCabinetType cabinetType = EnumCabinetType.Standard)
        {
            HorizontalBarrier = new List<Element>();
            VerticalBarrier = new List<Element>();
            CabinetElements = new List<Element>();
            FrontList = new List<Element>();

            CabinetType = cabinetType;

            Height = height;

            Width = width;

            Depth = depth;

            SizeElement = sizeElement;

            BackSize = backSize;

            Back = back;

            GlobalCabinetElement();

            ChangeBack();
            _horizontalBarrier = new HorizontalBarrier(this);
            _verticalBarrier = new VerticalBarrier(this);
            _front = new Front(this);
        }

        public void AddHorizontalBarrier(int number, int barrier = 0, int back = 0, List<int> height = default(List<int>))
        {
            _horizontalBarrier.AddBarrier(number,barrier,  back, height);
        }
        
        private void GlobalCabinetElement()
        {
            var leftSide=new Element();
            var rightSide = new Element();
            var bottom = new Element();
            var top = new Element();
            
            switch (CabinetType)
            {
                case EnumCabinetType.Standard:
                    leftSide = new Element
                        { EName = EnumCabinetElement.Leftside, Description = "Bok Lewy", EHeight = Height, EWidth = SizeElement, EDepth = Depth, Ex = 0, Ey = 0, Ez = _switchBack.ValueAxisZbyBackTypeAndSize(this) };

                    rightSide = new Element
                        { EName = EnumCabinetElement.Rightside, Description = "Bok Prawy", EHeight = Height, EWidth = SizeElement, EDepth = Depth, Ex = Width - SizeElement, Ez = _switchBack.ValueAxisZbyBackTypeAndSize(this) };

                    bottom = new Element
                        { EName = EnumCabinetElement.Bottom, Description = "Spód", EHeight = SizeElement, EWidth = Width - 2 * SizeElement, EDepth = Depth, Ex = SizeElement, Ey = 0, Ez = _switchBack.ValueAxisZbyBackTypeAndSize(this) };

                    top = new Element
                        { EName = EnumCabinetElement.Top, Description = "Góra", EHeight = SizeElement, EWidth = Width - 2 * SizeElement, EDepth = Depth, Ex = SizeElement, Ey = Height - SizeElement, Ez = _switchBack.ValueAxisZbyBackTypeAndSize(this) };
                    
                    break;
                case EnumCabinetType.odwrotna:

                    break;
                case EnumCabinetType.duzy_spod:
                    break;
                case EnumCabinetType.duza_gora:
                    break;
                default:

                    leftSide = new Element
                        { EName = EnumCabinetElement.Leftside, Description="Bok Lewy", EHeight = Height, EWidth = SizeElement, EDepth = Depth, Ex = 0, Ey = 0, Ez = _switchBack.ValueAxisZbyBackTypeAndSize(this) };

                    rightSide = new Element
                        { EName = EnumCabinetElement.Rightside, Description = "Bok Prawy", EHeight = Height, EWidth = SizeElement, EDepth = Depth, Ex = Width - SizeElement, Ez = _switchBack.ValueAxisZbyBackTypeAndSize(this) };

                    bottom = new Element
                        { EName = EnumCabinetElement.Bottom, Description = "Spód", EHeight = SizeElement, EWidth = Width - 2 * SizeElement, EDepth = Depth, Ex = SizeElement, Ey = 0, Ez = _switchBack.ValueAxisZbyBackTypeAndSize(this) };

                    top = new Element
                        { EName = EnumCabinetElement.Top, Description = "Góra", EHeight = SizeElement, EWidth = Width - 2 * SizeElement, EDepth = Depth, Ex = SizeElement, Ey = Height - SizeElement, Ez = _switchBack.ValueAxisZbyBackTypeAndSize(this) };
                    
                    break;
                
            }

            ChangeCabinetElement(EnumCabinetElement.Leftside,leftSide);
            ChangeCabinetElement(EnumCabinetElement.Rightside,rightSide);
            ChangeCabinetElement(EnumCabinetElement.Bottom,bottom);
            ChangeCabinetElement(EnumCabinetElement.Top,top);
        }

        private void ChangeBack()
        {
            Element elementBack;

            if (Back == EnumBack.Nakladane)
            {
                elementBack = new Element { EName = EnumCabinetElement.Back, Description = "Plecy", EHeight = Height, EWidth = Width, EDepth = BackSize, Ex = 0, Ey = 0, Ez = 0 };

                Depth = _switchBack.SwitchDepthByBackType(Depth, Back, BackSize);

                ChangeCabinetElement(EnumCabinetElement.Back,elementBack);
                
            }
            
        }

        public void AddBack(EnumBack back=EnumBack.Nakladane)
        {
            Back = back;
            ChangeBack();
        }

        public void AddVerticalBarrier(int number, int barrier = 0, int back = 0)
        {
            _verticalBarrier.AddBarrier(number,barrier,back);
        }

        private void ChangeCabinetElement(EnumCabinetElement enumCabinetElement, Element element)
        {
            if (CabinetElements.Exists(c => c.EName == enumCabinetElement))
            {
                var index = CabinetElements.FindIndex(c => c.EName == enumCabinetElement);
                CabinetElements[index] = element;
            }
            else
            {
                CabinetElements.Add(element);
            }
        }

        public void AddFront(int number, EnumFront enumFront)
        {
            var slots = new Slots { betweenVertically = 3, betweenHorizontally = 3 };
            FrontList=_front.AddFront(number, slots,enumFront);
        }

        public void AddFront(int numberVertically=0)
        {
            var slots = new Slots {betweenVertically = 3, betweenHorizontally = 3 };

            AddFront(slots,numberVertically);
        }

        public void AddFront(Slots slots,int number=0 )
        {
            FrontList = _front.AddFront(number, slots,EnumFront.Pionowo);
        }

        public void AddFront(Slots slots, int number, EnumFront enumFront)
        {
            FrontList = _front.AddFront(number,slots,enumFront);
        }

        public void AddFront(List<Element> frontList)
        {
            FrontList = _front.AddFront(frontList);
        }

        public void UpdateFront(Element front)
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

        public Element GetFront(int number)
        {
            if(FrontList.Count >= number)return FrontList[number];
            return new Element();
        }

        public List<Element> GetFrontList()
        {
            return FrontList;
        }

        public void DeleteFront(Element front)
        {
            if (!FrontList.Exists(x => x.Guid == front.Guid)) return;
            {
                FrontList.Remove(front);
            }
        }
    }
}
     
