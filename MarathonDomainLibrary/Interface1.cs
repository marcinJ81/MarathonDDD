using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonDomainLibrary
{
    public interface IRepositorty<T>
    {
        bool Save(T model);
        T GetById(int id);
        bool Update(T model, int id);
        bool Delete(int id);
    }
    public interface IGrupaRepository : IRepositorty<GrupaStartowa>
    {
    }

    public interface IZawodnikRepository : IRepositorty<Zawodnik>
    {
    }
}
