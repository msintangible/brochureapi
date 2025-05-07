using System;
using System.Collections.Generic;
using System.Linq;
using brochureapi.Models;
using brochureapi.repository;
using Microsoft.AspNetCore.Mvc;// controller and roting supporter
using Microsoft.Extensions.Logging;

namespace brochureapi.Controllers
{

    // web api controller  onASP.NET that handles crud operation for a brochure
    [ApiController] // tells asp.net that this a controller
    [Route("[controller]")] // sets base url to brochure
    public class BrochureController: ControllerBase // api only controller
    {

        
        private readonly ILogger<BrochureController> _logger;
        private readonly IBrochureRepository _repository;
       
        // depenndcy injection to bring in an  ILogger for logging and  an ibrochurerepo to access the data 
        public BrochureController(IBrochureRepository repository, ILogger<BrochureController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        //once the controller class is called a logger is created to log errors and repo is created to acess the brochure data
       

        [HttpPost] // web request sumbits an enity (brochure) 
       //action result is a return type  that allwos 200 ok 404 and 201 or return a data object
        public ActionResult Create(Brochure brochure)
        {
            // Validate if brochure is not null
            if (brochure == null)
            {
                return BadRequest("Brochure data is required.");
            }

            // Add brochure to repository
            _repository.AddBrochure(brochure);

            // Return HTTP 201 (Created) and the new brochure details
            return CreatedAtAction(nameof(GetById), new { id = brochure.Id }, brochure);
        }
        [HttpPut("{id}")] //the put method replaces a all current represantions of target resource
        public ActionResult Update(int id,Brochure brochure) {
            if (brochure == null ) {
                return BadRequest("Brochure data is required.");
            }
            brochure.Id = id; //sets id from the urs so the id you want to update and  the data u would to update
            _repository.UpdateBrochure(brochure); //update the brochure once it get the brochure
           

            return Ok(brochure); // HTTP 204 - update success, no content returned
        }

        [HttpGet("{id}")] 
        // requests a represnatation of the specified resources  only used to request data
        // ActionResult<Brochure> is used to specify the typre of data that the method is expected to return
        public ActionResult<Brochure> GetById(int id)
        {
            var brochure = _repository.GetBrochureById(id); // the object  brrochure
            if (brochure == null) return NotFound(); // if not found eturns notfound
            return Ok(brochure); //else returns ok which returns succes code with json body reposnse
        }
    
        [HttpGet(Name = "GetBrochure")]
        // making   ssure it returns a list of brochures 
        public ActionResult<IEnumerable<Brochure>> Get()
        {
            var brochures = _repository.GetBrochures();

            return Ok(brochures);
        }
        [HttpGet("filter")]
        public ActionResult<IEnumerable<Brochure>> Getbyfilter(String InputName)
        {
            var brochures = _repository.GetBrochures(); // Use actual data source to get all the brochures
            if (brochures == null || !brochures.Any()) //checks if the  empty of null
            {
                return NotFound("No brochures found.");
            }
            // Filter brochures by name (case-insensitive contains match)
            //filters them by says in the list aceess each object name andif its not null and contains the inputNmae the user enter then its filterd
            var filtered = brochures
                .Where(b => b.Name != null && b.Name.Contains(InputName, StringComparison.OrdinalIgnoreCase))
                .ToList(); //to list is need becuase the result is  an <IEnumberableBrochure> to list forces the result into a filtered list

            if (!filtered.Any())//if the filter is empty no brochures are founds ur meet a not found reposnse
            {
                return NotFound($"No brochures found containing '{InputName}'.");
            }

            return Ok(filtered);
        }
        [HttpDelete]
        public ActionResult DeleteBrochure(int id)
        {
            var brochure = _repository.GetBrochureById(id);
            if (brochure == null) return NotFound($"Brochure with ID {id} not found.");

            _repository.DeleteBrochure(id);
            return NoContent(); ;
        }



    }
}
