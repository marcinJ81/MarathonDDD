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
        public int IloscMiejsc { get; private set; }
        public bool Zamknieta { get; private set; }
        public GrupaStartowa(int id, int idDystans, string nazwa, int iloscMiejsc, bool zamknieta)
        {
            Id = id;
            IdDystans = idDystans;
            Nazwa = nazwa ?? throw new ArgumentNullException(nameof(nazwa));
            IloscMiejsc = iloscMiejsc;
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
            IloscMiejsc--;
        }
        public void DodajZawodnika(Zawodnik zawodnik)
        {
            if (zawodnik == null)
                throw new ArgumentNullException(nameof(zawodnik));
            if (Zamknieta)
                throw new DomainException("Grupa startowa jest zamknięta");
            if (Zawodnicy.Count >= IloscMiejsc)
                throw new DomainException("Brak miejsc w grupie startowej");
            if (Zawodnicy.Contains(zawodnik))
                throw new DomainException("Zawodnik już należy do tej grupy startowej");
            Zawodnicy.Add(zawodnik);
            this.IloscMiejsc++;
        }
    }
}
