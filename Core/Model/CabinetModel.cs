using System.Collections.Generic;
using Core.Factory;
using NLog;

namespace Core.Model
{
    public class CabinetModel
    {
        protected EnumCabinetType CabinetType;

        public string Name;
        public int Height;
        public int Width;
        public int Depth;
        public int SizeElement;
        public int BackSize;
        public EnumBack Back;

        public List<ElementModel> CabinetElements;
        public List<ElementModel> HorizontalBarrier;
        public List<ElementModel> VerticalBarrier;
        protected BarrierParameter HorizontalBarrierParameter;
        protected BarrierParameter VerticalBarrierParameter;
        public List<ElementModel> FrontList;
        protected readonly Back SwitchBack = new Back();

        
        protected HorizontalBarrierFactory HorizontalBarrierFactory;
        protected VerticalBarrierFactory VerticalBarrierFactory;
        protected Front Front;

        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    }
}