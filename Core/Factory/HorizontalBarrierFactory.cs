using System.Collections.Generic;
using System.Linq;
using Core.Model;

namespace Core.Factory
{
    public class HorizontalBarrierFactory:BarrierFactory
    {
        public HorizontalBarrierFactory(Cabinet cabinet)
        {
            _cabinet = cabinet;
            elements=new List<ElementModel>();
        }

        public List<ElementModel> AddBarrier(BarrierParameter barrierParameter)
        {
            //UWAGA - KAZDE NOWE DODAWANIE POZIOMOW NIWELUJE OSTATNIE RYSOWANIE - ZWIEKSZA ILOSC POZIOMOW O NOWE, ALE WYLICZA WSZYSTKO NA NOWO

            //ILOSC PRZEGROD POZIOMYCH DO WYLICZENIA
            //number = elements.Count() >= barrierParameter.Number ? barrierParameter.Number + elements.Count : barrierParameter.Number;
            number = barrierParameter.Number;
            //ODSUNIECIE OD PRZODU
            back = barrierParameter.Back;
            //WYSOKOSCI PRZESTRZENIE POMIEDZY
            //var height = barrierParameter.Height;
            //KTORY RZAD DO STAWIENIA LUB DOMYSLNIE PIERWSZY
            //int barrier = barrierParameter.Barrier.FirstOrDefault();

            //TABLICA NOWYCH PRZEGROD
            elements = new List<ElementModel>();

            //Wysokosc elementu
            tempHeight = _cabinet.SizeElement; ;
            //GLEBOKOSC NOWYCH PRZEGROD
            tempDepth = _cabinet.Depth - back;
            
            tempEy = (_cabinet.Height - _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Bottom).EHeight - _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Top).EHeight
                      - number * _cabinet.SizeElement) / (number + 1);

            //petla po wszystkich kolumnach
            for (int i = 0; i <= _cabinet.VerticalBarrier.Count(); i++)
            {
                //czy nie ma  informacji o wybranych kolumnach
                if (barrierParameter.GetBarrier()==null||barrierParameter.GetBarrier().Count==0)
                {
                    // brak informacji o kolumnach - wstawianie w każda
                    tempWidth = TempWidth(i);
                    tempEx = i == 0 ? _cabinet.CabinetElements.First((x => x.EName == EnumCabinetElement.Leftside)).EWidth : _cabinet.VerticalBarrier[i - 1].Ex + _cabinet.VerticalBarrier[i - 1].EWidth;

                    AddElement();
                }
                else
                {
                    //sa wybrane kolumny
                    if (barrierParameter.GetBarrier().Contains(i))
                    {
                        //wstawianie do danej kolumny - wybranej
                        tempWidth = TempWidth(i);
                        tempEx = i == 0 ? _cabinet.CabinetElements.First((x => x.EName == EnumCabinetElement.Leftside)).EWidth : _cabinet.VerticalBarrier[i - 1].Ex + _cabinet.VerticalBarrier[i - 1].EWidth;

                        AddElement();
                    }
                }
            }








            //if (_cabinet.VerticalBarrier.Count > 0)
            //{
            //    if (barrier == 0)
            //    {
            //        tempWidth = _cabinet.VerticalBarrier[barrier].Ex - _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Leftside).EWidth;
            //    }
            //    else if (barrier == _cabinet.VerticalBarrier.Count)
            //    {
            //        tempWidth = _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Rightside).Ex - _cabinet.VerticalBarrier[barrier - 1].Ex - _cabinet.VerticalBarrier[barrier - 1].EWidth;
            //    }
            //    else
            //    {
            //        tempWidth = _cabinet.VerticalBarrier[barrier].Ex - _cabinet.VerticalBarrier[barrier - 1].Ex - _cabinet.VerticalBarrier[barrier - 1].EWidth;
            //    }
            //}
            //else
            //{
            //    tempWidth = _cabinet.Width - 2 * _cabinet.SizeElement;
            //}

            ////polozenie elementu na osi x
            ////Jesli pierwsza lub jedyna przestrzen to punkt poczatkowy wiekszy o szerokosc boku
            ////Jesli wybrano inna przestrzen niz pierwsza ( jedyna ) to punkt poczatkowy bariery + jej grubosc

            //var tempEx = barrier == 0 ? _cabinet.CabinetElements.First((x => x.EName == EnumCabinetElement.Leftside)).EWidth : _cabinet.VerticalBarrier[barrier - 1].Ex + _cabinet.VerticalBarrier[barrier - 1].EWidth;


            ////polozenie elementu na osi y

            //if (!height.Any())
            //{
                

                
            //}
            //else
            //{

            //}

            return elements;
        }

        //wyliczenie szerokosc do danej kolumny
        private int TempWidth(int column)
        {
            int tempWidth;

            if (_cabinet.VerticalBarrier.Count > 0)
            {
                if (column == 0)
                {
                    tempWidth = _cabinet.VerticalBarrier[column].Ex - _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Leftside).EWidth;
                }
                else if (column == _cabinet.VerticalBarrier.Count)
                {
                    tempWidth = _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Rightside).Ex - _cabinet.VerticalBarrier[column - 1].Ex - _cabinet.VerticalBarrier[column - 1].EWidth;
                }
                else
                {
                    tempWidth = _cabinet.VerticalBarrier[column].Ex - _cabinet.VerticalBarrier[column - 1].Ex - _cabinet.VerticalBarrier[column - 1].EWidth;
                }
            }
            else
            {
                tempWidth = _cabinet.Width - 2 * _cabinet.SizeElement;
            }

            return tempWidth;
        }

        private void AddElement()
        {
            for (var i = 0; i < number; i++)
            {
                var element = new ElementModel
                {
                    EHeight = tempHeight,
                    EWidth = tempWidth,
                    EDepth = tempDepth,
                    Ex = tempEx,
                    Ey = _cabinet.CabinetElements.First(x => x.EName == EnumCabinetElement.Bottom).EHeight + tempEy * (i + 1) + _cabinet.SizeElement * i,
                    EName = EnumCabinetElement.HorizontalBarrier
                };

                elements.Add(element);


            }
        }
    }
}