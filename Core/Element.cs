using System;

namespace Core
{
    public class Element
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

        public Element()
        {
            Guid = Guid.NewGuid();
        }
    }

    
}
     
