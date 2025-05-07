using System.Collections.Generic;
using brochureapi.Models;

namespace brochureapi.repository
{
    public interface IBrochureRepository
    {
        public List<Brochure> GetBrochures();
        public void AddBrochure(Brochure brochure);
        public Brochure GetBrochureById(int id);
        public void DeleteBrochure(int id);
        public void UpdateBrochure(Brochure brochure);
        public List<Brochure> GetByFilter( string id); 
    }
}
