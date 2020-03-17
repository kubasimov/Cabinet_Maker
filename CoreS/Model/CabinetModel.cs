using CoreS.Enum;
using CoreS.Factory;
using NLog;
using System.Collections.Generic;

namespace CoreS.Model
{
    public class CabinetModel
    {
        protected EnumCabinetType CabinetType;

        //TODO: tymczasowo zmienne publiczne do serializacji. Przygotować dodatkowy model do serializacji(zapisu cabinet)
        public string _name;
        public int _height;
        public int _width;
        public int _depth;
        public int _sizeElement;
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

        protected FrontFactory FrontFactory;
        protected FrontParameter FrontParameter;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    }
}