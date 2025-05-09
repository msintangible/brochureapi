using AutoMapper;
using brochureapi.DTOs;
using brochureapi.EFCoreInMemoryDbDemo;
using brochureapi.Models;
using brochureapi.NewFolder;
using brochureapi.services;
using Microsoft.EntityFrameworkCore;
using Xunit.Sdk;

namespace brochureapi.repository
{
    public class BrochureService  : IBrochureService
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;
        public BrochureService(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<BrochureDTO> GetBrochures()
        {
            var brochures = _context.Brochures.Include(b => b.Pages).ToList();
            return _mapper.Map<List<BrochureDTO>>(brochures);
        }
        public void AddBrochure(BrochureDTO brochure)
        {
            _context.Brochures.Add(_mapper.Map<Brochure>(brochure)); // Corrected parameter from 'BrochureDTO' to 'brochure'
            _context.SaveChanges(); // Commits the transaction to the in-memory database
        }
        public BrochureDTO GetBrochureById(int id)
        {
            var brochure = _context.Brochures
                .Include(b => b.Pages)
                .FirstOrDefault(b => b.Id == id);
            return _mapper.Map<BrochureDTO>(brochure);
        }

        public void UpdateBrochure(BrochureDTO brochure) {

            var existingBrochure = _context.Brochures.Find(brochure.Id);
            if (existingBrochure != null)
            {
                // Update properties manually or use mapper
                _mapper.Map(brochure, existingBrochure); // this updates the tracked entity

                _context.SaveChanges(); // Only one tracked entity
            }
            
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
        


        public List<BrochureDTO> GetByFilter(String  input)
        {
            var brochures = _context.Brochures
                .Where(b => b.Name.Contains(input, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return _mapper.Map<List<BrochureDTO>>(brochures);
        }

        public List<PageDTO> GetAllPages(int id) {
            //Acces the pages set and where the pages id match the id of the brochure return all the pages
            var pages = _context.Pages
       .Where(p => p.BrochureId == id)
       .ToList();

            return _mapper.Map<List<PageDTO>>(pages);
        }
         
           public void  AddPage(int id, PageDTO page)
        {
            // Find the brochure by ID
            var brochure = _context.Brochures.Find(id);
            if (brochure == null)
            {
                throw new ArgumentException($"Brochure with ID {id} not found.");
            }

            // Map the PageDTO to a Page model
            var pageEntity = _mapper.Map<Page>(page);

            // Set the foreign key on the page entity
            pageEntity.BrochureId = id;

            // Add the page entity to the context
            _context.Pages.Add(pageEntity);

            // Save changes to the database
            _context.SaveChanges();
        }

    }
}
