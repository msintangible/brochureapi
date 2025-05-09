using brochureapi.DTOs;
using brochureapi.Models;

namespace brochureapi.services
{
    public interface IBrochurePageService
    {
        
        public void createPage( int brochureId,PageDTO page);  
        public PageDTO UpdatePage( int id, int pageId,PageDTO page);
        public void deletePage(int brochureId, int pageId);

        public PageDTO GetPageById(int brochureId, int pageId);

        public List<PageDTO> GetAllPages(int id);
    }
}
