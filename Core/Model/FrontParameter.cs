namespace Core.Model
{
    public class FrontParameter
    {
        public int Number;              //ilosc frontow

        public SlotsModel Slots;

        public EnumFront EnumFront;

        public FrontParameter()
        {
            Number = 0;
            Slots = new SlotsModel
            {
                Top=3,
                Bottom=3,
                Left=3,
                Right=3,
                BetweenVertically=3,
                BetweenHorizontally=3,
                BetweenCabinet=2
            };
        }
    }
}