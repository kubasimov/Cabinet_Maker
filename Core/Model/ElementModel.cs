using System;

namespace Core.Model
{
    public class ElementModel : Result
    {
        public Guid Guid ;
        public EnumCabinetElement EName;    //Wewnetrzna nazwa elementu
        public string Description { get; set; }          //opis elementu dla uzytkownika
        public int EHeight { get; set; }
        public int EWidth { get; set; }
        public int EDepth { get; set; }
        public int Ex;
        public int Ey;
        public int Ez;
        public bool Horizontal;
        public string Material;


        public ElementModel()
        {
            Guid = Guid.NewGuid();
        }
    }

    
}
     
