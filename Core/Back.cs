using System;

namespace Core
{
    public class Back
    {
        
        public int SwitchDepthByBackType( int depth, EnumBack back, int backSize)
        {
            var tempBack=0;

            switch (back)
            {
                case EnumBack.Brak:
                    tempBack = depth;
                    break;

                case EnumBack.Nakladane:
                    tempBack = depth - backSize;
                    break;

                case EnumBack.Wpuszczane:
                    break;

                default:
                    tempBack = depth;
                    break;
            }

            return tempBack;
        }

        public int ValueAxisZbyBackTypeAndSize(Cabinet cabinet)
        {
            var value = 0;

            switch (cabinet.Back)
            {
                case EnumBack.Brak:
                    value = 0;
                    break;
                case EnumBack.Nakladane:
                    value = cabinet.BackSize;
                    break;
                case EnumBack.Wpuszczane:
                    value = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return value;
        }
    }
}