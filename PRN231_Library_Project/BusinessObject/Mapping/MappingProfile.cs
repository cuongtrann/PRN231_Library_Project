using AutoMapper;
using PRN231_Library_Project.BusinessObject.DTO;
using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.BusinessObject.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDTO, Book>().ReverseMap();
            CreateMap<ReviewDTO, Review>().ReverseMap();
            CreateMap<AddBookRequest, Book> ().ForMember(des => des.CopiesAvailable, act => act.MapFrom(src => src.Copies))
                .ForMember(des => des.BookContent, act => act.MapFrom(src => src.FilePath));

        }
    }
}
