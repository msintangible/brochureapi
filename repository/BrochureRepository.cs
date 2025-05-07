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
        public List<Brochure> GetBrochures() {
            
                return _context.Brochures.ToList();
            }
        public void AddBrochure(Brochure brochure)
        {
            
            _context.Brochures.Add(brochure);
            _context.SaveChanges(); // Commits the transaction to the in-memory database
        }
        public Brochure GetBrochureById(int id)
        {
            
            return _context.Brochures.Find(id);
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

    }
}
