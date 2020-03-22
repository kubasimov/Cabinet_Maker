using CoreS.Helpers;
using CoreS.Model;
using System.Collections.Generic;

namespace CoreS.Factory
{
    public abstract class BarrierFactory : MyLogger
    {
        protected Cabinet _cabinet;
        protected List<ElementModelDTO> elements;
        protected int Number;
        protected int tempHeight;
        protected int tempDepth;
        protected int tempWidth;
        protected int tempEy;
        protected int tempEx;
        protected int back;
        protected List<int> Height;
        protected List<int> TempHeight;
    }
}