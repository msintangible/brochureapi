using brochureapi.DTOs;
using brochureapi.Models;
using brochureapi.repository;
using brochureapi.services;
using Microsoft.AspNetCore.Mvc;


namespace brochureapi.Controllers
{
    //  web api controller onASP.NET that handles crud operation for a brochure

    [ApiController] // tells asp.net that this a controller
    [Route("Brochure/{brouchureId}/pages")] // sets base url to brochure
    public class BrochurePageController : ControllerBase
    {

        private readonly ILogger<BrochurePageController> _logger;
        private readonly IBrochurePageService _service;

        // dependency injection to bring in an  ILogger for logging and  an ibrochurerepo to access the data 
        public BrochurePageController(IBrochurePageService service, ILogger<BrochurePageController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult CreatePage(int brochureId, [FromBody] PageDTO page)
        {
            if (page == null)
            {
                return BadRequest("Page data is required.");
            }
            // Add page to repository
            _service.createPage(brochureId, page);
            // Return HTTP 201 (Created) and the new brochure details
            return CreatedAtAction(nameof(GetPage), new { brochureId = brochureId, id = page.Id }, page);

        }

        [HttpGet("{id}")]
        public ActionResult<PageDTO> GetPage(int brochureId, int id)
        {
            var page = _service.GetPageById(brochureId, id);
            return Ok(page);
        }

        //once the controller class is called a logger is created to log errors and repo is created to acess the brochure data
        [HttpGet]
        public ActionResult<IEnumerable<PageDTO>> GetPages(int id)
        {
            var pages = _service.GetAllPages(id);

            return Ok(pages);
        }

        [HttpPut]
        public ActionResult ControllerUpdatePage(int brouchureId,int id,PageDTO page) {
            page.Id = id;
             _service.UpdatePage(brouchureId,id, page);
            return Ok(page);
        }


        [HttpDelete]
        public ActionResult DeletePage(int brouchureId, int id)
        {
             _service.deletePage(brouchureId, id);
            return NoContent();
        }


    }
}
