using CorsoCoreGabriel.Models.Options;
using CorsoCoreGabriel.Models.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorsoCoreGabriel.Models.Services.Application
{
    public class MemoryCacheCourseService : ICachedCourseService
    {
        private readonly ICourseService courseService;

        public IMemoryCache MemoryCache { get; }
        public IOptionsMonitor<CacheOptions> Options { get; }

        public MemoryCacheCourseService(ICourseService courseService, IMemoryCache memoryCache,
            IOptionsMonitor<CacheOptions> options)
        {
            this.courseService = courseService;
            MemoryCache = memoryCache;
            Options = options;
        }

        public Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            return MemoryCache.GetOrCreateAsync(
                $"Course{id}", cacheEntry =>
                {
                    cacheEntry.SetSize(1);
                    cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(Options.CurrentValue.SecondsCache));
                    return courseService.GetCourseAsync(id);
                });
        }

        public Task<List<CourseViewModel>> GetCoursesAsync()
        {
            return MemoryCache.GetOrCreateAsync(
              $"Courses", cacheEntry =>
              {
                  cacheEntry.SetSize(2);
                  cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(Options.CurrentValue.SecondsCache));
                  return courseService.GetCoursesAsync();
              });
        }
    }
}
