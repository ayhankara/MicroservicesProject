using Course.Services.Catalog.Dtos;
using Course.Shared.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;


namespace Course.Services.Catalog.Services
{
    public interface ICourseService
    {
        Task<Response<List<CourseDto>>> GetAllAsync();

        Task<Response<CourseDto>> GetByIdAsync(string id);

        Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId);

        Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);

        Task<Response<Shared.Dtos.NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto);

        Task<Response<Shared.Dtos.NoContent>> DeleteAsync(string id);
    }
}