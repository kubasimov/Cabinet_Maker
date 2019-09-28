using System;

namespace CoreS.Enum
{
    [Flags]
    public enum EnumFront
    {
        Nakladany = 0,
        Wpuszczany = 1,
        Poziomo = 2,
        Pionowo = 4,
        Szuflada = 8

    }
}