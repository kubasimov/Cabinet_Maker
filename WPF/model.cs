using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF
{
    public class Model
    {
        public double Wys;
        public double Szer;
        public double Gl;
        public double X;
        public double Y;
        public double Z;

        public Model(double a, double b, double c, double d, double e, double f)
        {
            Wys = a;
            Szer = b;
            Gl = c;
            X = d;
            Y = e;
            Z = f;
        }
    }

}
