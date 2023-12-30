using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDAO
    {
        IEnumerable<ISkiBrand> GetAllSkiBrands();
        IEnumerable<ISkiis> GetAllSkiis();

        ISkiBrand AddNewSkiBrand(ISkiBrand skiBrand);
        ISkiis AddNewSkiis(ISkiis skiis);

        void RemoveSkiBrand(int skiBrandId);
        void RemoveSkiis(int skiisId);

        void UpdateSkiBrand(ISkiBrand skiBrand);
        void UpdateSkiis(ISkiis skiis);
    }
}
