using System;

namespace Core
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