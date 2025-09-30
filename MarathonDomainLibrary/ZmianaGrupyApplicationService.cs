namespace MarathonDomainLibrary
{
    public class OperationResult
    {
        public bool Success { get; }
        public List<DomainException> Errors { get; }

        // Constructor and methods
    }
    public class ZmianaGrupyCommand
    {
        public int ZawodnikId { get; set; }
        public int GrupaWyjsciowaId { get; set; }
        public int GrupaDocelowaId { get; set; }
        public DateTime TerminGrupy { get; set; }
    }

    // Serwis aplikacji 
    public class ZmianaGrupyApplicationService
    {
        private readonly IGrupaRepository _grupaRepo;
        private readonly IZawodnikRepository _zawodnikRepo;
       
        public ZmianaGrupyApplicationService(
        IGrupaRepository grupaRepo,
        IZawodnikRepository zawodnikRepo)
        {
            _grupaRepo = grupaRepo;
            _zawodnikRepo = zawodnikRepo;
        }

        public void ZmienGrupe(ZmianaGrupyCommand command)
        {
            // 1. Pobierz dane
            var zawodnik = _zawodnikRepo.GetById(command.ZawodnikId);
            var grupaWyjsciowa = _grupaRepo.GetById(command.GrupaWyjsciowaId);
            var grupaDocelowa = _grupaRepo.GetById(command.GrupaDocelowaId);

            // 2. Utwórz agregat domenowy
            var zmiana = new ZmianaGrupyStartowej(
                command.TerminGrupy,
                zawodnik,
                grupaWyjsciowa,
                grupaDocelowa
            );
            // 3. Wykonaj operację domenową
            try
            {
                zmiana.ZmienGrupe(); 
            }
            catch (Exception ex)
            {
                //save to log
                throw;
            }

            // 4. Zapisz zmiany
            _zawodnikRepo.Save(zawodnik);
            _grupaRepo.Save(grupaWyjsciowa);
            _grupaRepo.Save(grupaDocelowa);
        }
    }
}
