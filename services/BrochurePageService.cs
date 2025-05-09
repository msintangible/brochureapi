using AutoMapper;
using brochureapi.DTOs;
using brochureapi.EFCoreInMemoryDbDemo;
using brochureapi.Models;

namespace brochureapi.services
{
    public class BrochurePageService : IBrochurePageService
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public BrochurePageService(ApiContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void createPage(int brochureId, PageDTO addPage)
        {
            var brochure = _context.Brochures.Find(brochureId);
            if (brochure != null)
            {
                var page = _mapper.Map<Page>(addPage);
                page.Brochure = brochure;
                brochure.Pages.Add(page);
                _context.SaveChanges();
            }
        }

        public PageDTO GetPageById(int brochureId, int pageId)
        {
            var brochure = _context.Brochures.Find(brochureId);
            if (brochure == null) return null;

            var pages = _context.Pages.FirstOrDefault(p => p.Id == pageId && p.BrochureId == brochureId);
            return _mapper.Map<PageDTO>(pages);
        }

        public PageDTO? UpdatePage(int id, int pageId, PageDTO page)
        {
            var brochure = _context.Brochures.Find(id);
            if (brochure == null) return null;

            var existingPage = _context.Pages.FirstOrDefault(p => p.Id == pageId && p.BrochureId == id);
            var existingPageDTO = _mapper.Map<PageDTO>(existingPage);
            if (existingPage != null)
            {
                _context.Entry(existingPage).CurrentValues.SetValues(page);
                _context.SaveChanges();
                return existingPageDTO;
            }
            return null;
        }
        public void deletePage(int brochureId, int pageId)
        {
            var brochure = _context.Brochures.Find(brochureId);
            if (brochure != null)
            {
                var page = _context.Pages.Find(pageId);
                if (page != null)
                {
                    _context.Pages.Remove(page);
                    _context.SaveChanges();
                }

            }
        }
        
        public List<PageDTO> GetAllPages(int Id)
        {
            //Acces the pages set and where the pages id match the id of the brochure return all the pages
            var pages =  _context.Pages.Where(p => p.BrochureId == Id).ToList();
            return _mapper.Map<List<PageDTO>>(pages);
        }

    

    }
    
   
}
