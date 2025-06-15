using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuevaAplicacion.Models.Interfaces
{
    public interface IIterator<T>
    {
        bool HasNext();
        bool HasPrevious();
        T Next();
        T Previous();
        T Current();
        void Reset();
    }
}
