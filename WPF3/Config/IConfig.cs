namespace WPF3.Config
{
    public interface IConfig
    {
        public SlotsConfig Get()
        {
            return new SlotsConfig 
            {
                Top = 3,
                Bottom = 3,
                Left = 3,
                Right = 3,
                BetweenVertically = 3,
                BetweenHorizontally = 3,
                BetweenCabinet = 2
            };
        }
    }
}
