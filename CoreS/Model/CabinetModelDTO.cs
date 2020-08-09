using CoreS.Enum;
using System.Collections.Generic;

namespace CoreS.Model
{
    public class CabinetModelDTO
    {
        protected EnumCabinetType CabinetType;

        public string _name;
        public int _height;
        public int _width;
        public int _depth;
        public int _sizeElement;
        public int BackSize;
        public EnumBack Back;

        public List<ElementModel> CabinetElements = new List<ElementModel>();
        public List<ElementModel> HorizontalBarrier = new List<ElementModel>();
        public List<ElementModel> VerticalBarrier = new List<ElementModel>();
        public List<ElementModel> FrontList = new List<ElementModel>();
    }
}
