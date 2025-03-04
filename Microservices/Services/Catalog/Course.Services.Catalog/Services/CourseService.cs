using AutoMapper;
using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Models;
using Course.Services.Catalog.Settings;
using Course.Shared.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Driver;

namespace Course.Services.Catalog.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Cours> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings )
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _courseCollection = database.GetCollection<Cours>(databaseSettings.CourseCollectionName);

            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;

        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(course => true).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Cours>();
            }

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find<Cours>(x => x.Id == id).FirstOrDefaultAsync();

            if (course == null)
            {
                return Response<CourseDto>.Fail("Course not found", 404);
            }
            course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();

            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }

        public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find<Cours>(x => x.UserId == userId).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Cours>();
            }

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var newCourse = _mapper.Map<Cours>(courseCreateDto);

            newCourse.CreatedDate = DateTime.Now;
            await _courseCollection.InsertOneAsync(newCourse);

            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 200);
        }

        public async Task<Response<Shared.Dtos.NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var updateCourse = _mapper.Map<Cours>(courseUpdateDto);

            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, updateCourse);

            if (result == null)
            {
                return Response<Shared.Dtos.NoContent>.Fail("Course not found", 404);
            }

            //await _publishEndpoint.Publish<CourseNameChangedEvent>(new CourseNameChangedEvent { CourseId = updateCourse.Id, UpdatedName = courseUpdateDto.Name });

            return Response<Shared.Dtos.NoContent>.Success(204);
        }

        public async Task<Response<Shared.Dtos.NoContent>> DeleteAsync(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount > 0)
            {
                return Response<Shared.Dtos.NoContent>.Success(204);
            }
            else
            {
                return Response<Shared.Dtos.NoContent>.Fail("Course not found", 404);
            }
        }
    }
}