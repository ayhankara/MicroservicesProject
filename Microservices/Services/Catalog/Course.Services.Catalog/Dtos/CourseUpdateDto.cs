﻿namespace Course.Services.Catalog.Dtos
{
    public class CourseUpdateDto
    {
        public string Id { get; set; }
        public int Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }

        public FeatureDto Feature { get; set; }

        public string CategoryId { get; set; }
        public string Picture { get; set; }
    }
}
