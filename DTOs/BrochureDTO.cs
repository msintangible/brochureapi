using brochureapi.DTOs;

namespace brochureapi.NewFolder
{
    public class BrochureDTO
    {
        private int v1;
        private string v2;
        private DateOnly dateOnly;

        public BrochureDTO()
        {
        }

        public BrochureDTO(int v1, string v2, DateOnly dateOnly)
        {
            this.Id = v1;
            this.Name = v2;
            this.Datetime = dateOnly;
        }
        public BrochureDTO(int bId, string bName, DateOnly dateOnly, List<PageDTO> pages) : this()
        {
            this.Id = bId;
            this.Name = bName;
            this.Datetime = dateOnly;
            this.Pages = pages;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly Datetime { get; set; }
        public List<PageDTO> Pages { get; set; } = new();
    }
}
