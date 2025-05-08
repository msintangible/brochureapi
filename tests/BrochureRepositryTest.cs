using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using brochureapi.EFCoreInMemoryDbDemo;
using brochureapi.Models;
using brochureapi.repository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace brochureapi.tests
{
    //repo test needs to interact witha db 
    //using in memeory to test
    public class BrochureRepositryTest
    {
        //createcontext create a  new in memeory databasecontext 

        private ApiContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApiContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Creates a unique in-memory DB per test
                .Options;
            return new ApiContext(options);
        }
        [Fact] // Marks  a method as a unit test
        public void AddBrochureTest()
        {
            //Arrange
            //set up the DB
            //using implements IDisposable  which doees automatic disposes
            using var context = CreateContext();
            var repo = new BrochureRepository(context); //instace  of the repoclass that takes in a db
                                                        //act
                                                        //created a brochure object ot be passed into the addbrochure and call getbrochures to return the brochure
            var brochure = new Brochure(1, "test", DateOnly.FromDateTime(DateTime.Today));
            repo.AddBrochure(brochure);
            var brochures = repo.GetBrochures();

            //assert
            Assert.Single(brochures);//only one is added so a single object should be returned
            Assert.Equal("test", brochures[0].Name);//checking the first element in the lsit accessing the object and checking if the name attribute matches test
            Assert.Equal(1, brochures[0].Id);//same but iwth id ( the expected value, the actual value)


        }
        [Fact]
        public void GetBrochureTest()
        {
            //Arranage
            using var context = CreateContext();
            var repo = new BrochureRepository(context);
            //act
            var brochure1 = new Brochure(4, "Brochure One", DateOnly.FromDateTime(DateTime.Today));
            var brochure2 = new Brochure(3, "Brochure Two", DateOnly.FromDateTime(DateTime.Today.AddDays(1)));
            repo.AddBrochure(brochure1);
            repo.AddBrochure(brochure2);

            var result = repo.GetBrochures();
            Assert.NotNull(result); //checks if the object exists
            Assert.Equal(2, result.Count);
        }
        [Fact]
        public void DeleteBrocchureTest()
        {
            //Arrange
            using var context = CreateContext();
            var repo = new BrochureRepository(context);
            var brochure1 = new Brochure(33, "Brochure 33", DateOnly.FromDateTime(DateTime.Today));
            repo.AddBrochure(brochure1);
            //act 
            var result = repo.GetBrochures();
            Assert.NotNull(result);

            repo.DeleteBrochure(33);
            var deletedBrochure = repo.GetBrochureById(33);
            //Assert
            Assert.Null(deletedBrochure); /// checks if the objects doesnt exitst
        }
        [Fact]
        public void GetBrochureByIdTest()
        {
            // Arrange
            using var context = CreateContext();
            var repo = new BrochureRepository(context);
            var brochure = new Brochure(21, "Specific Brochure", DateOnly.FromDateTime(DateTime.Today));
            repo.AddBrochure(brochure);

            // Act
            var result = repo.GetBrochureById(21);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Specific Brochure", result.Name);
            Assert.Equal(21, result.Id);
        }

        [Fact]
        public void UpdateBrochureTest()
        {
            // Arrange
            // set up the db the repochure created a  object to test then  pass it into the repo methods to test them
            using var context = CreateContext();
            var repo = new BrochureRepository(context);
            var brochure = new Brochure(22, "Old Name", DateOnly.FromDateTime(DateTime.Today));
            repo.AddBrochure(brochure);

            // Act
            brochure.Name = "Updated Name";
            repo.UpdateBrochure(brochure);
            var updated = repo.GetBrochureById(22);

            // Assert
            Assert.Equal("Updated Name", updated.Name);
        }
        [Fact]
        public void DeleteBrochureTest()
        {
            //Arrange
            using var context = CreateContext();
            var repo = new BrochureRepository(context);
            var brochure = new Brochure(22, "Old Name", DateOnly.FromDateTime(DateTime.Today));
            repo.AddBrochure(brochure);
            
            Assert.NotNull(repo.GetBrochureById(22));
            // Act
            repo.DeleteBrochure(22);
            Assert.Null(repo.GetBrochureById(22));
        }

        [Fact]
        public void GetAllPagesFromBrochureTest() {
            //Arrange
            using var context = CreateContext();
            var repo = new BrochureRepository(context);
            var brochure = new Brochure(22, "Old Name", DateOnly.FromDateTime(DateTime.Today));
            repo.AddBrochure(brochure);

            var result = repo.GetAllPages(22);

            Assert.NotNull(result);
            result.Should().BeOfType<List<Page>>();

        }
        [Fact]
        public void AddGetPagesTest()
        {
            // Arrange
            using var context = CreateContext();
            var repo = new BrochureRepository(context);
            var brochure = new Brochure { Id = 1, Name = "Test Brochure" };
            repo.AddBrochure(brochure);

            // Add pages to brochure
            var page1 = new Page { Name = "Page 1" };
            var page2 = new Page { Name = "Page 2" };
            repo.AddPage(1, page1);
            repo.AddPage(1, page2);

            // Act
            var result = repo.GetAllPages(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.Name == "Page 1");
            Assert.Contains(result, p => p.Name == "Page 2");
        }
    }
}