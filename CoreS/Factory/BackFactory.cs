﻿using CoreS.Enum;
using System;

namespace CoreS.Factory
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

        public int ValueAxisZbyBackTypeAndSize(EnumBack enumBack,int backSize)
        {
            int value;
            switch (enumBack)
            {
                case EnumBack.Brak:
                    value = 0;
                    break;
                case EnumBack.Nakladane:
                    value = backSize;
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