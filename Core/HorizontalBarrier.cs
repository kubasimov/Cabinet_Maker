using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class HorizontalBarrier
    {
        private Cabinet _cabinet;

        public HorizontalBarrier(Cabinet cabinet)
        {
            _cabinet = cabinet;
        }

        /// <summary>
        /// Dodanie poziomej przegrody szafki
        /// </summary>
        /// <param name="number">Ilosc dodawanych przegrod</param>
        /// <param name="barrier">numer rzedu do wstawienia przegrody - domyslnie 0</param>
        /// <param name="back">wielkosc odsuniecia od przodu wstawianej przegrody</param>
        /// <param name="height">lista wysokosci przestrzeni pomiedzy polkami liczac od dolu</param>
        public void AddBarrier(int number, int barrier = 0, int back=0, List<int> height = default(List<int>))
        {
            int tempHeight;
            
            int tempDepth = _cabinet.Depth - back;

            int tempEy;

            int tempWidth;

            if (_cabinet.VerticalBarrier.Count>0)
            {
                if (barrier == 0)
                {
                    tempWidth = _cabinet.VerticalBarrier[barrier].Ex- _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Leftside).EWidth;
                }
                else if (barrier == _cabinet.VerticalBarrier.Count)
                {
                    tempWidth = _cabinet.CabinetElements.First(x=>x.EName==EnumCabinetElement.Rightside).Ex- _cabinet.VerticalBarrier[barrier-1].Ex - _cabinet.VerticalBarrier[barrier-1].EWidth;
                }
                else
                {
                    tempWidth = _cabinet.VerticalBarrier[barrier].Ex - _cabinet.VerticalBarrier[barrier-1].Ex - _cabinet.VerticalBarrier[barrier-1].EWidth;
                }
            }
            else
            {
                tempWidth = _cabinet.Width - 2 * _cabinet.SizeElement;
            }
            
            
            //Wysokosc elementu
            tempHeight = _cabinet.SizeElement;



            //polozenie elementu na osi x
            //Jesli pierwsza lub jedyna przestrzen to punkt poczatkowy wiekszy o szerokosc boku
            //Jesli wybrano inna przestrzen niz pierwsza ( jedyna ) to punkt poczatkowy bariery + jej grubosc

            var tempEx = barrier==0 ? _cabinet.CabinetElements.First((x=>x.EName==EnumCabinetElement.Leftside)).EWidth : _cabinet.VerticalBarrier[barrier-1].Ex+ _cabinet.VerticalBarrier[barrier-1].EWidth;


            //polozenie elementu na osi y

            if (height==null)
            {
                tempEy = (_cabinet.Height - _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Bottom).EHeight - _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Top).EHeight
                          - number* _cabinet.SizeElement) / (number+1);

                for (var i = 0; i < number; i++)
                {
                    var element = new Element
                    {
                        EHeight = tempHeight,
                        EWidth = tempWidth,
                        EDepth = tempDepth,
                        Ex = tempEx,
                        Ey = _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Bottom).EHeight + tempEy * (i+1)  + _cabinet.SizeElement*i,
                        EName=EnumCabinetElement.HorizontalBarrier
                    };

                    _cabinet.HorizontalBarrier.Add(element);

                    
                }
            }
            else
            {
                
            }
            
            
        }
    }
}