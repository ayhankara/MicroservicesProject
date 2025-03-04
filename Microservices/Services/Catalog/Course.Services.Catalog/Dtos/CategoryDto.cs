using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoDB.Bson.Serialization.Attributes;

namespace Course.Services.Catalog.Dtos
{
    public class CategoryDto
    {
        [BsonIgnoreIfNull] // MongoDB için
        [BindNever] // ASP.NET Core doğrulamasında kontrol etme
        public string? Id { get; set; }

        public string Name { get; set; }
    }

}
