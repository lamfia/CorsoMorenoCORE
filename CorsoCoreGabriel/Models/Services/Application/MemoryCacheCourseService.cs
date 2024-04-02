using CorsoCoreGabriel.Models.InputModel;
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

        public Task<ListViewModel<CourseViewModel>> GetCoursesAsync(string search, int page, string orderby, bool ascending)
        {
            //Solo se sta tra le prime 5 pagine e se non hanno cercato qualcosa
            bool canCache = page <= 5 && (string.IsNullOrEmpty(search));

            if (canCache)
            {
                return MemoryCache.GetOrCreateAsync(
                  $"Courses{search}-{page}-{orderby}-{ascending}", cacheEntry =>
                  {
                      cacheEntry.SetSize(2);
                      cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(Options.CurrentValue.SecondsCache));
                      return courseService.GetCoursesAsync(search, page, orderby, ascending);
                  });
            }

            //altrimenti si fa la query direttamente e non si mette in cache
            return courseService.GetCoursesAsync(search, page, orderby, ascending);
        }

        public Task<List<CourseViewModel>> getBestRatingCourses()
        {
            return MemoryCache.GetOrCreateAsync($"BestRatingCourses", cacheEntry =>
            {
                cacheEntry.SetSize(2);
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
                return courseService.getBestRatingCourses();
            });
        }

        public Task<List<CourseViewModel>> getMostRecentCourses()
        {
            return MemoryCache.GetOrCreateAsync($"MostRecentCourses", cacheEntry =>
            {
                cacheEntry.SetSize(2);
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
                return courseService.getMostRecentCourses();
            });
        }
    }
}
