using AutoMapper;
using brochureapi.DTOs;
using brochureapi.EFCoreInMemoryDbDemo;
using brochureapi.Models;
using brochureapi.NewFolder;
using brochureapi.services;
using Microsoft.EntityFrameworkCore;
using Nest;
using Xunit;

namespace brochureapi.tests
{
    public class BrochurePageServiceTest
    {
        private ApiContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApiContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApiContext(options);
        }
        private IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Brochure, BrochureDTO>().ReverseMap();
                cfg.CreateMap<Models.Page, PageDTO>().ReverseMap();
            });

            return config.CreateMapper();
        }

        [Fact]
        public void AddPageTest()
        {
            using var context = CreateContext();
             var mapper = CreateMapper();
            var service = new BrochurePageService(context,mapper);
            var brochureServ  = new repository.BrochureService(context, mapper);
            var brochure = new BrochureDTO(1, "test", DateOnly.FromDateTime(DateTime.Today));
            brochureServ.AddBrochure(brochure);
            
            //manually setting the primary key (Id) can confuse EF into thinking you're updating an existing entity, not inserting a new one.
            //dont setpage ids
            var page = new PageDTO { Name = "Page 1" };
            service.createPage(brochure.Id, page);
            var pages = service.GetAllPages(brochure.Id);
            Assert.Single(pages);
            Assert.Equal("Page 1", pages[0].Name);
        }

        [Fact]
        public void GetPageTest()
        {

            using var context = CreateContext();
            var mapper = CreateMapper();
            var service = new BrochurePageService(context, mapper);
            var brochureServ = new repository.BrochureService(context, mapper);
            var brochure = new BrochureDTO(1, "test", DateOnly.FromDateTime(DateTime.Today));
            brochureServ.AddBrochure(brochure);

            var page = new PageDTO { Name = "Page 1" };
            var page2 = new PageDTO { Name = "Page 2" };
            service.createPage(brochure.Id, page);
            service.createPage(brochure.Id, page2);
            var pages = service.GetAllPages(brochure.Id);
            Assert.Equal(2, pages.Count);
        }

        [Fact]
        public void UpdatePageTest()
        {
            using var context = CreateContext();
            var mapper = CreateMapper();
            var service = new BrochurePageService(context, mapper);
            var brochureServ = new repository.BrochureService(context, mapper);
            var brochure = new BrochureDTO(1, "test", DateOnly.FromDateTime(DateTime.Today));
            brochureServ.AddBrochure(brochure);
            var page = new PageDTO { Name = "Page 1" };
            service.createPage(brochure.Id, page);
            page.Name = "Updated Page 1";
            service.UpdatePage(1,1, page);
            var pages = service.GetAllPages(brochure.Id);
            Assert.Equal("Updated Page 1", pages[0].Name);
        }
        [Fact]
        public void DeletePageTest()
        {
            using var context = CreateContext();
            var mapper = CreateMapper();
            var service = new BrochurePageService(context, mapper);
            var brochureServ = new repository.BrochureService(context, mapper);
            var brochure = new BrochureDTO(1, "test", DateOnly.FromDateTime(DateTime.Today));
            brochureServ.AddBrochure(brochure);
            var page = new PageDTO { Name = "Page 1" };
            service.createPage(brochure.Id, page);
            service.deletePage(1, page.Id);
            var pages = service.GetAllPages(brochure.Id);
            Assert.Empty(pages);
        }
        [Fact]
        public void GetPageByID()
        {

            using var context = CreateContext();
            var mapper = CreateMapper();
            var service = new BrochurePageService(context, mapper);
            var brochureServ = new repository.BrochureService(context, mapper);
            var brochure = new BrochureDTO(1, "test", DateOnly.FromDateTime(DateTime.Today));
            brochureServ.AddBrochure(brochure);
            var page = new PageDTO { Name = "Page 1" };
            service.createPage(brochure.Id, page);
            var retrievedPage = service.GetPageById( 1,page.Id);
            Assert.Equal(page.Name, retrievedPage.Name);
        }


    }
}
