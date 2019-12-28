using System;

namespace CoreS.Enum
{
    [Flags]
    public enum EnumFront
    {
        None=0,
        Nakladany = 1,
        Wpuszczany = 2,
        Poziomo = 4,
        Pionowo = 8,
        Szuflada = 16

    }
}