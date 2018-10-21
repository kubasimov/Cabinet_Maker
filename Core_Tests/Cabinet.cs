using System;
using System.Collections.Generic;
using System.Linq;

namespace Core_Tests
{
    public class Cabinet
    {
        
        public int Height;
        public int Width;
        public int Depth;
        public int Size;
        public int BackSize;
        public Back Back;
        public List<Element> CabinetElements;
        public List<Element> HorizontalBarrier;
        public List<Element> VerticalBarrier;



        public Cabinet(int height = 720, int width = 600, int depth = 510,int size = 18,int backSize=3)
        {
            HorizontalBarrier = new List<Element>();
            VerticalBarrier = new List<Element>();
            CabinetElements = new List<Element>();
            Height = height;
            Width = width;
            Depth = depth;
            Size = size;
            BackSize = backSize;

            CabinetElement();

        }

        private void CabinetElement()
        {

            var leftSide = new Element{EName="leftside",EHeight=Height,EWidth=Size,EDepth=Depth,Ex=0,Ey=0};
            var rightSide = new Element{EName="rightside",EHeight=Height,EWidth=Size,EDepth=Depth,Ex=Width-2*Size};
            var bottom = new Element{EName="bottom",EHeight=Size,EWidth=Width-2*Size,EDepth=Depth,Ex=0,Ey=Size};
            var top = new Element
                {EName="top",EHeight = Size, EWidth = Width - 2 * Size, EDepth = Depth, Ex = Height - Size, Ey = Size};

            CabinetElements.Add(leftSide);
            CabinetElements.Add(rightSide);
            CabinetElements.Add(bottom);
            CabinetElements.Add(top);

        }


        public void AddBack(Back back=Back.Imposed)
        {
            Back = back;
        }

        public void AddHorizontalBarrier(int number,int barrier = 0, int back=0)
        {
            int tempHeight;
            
            int tempDepth = Depth - back;

            int tempEy;

            int tempWidth;

            //Szerokosc elementu
            if (barrier == 0)
            {
                tempWidth = Width - 2 * Size;
            }
            else
            {
                if (barrier == VerticalBarrier.Count)
                {
                    tempWidth = Width - VerticalBarrier[barrier].Ex - VerticalBarrier[barrier].EWidth;
                }
                else
                {
                    tempWidth = VerticalBarrier[barrier + 1].Ex - VerticalBarrier[barrier].Ex -
                                VerticalBarrier[barrier].EWidth;
                }
            }

            //Wysokosc elementu
            tempHeight = Size;



            //polozenie elementu na osi x
            //Jesli pierwsza lub jedyna przestrzen to punkt poczatkowy wiekszy o szerokosc boku
            //Jesli wybrano inna przestrzen niz pierwsza ( jedyna ) to punkt poczatkowy bariery + jej grubosc

            var tempEx = barrier==0 ? CabinetElements.First((x=>x.EName=="leftside")).EWidth : VerticalBarrier[barrier].Ex+VerticalBarrier[barrier].EWidth;


            //polozenie elementu na osi y

            tempEy = (Height - CabinetElements.First(x => x.EName == "bottom").EHeight - CabinetElements.First(x => x.EName == "top").EHeight
                      - number*Size) / (number+1);

            for (var i = 0; i < number; i++)
            {
                var element = new Element
                {
                    EHeight = tempHeight,
                    EWidth = tempWidth,
                    EDepth = tempDepth,
                    Ex = tempEx,
                    Ey = CabinetElements.First(x => x.EName == "bottom").EHeight + tempEy * (i+1)  + Size*i
                };

                HorizontalBarrier.Add(element);

                
            }
            
        }

        public void AddVerticalBarrier(int number, int barrier = 0, int back = 0)
        {
            int tempHeight= Height - CabinetElements.First(x => x.EName == "bottom").EHeight - CabinetElements.First(x => x.EName == "top").EHeight;

            int tempWidth = Size;

            int tempDepth = Depth - back;

            int tempEy= CabinetElements.First(x => x.EName == "bottom").EHeight;

            int tempEx = (Width - CabinetElements.First(x => x.EName == "leftside").EWidth -
                         CabinetElements.First(x => x.EName == "rightside").EWidth-number * Size)/(number+1);
            for (int i = 0; i < number; i++)
            {
                var element = new Element
                {
                    EHeight = tempHeight,
                    EWidth = tempWidth,
                    EDepth = tempDepth,
                    Ex = CabinetElements.First(x => x.EName == "leftside").EWidth+tempEx*(i+1)+Size*i,
                    Ey = tempEy
                };
                VerticalBarrier.Add(element);
            }
        }
    }

    public class Element
    {
        public string EName;
        public int EHeight;
        public int EWidth;
        public int EDepth;
        public int Ex;
        public int Ey;
    }

    public enum Back
    {
        Imposed,
        Mortice
    }
}
     
