using brochureapi.Models;
using brochureapi.repository;
using Microsoft.AspNetCore.Mvc;

namespace brochureapi.Controllers
{
  //  web api controller onASP.NET that handles crud operation for a brochure

    [ApiController] // tells asp.net that this a controller
    [Route("Brochure/{id}/Pages")] // sets base url to brochure
    public class BrochurePageController : ControllerBase
    {

        private readonly ILogger<BrochurePageController> _logger;
        private readonly IBrochureRepository _repository;

        // dependency injection to bring in an  ILogger for logging and  an ibrochurerepo to access the data 
        public BrochurePageController(IBrochureRepository repository, ILogger<BrochurePageController> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        //once the controller class is called a logger is created to log errors and repo is created to acess the brochure data
        [HttpGet]
        public ActionResult<IEnumerable<Page>> GetPages(int id)
        {
            var pages = _repository.GetAllPages(id);

            return Ok(pages);
        }
    }
}
