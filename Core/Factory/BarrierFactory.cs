using System.Collections.Generic;
using Core.Model;

namespace Core.Factory
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

        
        public abstract List<ElementModel> NewBarrier(BarrierParameter barrierParameter);
        
    }
}