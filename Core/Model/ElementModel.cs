using System;

namespace Core.Model
{
    public class ElementModel : Result
    {
        public Guid Guid;
        public EnumCabinetElement EName;    //Wewnetrzna nazwa elementu
        public string Description;          //opis elementu dla uzytkownika
        public int EHeight;
        public int EWidth;
        public int EDepth;
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
     
