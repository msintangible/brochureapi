using AutoMapper;
using brochureapi.DTOs;
using brochureapi.NewFolder;

namespace brochureapi.Mappings
{
    public class Mapper : Profile
    {

        public Mapper()
        {
            CreateMap<Models.Brochure, BrochureDTO>().ReverseMap();

            CreateMap<Models.Page, PageDTO>().ReverseMap();
               
        }
    }
}
