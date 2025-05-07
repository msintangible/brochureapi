using System;
namespace brochureapi.Models
{
    public class Brochure
    {
        
        public Brochure() { }
        public Brochure(int bId, string bName, DateOnly dateOnly)
        {
            this.Id = bId;
            this.Name = bName;
            this.Datetime = dateOnly;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly Datetime {get;set;}

    }
}
