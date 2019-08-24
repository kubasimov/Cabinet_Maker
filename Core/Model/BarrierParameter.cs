using System.Collections.Generic;

namespace Core.Model
{
    public class BarrierParameter
    {
        public int Number;              //ilosc przegrod
        private List<int> Barrier;       //przegroda pionowa do ktorej wstawiany jest poziom
        public int Back;                //wielkosc odsuniecia od przodu
        public List<int> Height;        //wysokosci poziomow

        public BarrierParameter()
        {
            Number = 0;
            Barrier = new List<int>();
            Back = 0;
            //Height = new List<int>();
        }

        public void AddBarrier(int item)
        {
            Barrier.Add(item);
        }

        public List<int> GetBarrier()
        {
            return Barrier;
        }
    }
}