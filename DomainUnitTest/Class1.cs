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
            var zawodnicy = new List<Zawodnik>
            {
                new Zawodnik("Marcin", "Juranek", 1),
                new Zawodnik("Anna", "Kowalska", 1),
                new Zawodnik("Jan", "Nowak", 1),
                new Zawodnik("Katarzyna", "Wiśniewska", 1),
                new Zawodnik("Piotr", "Wójcik", 1),
                new Zawodnik("Magdalena", "Kamińska", 1),
                new Zawodnik("Tomasz", "Lewandowski", 1),
                new Zawodnik("Agnieszka", "Zielińska", 1),
                new Zawodnik("Krzysztof", "Szymański", 1),
                new Zawodnik("Monika", "Dąbrowska", 1),
                new Zawodnik("Michał", "Kozłowski", 1),
                new Zawodnik("Ewa", "Jankowska", 1),
                new Zawodnik("Paweł", "Mazur", 1),
                new Zawodnik("Joanna", "Krawczyk", 1),
                new Zawodnik("Andrzej", "Piotrowski", 1)
            };
            var grupaWyjsciowa = new GrupaStartowa(1, 1, "Grupa 1 Dystans 100 KM", 15, false);
            foreach (var zawodnik in zawodnicy)
            {
                grupaWyjsciowa.DodajZawodnika(zawodnik);
            }

            var zawodnicy2 = new List<Zawodnik>()
            {
                new Zawodnik("Robert", "Grabowski", 1),
                new Zawodnik("Barbara", "Pawlak", 1),
                new Zawodnik("Marek", "Michalski", 1),
                new Zawodnik("Dorota", "Król", 1),
                new Zawodnik("Grzegorz", "Wróbel", 1),
                new Zawodnik("Aleksandra", "Adamczyk", 1),
                new Zawodnik("Rafał", "Dudek", 1),
                new Zawodnik("Beata", "Nowakowska", 1),
                new Zawodnik("Dariusz", "Sikora", 1),
                new Zawodnik("Iwona", "Rutkowska", 1),
                new Zawodnik("Jacek", "Baran", 1),
                new Zawodnik("Marta", "Głowacka", 1),
                new Zawodnik("Sebastian", "Lis", 1),
                new Zawodnik("Natalia", "Kalinowska", 1)
            };
            var grupaDocelowa = new GrupaStartowa(2, 1, "Grupa 2 Dystans 100 KM", 15, false);
            foreach (var zawodnik in zawodnicy2)
            {
                grupaDocelowa.DodajZawodnika(zawodnik);
            }

            var zawodnikZmiana = zawodnicy.FirstOrDefault(x => x.Nazwisko == "Juranek" && x.Imie == "Marcin");

            var changeDate = DateTime.Now.AddDays(1);
            var zmiana = new ZmianaGrupyStartowej(
                changeDate,
                zawodnikZmiana,
                grupaWyjsciowa,
                grupaDocelowa
            );

            // Act
            zmiana.ZmienGrupe();

            // Assert
            grupaWyjsciowa.Zawodnicy.Should().NotContain(zawodnikZmiana);
            grupaWyjsciowa.IloscMiejscDostepnych.Should().Be(1);
            grupaDocelowa.Zawodnicy.Should().Contain(zawodnikZmiana);
            grupaDocelowa.IloscMiejscDostepnych.Should().Be(0);
 
        }

        //[Test]
        //public void ZmienGrupe_TerminPrzekroczony_RzucaWyjatek()
        //{
        //    // Arrange
        //    var zawodnik = new Zawodnik { CzyDokonalZmianyGrupy = false };
        //    var grupaWyjsciowa = new GrupaStartowa { IdDystans = 1 };
        //    var grupaDocelowa = new GrupaStartowa { IdDystans = 1 };

        //    var zmiana = new ZmianaGrupyStartowej(
        //        DateTime.Now.AddDays(-1), // termin w przeszłości
        //        zawodnik,
        //        grupaWyjsciowa,
        //        grupaDocelowa
        //    );

        //    // Act & Assert
        //    var action = () => zmiana.ZmienGrupe();
        //    action.Should().Throw<DomainException>()
        //        .WithMessage("Termin zmiany grupy przekroczony");
        //}

        //[Test]
        //public void ZmienGrupe_RoznyDystans_RzucaWyjatek()
        //{
        //    // Arrange
        //    var zawodnik = new Zawodnik { CzyDokonalZmianyGrupy = false };
        //    var grupaWyjsciowa = new GrupaStartowa { IdDystans = 1 };
        //    var grupaDocelowa = new GrupaStartowa { IdDystans = 2 }; // inny dystans

        //    var zmiana = new ZmianaGrupyStartowej(
        //        DateTime.Now.AddDays(1),
        //        zawodnik,
        //        grupaWyjsciowa,
        //        grupaDocelowa
        //    );

        //    // Act & Assert
        //    var action = () => zmiana.ZmienGrupe();
        //    action.Should().Throw<DomainException>()
        //        .WithMessage("Grupy mają różne dystanse");
        //}

        //[Test]
        //public void ZmienGrupe_ZawodnikJuzDokonalZmiany_RzucaWyjatek()
        //{
        //    // Arrange
        //    var zawodnik = new Zawodnik { CzyDokonalZmianyGrupy = true };
        //    var grupaWyjsciowa = new GrupaStartowa { IdDystans = 1 };
        //    var grupaDocelowa = new GrupaStartowa { IdDystans = 1 };

        //    var zmiana = new ZmianaGrupyStartowej(
        //        DateTime.Now.AddDays(1),
        //        zawodnik,
        //        grupaWyjsciowa,
        //        grupaDocelowa
        //    );

        //    // Act & Assert
        //    var action = () => zmiana.ZmienGrupe();
        //    action.Should().Throw<DomainException>()
        //        .WithMessage("Zawodnik dokonał już zmiany grupy");
        //}

        //[Test]
        //public void ZmienGrupe_BrakMiejsc_RzucaWyjatek()
        //{
        //    // Arrange
        //    var zawodnik = new Zawodnik { CzyDokonalZmianyGrupy = false };
        //    var grupaWyjsciowa = new GrupaStartowa { IdDystans = 1 };
        //    var grupaDocelowa = new GrupaStartowa
        //    {
        //        IdDystans = 1,
        //        IloscMiejsc = 2,
        //        Zawodnicy = new List<Zawodnik> { new Zawodnik(), new Zawodnik() } // pełna
        //    };

        //    var zmiana = new ZmianaGrupyStartowej(
        //        DateTime.Now.AddDays(1),
        //        zawodnik,
        //        grupaWyjsciowa,
        //        grupaDocelowa
        //    );

        //    // Act & Assert
        //    var action = () => zmiana.ZmienGrupe();
        //    action.Should().Throw<DomainException>()
        //        .WithMessage("Brak miejsc w grupie docelowej");
        //}

        //[TestCase("zawodnik")]
        //[TestCase("grupaWyjsciowa")]
        //[TestCase("grupaDocelowa")]
        //public void Konstruktor_NullArgument_RzucaWyjatek(string paramName)
        //{
        //    // Arrange & Act
        //    Action action = () => new ZmianaGrupyStartowej(
        //        DateTime.Now,
        //        paramName == "zawodnik" ? null : new Zawodnik(),
        //        paramName == "grupaWyjsciowa" ? null : new GrupaStartowa(),
        //        paramName == "grupaDocelowa" ? null : new GrupaStartowa()
        //    );

        //    // Assert
        //    action.Should().Throw<ArgumentNullException>()
        //        .WithParameterName(paramName);
        //}
    }
}
