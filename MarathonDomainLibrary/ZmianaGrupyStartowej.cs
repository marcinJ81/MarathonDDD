using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonDomainLibrary
{
    public class ZmianaGrupyStartowej
    {
        private readonly DateTime _terminZmiany;
        private readonly Zawodnik _zawodnik;
        private readonly GrupaStartowa _grupaDocelowa;
        private readonly GrupaStartowa _grupaWyjsciowa;

        public ZmianaGrupyStartowej(
            DateTime terminZmiany,
            Zawodnik zawodnik,
            GrupaStartowa grupaWyjsciowa,
            GrupaStartowa grupaDocelowa)
        {
            _terminZmiany = terminZmiany;
            _zawodnik = zawodnik ?? throw new ArgumentNullException(nameof(zawodnik));
            _grupaWyjsciowa = grupaWyjsciowa ?? throw new ArgumentNullException(nameof(grupaWyjsciowa));
            _grupaDocelowa = grupaDocelowa ?? throw new ArgumentNullException(nameof(grupaDocelowa));
        }

        public void ZmienGrupe()
        {
            ValidateTermin();
            ValidateDystanse();
            ValidateZawodnik();
            ValidateIloscMiejsc();

            // Wykonaj zmianę na encjach
            _zawodnik.ZmienGrupe(_grupaDocelowa);
            _grupaWyjsciowa.UsunZawodnika(_zawodnik);
            _grupaDocelowa.DodajZawodnika(_zawodnik);

        }

        private void ValidateTermin()
        {
            if (_terminZmiany < DateTime.Now)
                throw new DomainException("Termin zmiany grupy przekroczony");
        }

        private void ValidateDystanse()
        {
            if (_grupaDocelowa.IdDystans != _grupaWyjsciowa.IdDystans)
                throw new DomainException("Grupy mają różne dystanse");
        }

        private void ValidateZawodnik()
        {
            if (_zawodnik.CzyDokonalZmianyGrupy)
                throw new DomainException("Zawodnik dokonał już zmiany grupy");
        }

        private void ValidateIloscMiejsc()
        {
            if (_grupaDocelowa.IloscMiejsc >= _grupaDocelowa.Zawodnicy.Count)
                throw new DomainException("Brak miejsc w grupie docelowej");
        }

      
    }
}
