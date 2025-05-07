using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace brochureapi.Models
{
    public class Page
    {
        public int Id { get; set; } // primaray key
        public string Name { get; set; }
        public int BrochureId { get; set; }//foregin key
        [JsonIgnore]
        public Brochure? Brochure { get; set; }  //links back to a brochure 

    }
}
