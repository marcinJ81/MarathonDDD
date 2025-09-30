using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonDomainLibrary
{
    // Statyczna klasa do zbierania zdarzeń
    public static class DomainEvents
    {
        private static List<IDomainEvent> _events = new();

        public static void Raise(IDomainEvent domainEvent)
        {
            _events.Add(domainEvent);
        }

        public static void ClearEvents()
        {
            _events.Clear();
        }

        public static IReadOnlyList<IDomainEvent> GetEvents()
        {
            return _events.AsReadOnly();
        }
    }

    // Interfejs bazowy
    public interface IDomainEvent
    {
        DateTime DataZdarzenia { get; }
    }
}
