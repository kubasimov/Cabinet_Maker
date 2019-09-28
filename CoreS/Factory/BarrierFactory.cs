using System.Collections.Generic;
using CoreS.Model;

namespace CoreS.Factory
{
    public abstract class BarrierFactory
    {
        protected Cabinet _cabinet;
        protected List<ElementModel> elements;
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