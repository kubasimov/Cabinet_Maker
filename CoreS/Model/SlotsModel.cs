using Config;

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
        }

        public SlotsModel(IConfig conf)
        {
            Top = conf.SlotsTop();
            Bottom = conf.SlotsBottom();
            Left = conf.SlotsLeft();
            Right = conf.SlotsRight();
            BetweenVertically = conf.SlotsBetweenVertically();
            BetweenHorizontally= conf.SlotsBetweenHorizontally();
            BetweenCabinet = conf.SlotsBetweenCabinet();
        }
    }


}