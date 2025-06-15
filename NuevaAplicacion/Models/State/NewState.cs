using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuevaAplicacion.Models.State
{
    public class NewState : TicketState
    {
        public override string StateName => "Nuevo";
        public override bool CanEdit => true;

        public override void Next(Ticket ticket)
        {
            ticket.SetState(new InProgressState());
        }

        public override void Previous(Ticket ticket)
        {
            // No se puede retroceder desde Nuevo
        }
    }
}