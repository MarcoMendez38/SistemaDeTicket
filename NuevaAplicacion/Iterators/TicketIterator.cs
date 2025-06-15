using NuevaAplicacion.Models;
using NuevaAplicacion.Models.Interfaces;


namespace NuevaAplicacion.Iterators
{
    public class TicketIterator : IIterator<Ticket>
    {
        private readonly List<Ticket> _tickets;
        private int _currentIndex;

        public TicketIterator(List<Ticket> tickets)
        {
            _tickets = tickets;
            _currentIndex = 0;
        }

        public bool HasNext() => _currentIndex < _tickets.Count - 1;
        public bool HasPrevious() => _currentIndex > 0;

        public Ticket Next()
        {
            if (HasNext())
            {
                _currentIndex++;
            }
            return Current();
        }

        public Ticket Previous()
        {
            if (HasPrevious())
            {
                _currentIndex--;
            }
            return Current();
        }

        public Ticket Current() => _tickets.Count > 0 && _currentIndex < _tickets.Count ? _tickets[_currentIndex] : null;

        public void Reset() => _currentIndex = 0;
    }
}