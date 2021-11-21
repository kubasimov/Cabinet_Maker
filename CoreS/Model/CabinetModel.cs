using CoreS.Enum;
using CoreS.Factory;
using NLog;
using System.Collections.Generic;

namespace CoreS.Model
{
    public class CabinetModel
    {
        protected EnumCabinetType _cabinetType;

        //TODO: tymczasowo zmienne publiczne do serializacji. Przygotować dodatkowy model do serializacji(zapisu cabinet)
        public string _name;
        public int _height;
        public int _width;
        public int _depth;
        public int _sizeElement;
        public int _backSize;
        public EnumBack _enumBack;

        public List<ElementModel> CabinetElements = new List<ElementModel>();
        public List<ElementModel> HorizontalBarrier = new List<ElementModel>();
        public List<ElementModel> VerticalBarrier = new List<ElementModel>();
        public List<ElementModel> FrontList = new List<ElementModel>();

        protected readonly Back SwitchBack = new Back();

        
        protected HorizontalBarrierFactory HorizontalBarrierFactory;
        protected VerticalBarrierFactory VerticalBarrierFactory;
        protected FrontFactory FrontFactory;

        protected BarrierParameter HorizontalBarrierParameter;
        protected BarrierParameter VerticalBarrierParameter;
        protected FrontParameter FrontParameter;

        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    }
}