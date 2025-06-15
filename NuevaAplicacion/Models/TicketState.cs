using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuevaAplicacion.Models
{
    public abstract class TicketState
    {
        public abstract string StateName { get; }
        public abstract void Next(Ticket ticket);
        public abstract void Previous(Ticket ticket);
        public abstract bool CanEdit { get; }
    }
}
