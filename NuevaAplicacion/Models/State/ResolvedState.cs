using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuevaAplicacion.Models.State
{
    public class ResolvedState : TicketState
    {
        public override string StateName => "Resuelto";
        public override bool CanEdit => false;

        public override void Next(Ticket ticket)
        {
            ticket.SetState(new ClosedState());
        }

        public override void Previous(Ticket ticket)
        {
            ticket.SetState(new InProgressState());
        }
    }
}