using NuevaAplicacion.Iterators;
using NuevaAplicacion.Models;


namespace NuevaAplicacion.Controllers
{
    public class TicketController
    {
        private List<Ticket> _tickets;
        private TicketIterator _iterator;
        private int _nextId = 1;

        public TicketController()
        {
            _tickets = new List<Ticket>();
            _iterator = new TicketIterator(_tickets);
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            CreateTicket("Error en login", "Los usuarios no pueden iniciar sesión", "Juan Pérez", "alta");
            CreateTicket("Impresora no funciona", "La impresora del piso 2 no imprime", "María González", "media");
            CreateTicket("Actualización de software", "Actualizar Office en todas las PCs", "Carlos López", "baja");
        }

        public void CreateTicket(string title, string description, string assignedTo, string priority)
        {
            var ticket = new Ticket
            {
                Id = _nextId++,
                Title = title,
                Description = description,
                AssignedTo = assignedTo,
                CreatedBy = "Admin",
                Priority = PriorityFlyweight.GetPriority(priority)
            };

            _tickets.Add(ticket);
            _iterator = new TicketIterator(_tickets);
        }

        public void UpdateTicket(int id, string title, string description, string assignedTo, string priority)
        {
            var ticket = _tickets.FirstOrDefault(t => t.Id == id);
            if (ticket != null)
            {
                ticket.UpdateTicket(title, description, assignedTo, PriorityFlyweight.GetPriority(priority));
            }
        }

        public void AdvanceTicketState(int id)
        {
            var ticket = _tickets.FirstOrDefault(t => t.Id == id);
            ticket?.NextState();
        }

        public void RegressTicketState(int id)
        {
            var ticket = _tickets.FirstOrDefault(t => t.Id == id);
            ticket?.PreviousState();
        }

        public List<Ticket> GetAllTickets() => _tickets;
        public Ticket GetCurrentTicket() => _iterator.Current();
        public Ticket NextTicket() => _iterator.Next();
        public Ticket PreviousTicket() => _iterator.Previous();
        public bool HasNextTicket() => _iterator.HasNext();
        public bool HasPreviousTicket() => _iterator.HasPrevious();
        public void ResetIterator() => _iterator.Reset();
    }
}