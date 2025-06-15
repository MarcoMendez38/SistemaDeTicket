using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuevaAplicacion.Models.State
{
    public class InProgressState : TicketState
    {
        public override string StateName => "En Progreso";
        public override bool CanEdit => true;

        public override void Next(Ticket ticket)
        {
            ticket.SetState(new ResolvedState());
        }

        public override void Previous(Ticket ticket)
        {
            ticket.SetState(new NewState());
        }
    }
}