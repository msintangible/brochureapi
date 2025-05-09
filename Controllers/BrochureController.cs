using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using brochureapi.Models;
using brochureapi.NewFolder;
using brochureapi.services;
using Microsoft.AspNetCore.Mvc;// controller and roting supporter
using Microsoft.Extensions.Logging;
using Moq;

namespace brochureapi.Controllers
{

    // web api controller  onASP.NET that handles crud operation for a brochure
    [ApiController] // tells asp.net that this a controller
    [Route("[controller]")] // sets base url to brochure
    public class BrochureController: ControllerBase // api only controller
    {

        
        private readonly ILogger<BrochureController> _logger;
        private readonly IBrochureService _service;
        

        // dependency injection to bring in an  ILogger for logging and  an ibrochurerepo to access the data 
        public BrochureController(IBrochureService service, ILogger<BrochureController> logger)
        {
            _service = service;
            _logger = logger;
          
        }

       

        //once the controller class is called a logger is created to log errors and repo is created to acess the brochure data


        [HttpPost] // web request sumbits an enity (brochure) 
       //action result is a return type  that allwos 200 ok 404 and 201 or return a data object
        public ActionResult Create(BrochureDTO brochure)
        {
            // Validate if brochure is not null
            if (brochure == null)
            {
                return BadRequest("Brochure data is required.");
            }

            // Add brochure to repository
            _service.AddBrochure(brochure);

            // Return HTTP 201 (Created) and the new brochure details
            return CreatedAtAction(nameof(GetById), new { id = brochure.Id }, brochure);
        }
        [HttpPut("{id}")] //the put method replaces a all current represantions of target resource
        public ActionResult Update(int id,BrochureDTO brochure) {
            if (brochure == null ) {
                return BadRequest("Brochure data is required.");
            }
            brochure.Id = id; //sets id from the urs so the id you want to update and  the data u would to update
            _service.UpdateBrochure(brochure); //update the brochure once it get the brochure
           

            return Ok(brochure); // HTTP 204 - update success, no content returned
        }

        [HttpGet("{id}")] 
        // requests a represnatation of the specified resources  only used to request data
        // ActionResult<Brochure> is used to specify the typre of data that the method is expected to return
        public ActionResult<BrochureDTO> GetById([FromRoute]int id)
        {
            var brochure = _service.GetBrochureById(id); // the object  brochure
            if (brochure == null) return NotFound(); // if not found returns notfound
            return Ok(brochure); //else returns ok which returns succes code with json body reposnse
        }
    
        [HttpGet(Name = "GetBrochure")]
        // making   sure it returns a list of brochures 
        public ActionResult<IEnumerable<BrochureDTO>> Get()
        {
            var brochures = _service.GetBrochures();

            return Ok(brochures);
        }
        [HttpGet("filter")]
        public ActionResult<IEnumerable<BrochureDTO>> GetByFilter([FromQuery]string inputName)
        {
            var filtered = _service.GetByFilter(inputName);


            return Ok(filtered);
        }
        [HttpDelete]
        public ActionResult DeleteBrochure(int id)
        {
            var brochure = _service.GetBrochureById(id);
            if (brochure == null) return NotFound($"Brochure with ID {id} not found.");

            _service.DeleteBrochure(id);
            return NoContent(); 
        }

    



    }
}
