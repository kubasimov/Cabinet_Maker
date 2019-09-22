using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Export
{
    public interface IImport
    {
        Cabinet Import(string path);
    }
}
