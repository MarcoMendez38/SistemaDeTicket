using NuevaAplicacion.Models.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuevaAplicacion.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ResolvedDate { get; set; }
        public string AssignedTo { get; set; }
        public string CreatedBy { get; set; }
        public PriorityFlyweight Priority { get; set; }
        private TicketState _state;
        public List<string> History { get; set; }

        public Ticket()
        {
            _state = new NewState();
            History = new List<string>();
            CreatedDate = DateTime.Now;
            AddToHistory($"Ticket creado en estado: {_state.StateName}");
        }

        public string CurrentState => _state.StateName;
        public bool CanEdit => _state.CanEdit;

        public void SetState(TicketState state)
        {
            _state = state;
            AddToHistory($"Estado cambiado a: {state.StateName}");
            if (state is ResolvedState || state is ClosedState)
            {
                ResolvedDate = DateTime.Now;
            }
        }

        public void NextState()
        {
            _state.Next(this);
        }

        public void PreviousState()
        {
            _state.Previous(this);
        }

        private void AddToHistory(string action)
        {
            History.Add($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {action}");
        }

        public void UpdateTicket(string title, string description, string assignedTo, PriorityFlyweight priority)
        {
            if (CanEdit)
            {
                Title = title;
                Description = description;
                AssignedTo = assignedTo;
                Priority = priority;
                AddToHistory("Ticket actualizado");
            }
        }
    }
}