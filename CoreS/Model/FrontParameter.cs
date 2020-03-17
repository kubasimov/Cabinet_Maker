using CoreS.Enum;

namespace CoreS.Model
{
    public class FrontParameter
    {
        public int Number;

        public SlotsModel Slots;

        public EnumFront EnumFront;

        public FrontParameter()
        {
            Number = 0;
            Slots = new SlotsModel();
            
        }
    }
}