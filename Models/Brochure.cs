using System;
namespace brochureapi.Models
{
    public class Brochure
    {
        public Brochure()
        {
             
        }

        public Brochure(int bId, string bName, DateOnly dateOnly, List<Page> pages) : this()
        {
            this.Id = bId;
            this.Name = bName;
            this.Datetime = dateOnly;
            this.Pages = pages;
        }
        public Brochure(int bId, string bName, DateOnly dateOnly) : this()
        {
            this.Id = bId;
            this.Name = bName;
            this.Datetime = dateOnly;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly Datetime { get; set; }

        public List<Page> Pages { get; set; } = new List<Page>();
    }
}
