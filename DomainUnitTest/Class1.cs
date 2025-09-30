using FluentAssertions;
using MarathonDomainLibrary;
using NUnit.Framework;

namespace DomainUnitTest
{
    

    [TestFixture]
    public class ZmianaGrupyStartowejTests
    {
        [Test]
        public void ZmienGrupe_PomyslnaZmiana_ZawodnikPrzeniesiony()
        {
            // Arrange
            var zawodnik = new Zawodnik("marcin", "Juranek", 1);
            var grupaWyjsciowa = new GrupaStartowa(1, 1, "Grupa 1 Dystans 100 KM", 10, false);
            grupaWyjsciowa.DodajZawodnika(zawodnik);
            var grupaDocelowa = new GrupaStartowa(2, 1, "Grupa 2 Dystans 100 KM", 10, false);

            var zmiana = new ZmianaGrupyStartowej(
                 new DateTime(2025,5,15),
                zawodnik,
                grupaWyjsciowa,
                grupaDocelowa
            );

            // Act
            zmiana.ZmienGrupe();

            // Assert
            grupaWyjsciowa.Zawodnicy.Should().NotContain(zawodnik);
            grupaDocelowa.Zawodnicy.Should().Contain(zawodnik);
            zawodnik.GrupaStartowa.Should().Be(grupaDocelowa);
        }

        [Test]
        public void ZmienGrupe_TerminPrzekroczony_RzucaWyjatek()
        {
            // Arrange
            var zawodnik = new Zawodnik { CzyDokonalZmianyGrupy = false };
            var grupaWyjsciowa = new GrupaStartowa { IdDystans = 1 };
            var grupaDocelowa = new GrupaStartowa { IdDystans = 1 };

            var zmiana = new ZmianaGrupyStartowej(
                DateTime.Now.AddDays(-1), // termin w przeszłości
                zawodnik,
                grupaWyjsciowa,
                grupaDocelowa
            );

            // Act & Assert
            var action = () => zmiana.ZmienGrupe();
            action.Should().Throw<DomainException>()
                .WithMessage("Termin zmiany grupy przekroczony");
        }

        [Test]
        public void ZmienGrupe_RoznyDystans_RzucaWyjatek()
        {
            // Arrange
            var zawodnik = new Zawodnik { CzyDokonalZmianyGrupy = false };
            var grupaWyjsciowa = new GrupaStartowa { IdDystans = 1 };
            var grupaDocelowa = new GrupaStartowa { IdDystans = 2 }; // inny dystans

            var zmiana = new ZmianaGrupyStartowej(
                DateTime.Now.AddDays(1),
                zawodnik,
                grupaWyjsciowa,
                grupaDocelowa
            );

            // Act & Assert
            var action = () => zmiana.ZmienGrupe();
            action.Should().Throw<DomainException>()
                .WithMessage("Grupy mają różne dystanse");
        }

        [Test]
        public void ZmienGrupe_ZawodnikJuzDokonalZmiany_RzucaWyjatek()
        {
            // Arrange
            var zawodnik = new Zawodnik { CzyDokonalZmianyGrupy = true };
            var grupaWyjsciowa = new GrupaStartowa { IdDystans = 1 };
            var grupaDocelowa = new GrupaStartowa { IdDystans = 1 };

            var zmiana = new ZmianaGrupyStartowej(
                DateTime.Now.AddDays(1),
                zawodnik,
                grupaWyjsciowa,
                grupaDocelowa
            );

            // Act & Assert
            var action = () => zmiana.ZmienGrupe();
            action.Should().Throw<DomainException>()
                .WithMessage("Zawodnik dokonał już zmiany grupy");
        }

        [Test]
        public void ZmienGrupe_BrakMiejsc_RzucaWyjatek()
        {
            // Arrange
            var zawodnik = new Zawodnik { CzyDokonalZmianyGrupy = false };
            var grupaWyjsciowa = new GrupaStartowa { IdDystans = 1 };
            var grupaDocelowa = new GrupaStartowa
            {
                IdDystans = 1,
                IloscMiejsc = 2,
                Zawodnicy = new List<Zawodnik> { new Zawodnik(), new Zawodnik() } // pełna
            };

            var zmiana = new ZmianaGrupyStartowej(
                DateTime.Now.AddDays(1),
                zawodnik,
                grupaWyjsciowa,
                grupaDocelowa
            );

            // Act & Assert
            var action = () => zmiana.ZmienGrupe();
            action.Should().Throw<DomainException>()
                .WithMessage("Brak miejsc w grupie docelowej");
        }

        [TestCase("zawodnik")]
        [TestCase("grupaWyjsciowa")]
        [TestCase("grupaDocelowa")]
        public void Konstruktor_NullArgument_RzucaWyjatek(string paramName)
        {
            // Arrange & Act
            Action action = () => new ZmianaGrupyStartowej(
                DateTime.Now,
                paramName == "zawodnik" ? null : new Zawodnik(),
                paramName == "grupaWyjsciowa" ? null : new GrupaStartowa(),
                paramName == "grupaDocelowa" ? null : new GrupaStartowa()
            );

            // Assert
            action.Should().Throw<ArgumentNullException>()
                .WithParameterName(paramName);
        }
    }
}
