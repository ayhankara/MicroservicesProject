using AutoMapper;
using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Models;

namespace Course.Services.Catalog.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<Cours, CourseDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();

            CreateMap<Cours, CourseCreateDto>().ReverseMap();
            CreateMap<Cours, CourseUpdateDto>().ReverseMap();

        }
    }
}
