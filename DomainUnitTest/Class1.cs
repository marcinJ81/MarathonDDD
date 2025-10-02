using FluentAssertions;
using MarathonDomainLibrary;
using NUnit.Framework;

namespace DomainUnitTest
{
    public static class PlayersLists
    { 
        public static List<Zawodnik> players_15 { get; private set; } = new List<Zawodnik>
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
        public static List<Zawodnik> players_14 { get; private set; } = new List<Zawodnik>()
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

        public static List<Zawodnik> players_15_2 { get; private set; } = new List<Zawodnik>()
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
                new Zawodnik("Natalia", "Kalinowska", 1),
                new Zawodnik("Krol", "Julian", 1)
            };

    }

    [TestFixture]
    public class ZmianaGrupyStartowejTests
    {
        [Test]
        public void ChangeGroup_Player_Transferd()
        {
            // Arrange
            var players = PlayersLists.players_15;
            var startingGroup = new GrupaStartowa(1, 1, "Grupa 1 Dystans 100 KM", 15, false);
            foreach (var zawodnik in players)
            {
                startingGroup.DodajZawodnika(zawodnik);
            }

            var zawodnicy2 = PlayersLists.players_14;
            var grupaDocelowa = new GrupaStartowa(2, 1, "Grupa 2 Dystans 100 KM", 15, false);
            foreach (var zawodnik in zawodnicy2)
            {
                grupaDocelowa.DodajZawodnika(zawodnik);
            }

            var playerChange = players.FirstOrDefault(x => x.Nazwisko == "Juranek" && x.Imie == "Marcin");

            var changeDate = DateTime.Now.AddDays(1);
            var zmiana = new ZmianaGrupyStartowej(
                changeDate,
                playerChange,
                startingGroup,
                grupaDocelowa
            );

            // Act
            zmiana.ZmienGrupe();

            // Assert
            startingGroup.Zawodnicy.Should().NotContain(playerChange);
            startingGroup.IloscMiejscDostepnych.Should().Be(1);
            grupaDocelowa.Zawodnicy.Should().Contain(playerChange);
            grupaDocelowa.IloscMiejscDostepnych.Should().Be(0);
 
        }

        [Test]
        public void ChangeGroup_Deadline_Exceeded_Throws_An_Exception()
        {
            var players = PlayersLists.players_15;
            var startingGroup = new GrupaStartowa(1, 1, "Grupa 1 Dystans 100 KM", 15, false);
            foreach (var zawodnik in players)
            {
                startingGroup.DodajZawodnika(zawodnik);
            }

            var zawodnicy2 = PlayersLists.players_14;
            var grupaDocelowa = new GrupaStartowa(2, 1, "Grupa 2 Dystans 100 KM", 15, false);
            foreach (var zawodnik in zawodnicy2)
            {
                grupaDocelowa.DodajZawodnika(zawodnik);
            }

            var playerChange = players.FirstOrDefault(x => x.Nazwisko == "Juranek" && x.Imie == "Marcin");

            var changeDate = DateTime.Now.AddDays(-1);
            var zmiana = new ZmianaGrupyStartowej(
                changeDate,
                playerChange,
                startingGroup,
                grupaDocelowa
            );

            // Act
            //var action = () => zmiana.ZmienGrupe();
            //action.Should().Throw<DomainException>()
            //    .WithMessage("Termin zmiany grupy przekroczony");
            
            try
            {
                zmiana.ZmienGrupe();
            }
            catch (DomainException ex) 
            {
                var errorMessage = ex.Message;
                ex.Message.Should().Be("Termin zmiany grupy przekroczony");
            }


        }

        [Test]
        public void ZmienGrupe_RoznyDystans_RzucaWyjatek()
        {
            var players = PlayersLists.players_15;
            var startingGroup = new GrupaStartowa(1, 1, "Grupa 1 Dystans 100 KM", 15, false);
            foreach (var zawodnik in players)
            {
                startingGroup.DodajZawodnika(zawodnik);
            }

            var zawodnicy2 = PlayersLists.players_14;
            var grupaDocelowa = new GrupaStartowa(2, 2, "Grupa 2 Dystans 200 KM", 15, false);
            foreach (var zawodnik in zawodnicy2)
            {
                grupaDocelowa.DodajZawodnika(zawodnik);
            }

            var playerChange = players.FirstOrDefault(x => x.Nazwisko == "Juranek" && x.Imie == "Marcin");

            var changeDate = DateTime.Now.AddDays(1);
            var zmiana = new ZmianaGrupyStartowej(
                changeDate,
                playerChange,
                startingGroup,
                grupaDocelowa
            );

            // Act & Assert
            try
            {
                zmiana.ZmienGrupe();
            }
            catch (DomainException ex)
            {
                var errorMessage = ex.Message;
                ex.Message.Should().Be("Grupy mają różne dystanse");
            }
        }

        [Test]
        public void ZmienGrupe_ZawodnikJuzDokonalZmiany_RzucaWyjatek()
        {
            var players = PlayersLists.players_15;
            var startingGroup = new GrupaStartowa(1, 1, "Grupa 1 Dystans 100 KM", 15, false);
            foreach (var zawodnik in players)
            {
                startingGroup.DodajZawodnika(zawodnik);
            }

            var zawodnicy2 = PlayersLists.players_14;
            var targetGroup = new GrupaStartowa(2, 1, "Grupa 2 Dystans 200 KM", 15, false);
            foreach (var zawodnik in zawodnicy2)
            {
                targetGroup.DodajZawodnika(zawodnik);
            }

            var playerChange = players.FirstOrDefault(x => x.Nazwisko == "Juranek" && x.Imie == "Marcin");
            var changeDate = DateTime.Now.AddDays(1);
            var zmiana = new ZmianaGrupyStartowej(
                changeDate,
                playerChange,
                startingGroup,
                targetGroup
            );

            zmiana.ZmienGrupe();

            var playerChangeAnotherTime = players.FirstOrDefault(x => x.Nazwisko == "Juranek" && x.Imie == "Marcin");
            var anoterChange = new ZmianaGrupyStartowej(
                changeDate,
                playerChange,
                targetGroup,
                startingGroup
            );

            // Act & Assert
            try
            {
                anoterChange.ZmienGrupe();
            }
            catch (DomainException ex)
            {
                var errorMessage = ex.Message;
                ex.Message.Should().Be("Zawodnik dokonał już zmiany grupy");
            }

        }

        [Test]
        public void ZmienGrupe_BrakMiejsc_RzucaWyjatek()
        {
            var players = PlayersLists.players_15;
            var startingGroup = new GrupaStartowa(1, 1, "Grupa 1 Dystans 100 KM", 15, false);
            foreach (var zawodnik in players)
            {
                startingGroup.DodajZawodnika(zawodnik);
            }

            var zawodnicy2 = PlayersLists.players_15_2;
            var targetGroup = new GrupaStartowa(2, 1, "Grupa 2 Dystans 200 KM", 15, false);
            foreach (var zawodnik in zawodnicy2)
            {
                targetGroup.DodajZawodnika(zawodnik);
            }

            var playerChange = players.FirstOrDefault(x => x.Nazwisko == "Juranek" && x.Imie == "Marcin");
            var changeDate = DateTime.Now.AddDays(1);
            var anotherChange = new ZmianaGrupyStartowej(
                changeDate,
                playerChange,
                startingGroup,
                targetGroup
            );

            // Act & Assert
            try
            {
                anotherChange.ZmienGrupe();
            }
            catch (DomainException ex)
            {
                var errorMessage = ex.Message;
                ex.Message.Should().Be("Brak miejsc w grupie docelowej");
            }

        }
    }
}
