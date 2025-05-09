using System.Collections.Generic;
using brochureapi.DTOs;
using brochureapi.Models;
using brochureapi.NewFolder;

namespace brochureapi.services
{
    public interface IBrochureService
    {
        public List<BrochureDTO> GetBrochures();
        public void AddBrochure(BrochureDTO brochure);
        public BrochureDTO GetBrochureById(int id);
        public void DeleteBrochure(int id);
        public void UpdateBrochure(BrochureDTO brochure);
        public List<BrochureDTO> GetByFilter( string id);

        public List<PageDTO> GetAllPages(int id);
    }
}
