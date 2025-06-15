using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuevaAplicacion.Models.State
{
    public class ClosedState : TicketState
    {
        public override string StateName => "Cerrado";
        public override bool CanEdit => false;

        public override void Next(Ticket ticket)
        {
            // No se puede avanzar desde Cerrado
        }

        public override void Previous(Ticket ticket)
        {
            ticket.SetState(new ResolvedState());
        }
    }
}