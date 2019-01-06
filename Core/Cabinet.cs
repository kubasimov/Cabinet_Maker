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
        private readonly Back _switchBack = new Back();

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Cabinet( int height = 720, int width = 600, int depth = 510, int sizeElement = 18, EnumBack back=EnumBack.Brak, int backSize = 3, EnumCabinetType cabinetType = EnumCabinetType.Standard)
        {
            HorizontalBarrier = new List<Element>();
            VerticalBarrier = new List<Element>();
            CabinetElements = new List<Element>();

            CabinetType = cabinetType;

            Height = height;

            Width = width;

            Depth = new Back().SwitchDepthByBackType(depth,back,backSize);
            
            SizeElement = sizeElement;

            BackSize = backSize;

            Back = back;

            GlobalCabinetElement();

        }

        private void GlobalCabinetElement()
        {
            Element leftSide;
            Element rightSide;
            Element bottom;
            Element top;
            Element back;

            switch (CabinetType)
            {
                default:

                    leftSide = new Element { EName = EnumCabinetElement.Leftside, Description="Bok Lewy", EHeight = Height, EWidth = SizeElement, EDepth = Depth, Ex = 0, Ey = 0, Ez = _switchBack.ValueAxisZbyBackTypeAndSize(this) };

                    rightSide = new Element { EName = EnumCabinetElement.Rightside, Description = "Bok Prawy", EHeight = Height, EWidth = SizeElement, EDepth = Depth, Ex = Width - SizeElement, Ez = _switchBack.ValueAxisZbyBackTypeAndSize(this) };

                    bottom = new Element { EName = EnumCabinetElement.Bottom, Description = "Spód", EHeight = SizeElement, EWidth = Width - 2 * SizeElement, EDepth = Depth, Ex = SizeElement, Ey = 0, Ez = _switchBack.ValueAxisZbyBackTypeAndSize(this) };
                    top = new Element
                    { EName = EnumCabinetElement.Top, Description = "Góra", EHeight = SizeElement, EWidth = Width - 2 * SizeElement, EDepth = Depth, Ex = SizeElement, Ey = Height - SizeElement, Ez = _switchBack.ValueAxisZbyBackTypeAndSize(this) };

                    if (Back==EnumBack.Nakladane)
                    {
                        back = new Element{EName=EnumCabinetElement.Back, Description="Plecy", EHeight=Height, EWidth=Width, EDepth = BackSize, Ex=0,Ey=0,Ez=0};
                    }else

                    back = new Element();

                    break;

            }
            
            CabinetElements.Add(leftSide);
            CabinetElements.Add(rightSide);
            CabinetElements.Add(bottom);
            CabinetElements.Add(top);
            CabinetElements.Add(back);
        }


        public void AddBack(EnumBack back=EnumBack.Nakladane)
        {
            Back = back;
        }

        /// <summary>
        /// Dodanie poziomej przegrody szafki
        /// </summary>
        /// <param name="number">Ilosc dodawanych przegrod</param>
        /// <param name="barrier">numer rzedu do wstawienia przegrody - domyslnie 0</param>
        /// <param name="back">wielkosc odsuniecia od przodu wstawianej przegrody</param>
        public void AddHorizontalBarrier(int number, int barrier = 0, int back=0, List<int> height = default(List<int>))
        {
            int tempHeight;
            
            int tempDepth = Depth - back;

            int tempEy;

            int tempWidth;

            if (VerticalBarrier.Count>0)
            {
                if (barrier == 0)
                {
                    tempWidth = VerticalBarrier[barrier].Ex-CabinetElements.First(x => x.EName == EnumCabinetElement.Leftside).EWidth;
                }
                else if (barrier == VerticalBarrier.Count)
                {
                    tempWidth = CabinetElements.First(x=>x.EName==EnumCabinetElement.Rightside).Ex- VerticalBarrier[barrier-1].Ex - VerticalBarrier[barrier-1].EWidth;
                }
                else
                {
                    tempWidth = VerticalBarrier[barrier].Ex - VerticalBarrier[barrier-1].Ex - VerticalBarrier[barrier-1].EWidth;
                }
            }
            else
            {
                tempWidth = Width - 2 * SizeElement;
            }
            
            
            //Wysokosc elementu
            tempHeight = SizeElement;



            //polozenie elementu na osi x
            //Jesli pierwsza lub jedyna przestrzen to punkt poczatkowy wiekszy o szerokosc boku
            //Jesli wybrano inna przestrzen niz pierwsza ( jedyna ) to punkt poczatkowy bariery + jej grubosc

            var tempEx = barrier==0 ? CabinetElements.First((x=>x.EName==EnumCabinetElement.Leftside)).EWidth : VerticalBarrier[barrier-1].Ex+VerticalBarrier[barrier-1].EWidth;


            //polozenie elementu na osi y

            if (height==null)
            {
                tempEy = (Height - CabinetElements.First(x => x.EName == EnumCabinetElement.Bottom).EHeight - CabinetElements.First(x => x.EName == EnumCabinetElement.Top).EHeight
                      - number*SizeElement) / (number+1);

                for (var i = 0; i < number; i++)
                {
                    var element = new Element
                    {
                        EHeight = tempHeight,
                        EWidth = tempWidth,
                        EDepth = tempDepth,
                        Ex = tempEx,
                        Ey = CabinetElements.First(x => x.EName == EnumCabinetElement.Bottom).EHeight + tempEy * (i+1)  + SizeElement*i,
                        EName=EnumCabinetElement.HorizontalBarrier
                    };

                    HorizontalBarrier.Add(element);

                    
                }
            }
            else
            {
                
            }
            
            
        }

        public void AddVerticalBarrier(int number, int barrier = 0, int back = 0)
        {
            int tempHeight= Height - CabinetElements.First(x => x.EName == EnumCabinetElement.Bottom).EHeight - CabinetElements.First(x => x.EName == EnumCabinetElement.Top).EHeight;

            int tempWidth = SizeElement;

            int tempDepth = Depth - back;

            int tempEy= CabinetElements.First(x => x.EName == EnumCabinetElement.Bottom).EHeight;

            int tempEx = (Width - CabinetElements.First(x => x.EName == EnumCabinetElement.Leftside).EWidth -
                         CabinetElements.First(x => x.EName == EnumCabinetElement.Rightside).EWidth-number * SizeElement)/(number+1);
            for (int i = 0; i < number; i++)
            {
                var element = new Element
                {
                    EHeight = tempHeight,
                    EWidth = tempWidth,
                    EDepth = tempDepth,
                    Ex = CabinetElements.First(x => x.EName == EnumCabinetElement.Leftside).EWidth+tempEx*(i+1)+SizeElement*i,
                    Ey = tempEy,
                    EName = EnumCabinetElement.VerticalBarrier
                };
                VerticalBarrier.Add(element);
            }
        }

        
    }
}
     
