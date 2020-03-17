using System;
using System.Collections.Generic;
using System.Text;

namespace CoreS.Config.ConfigModel
{
    public class SlotsConfigModel
    {
        public SlotsConfigModel()
        {
            Top = 3;
            Bottom = 3;
            Left = 3;
            Right = 3;
            BetweenVertically = 3;
            BetweenCabinet = 2;
            BetweenHorizontally=3;
        }

        public int Top { get; }
        public int Bottom { get; }
        public int Left { get; }
        public int Right { get; }
        public int BetweenVertically { get; }
        public int BetweenCabinet { get; }
        public int BetweenHorizontally { get; }
    }
}
