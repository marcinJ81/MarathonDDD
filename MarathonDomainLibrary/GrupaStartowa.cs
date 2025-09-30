using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonDomainLibrary
{
    public class GrupaStartowa
    {
        public int Id { get; private set; }
        public int IdDystans { get; private set; }
        public string Nazwa { get; private set; }
        public List<Zawodnik> Zawodnicy { get; private set; } = new List<Zawodnik>();
        public int IloscMiejscDostepnych { get; private set; }
        public int IloscWszystkichMiejsc { get; private set; }
        public bool Zamknieta { get; private set; }
        public GrupaStartowa(int id, int idDystans, string nazwa, int iloscMiejsc, bool zamknieta)
        {
            Id = id;
            IdDystans = idDystans;
            Nazwa = nazwa ?? throw new ArgumentNullException(nameof(nazwa));
            IloscWszystkichMiejsc = iloscMiejsc;
            IloscMiejscDostepnych = iloscMiejsc;
            Zamknieta = zamknieta;
        }

        public void UsunZawodnika(Zawodnik zawodnik)
        {
            if (zawodnik == null)
                throw new ArgumentNullException(nameof(zawodnik));
            if (!Zawodnicy.Contains(zawodnik))
                throw new DomainException("Zawodnik nie należy do tej grupy startowej");
            if (zawodnik.IdDystans != this.IdDystans)
            { 
                throw new DomainException("Zawodnik należy do innego dystansu");
            }
            Zawodnicy.Remove(zawodnik);
            IloscMiejscDostepnych++;
        }
        public void DodajZawodnika(Zawodnik zawodnik)
        {
            if (zawodnik == null)
                throw new ArgumentNullException(nameof(zawodnik));
            if (Zamknieta)
                throw new DomainException("Grupa startowa jest zamknięta");
            if ((IloscMiejscDostepnych + IloscWszystkichMiejsc) == IloscWszystkichMiejsc )
                throw new DomainException("Brak miejsc w grupie startowej");
            if (Zawodnicy.Contains(zawodnik))
                throw new DomainException("Zawodnik już należy do tej grupy startowej");
            Zawodnicy.Add(zawodnik);
            this.IloscMiejscDostepnych--;
        }
    }
}
