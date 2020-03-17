using CoreS.Config;

namespace CoreS.Model
{
    public class SlotsModel
    {
        public int Top;
        public int Bottom;
        public int Left;
        public int Right;
        public int BetweenVertically;
        public int BetweenHorizontally;
        public int BetweenCabinet;

        public SlotsModel()
        {
            var t = Settings.Instance.GetSlots();
            Top = t.Top;
            Bottom = t.Bottom;
            Left = t.Left;
            Right = t.Right;
            BetweenVertically = t.BetweenVertically;
            BetweenHorizontally=t.BetweenHorizontally;
            BetweenCabinet = t.BetweenCabinet;
        }
    }


}