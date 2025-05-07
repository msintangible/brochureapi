using brochureapi.EFCoreInMemoryDbDemo;
using brochureapi.Models;
using Microsoft.EntityFrameworkCore;
using Xunit.Sdk;

namespace brochureapi.repository
{
    public class BrochureRepository  : IBrochureRepository
    {
        private readonly ApiContext _context;

        public BrochureRepository(ApiContext context)
        {
            _context = context;
        }
        public List<Brochure> GetBrochures()
        {
            return _context.Brochures
                .Include(b => b.Pages) // This loads related pages
                .ToList();
        }
        public void AddBrochure(Brochure brochure)
        {
            
            _context.Brochures.Add(brochure);
            _context.SaveChanges(); // Commits the transaction to the in-memory database
        }
        public Brochure GetBrochureById(int id)
        {
            return _context.Brochures
                .Include(b => b.Pages)
                .FirstOrDefault(b => b.Id == id);
        }

        public void UpdateBrochure(Brochure brochure) {

            _context.Brochures.Update(brochure);
            _context.SaveChanges(); // Commits the transaction to the in-memory database
        }
        public void DeleteBrochure(int id)
        {
            var brochure = _context.Brochures.Find(id);
            if (brochure != null)
            {
                _context.Brochures.Remove(brochure);
                _context.SaveChanges(); // Commits the delete
            }
        }


        public List<Brochure> GetByFilter(String  input)
        {
            return _context.Brochures
                .Where(b => b.Name.Contains(input, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Page> GetAllPages(int id) {
            //Acces the pages set and where the pages id match the id of the brochure return all the pages
            return _context.Pages.Where(p => p.BrochureId == id).ToList();
        }

        public void AddPage(int id,Page page) {

            _context.Brochures.Find(id);
            // Set the foreign key on the page
            page.BrochureId = id;
            _context.Pages.Add(page);
            _context.SaveChanges();
        }

    }
}
