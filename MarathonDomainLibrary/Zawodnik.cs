using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonDomainLibrary
{
    public class Zawodnik
    {
        public int Id { get; private set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int IdDystans { get; private set; }
        public GrupaStartowa GrupaStartowa { get; private set; }
        public bool CzyDokonalZmianyGrupy { get; private set; }
        public NumerStartowy NumerStartowy { get; private set; }
        public Zawodnik(int id, string imie, string nazwisko, int idDystans)
        {
            Id = id;
            Imie = imie ?? throw new ArgumentNullException(nameof(imie));
            Nazwisko = nazwisko ?? throw new ArgumentNullException(nameof(nazwisko));
            IdDystans = idDystans;
            CzyDokonalZmianyGrupy = false;
        }

        public void ZmienGrupe(GrupaStartowa nowaGrupa)
        {
            GrupaStartowa = nowaGrupa ?? throw new ArgumentNullException(nameof(nowaGrupa));
            CzyDokonalZmianyGrupy = true;
        }
    }

    public class NumerStartowy
    {
        public int Id { get; private set; }
        public string Numer { get; private set; }
        public NumerStartowy(int id, string numer)
        {
            Id = id;
            Numer = numer ?? throw new ArgumentNullException(nameof(numer));
        }
    }
}
