using CorsoCoreGabriel.Models.Entities;
using CorsoCoreGabriel.Models.Options;
using CorsoCoreGabriel.Models.Services.Infrastructure;
using CorsoCoreGabriel.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CorsoCoreGabriel.Models.Services.Application
{
    public class EfCoreCourseService : ICourseService
    {
        private readonly MyCourseDbContext dbContext;
        public IOptionsMonitor<CoursesOptions> coursesoptions { get; }

        public EfCoreCourseService(MyCourseDbContext dbContext, IOptionsMonitor<CoursesOptions> optionsMonitor)
        {
            this.dbContext = dbContext;
            coursesoptions = optionsMonitor;
        }


        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            IQueryable<CourseDetailViewModel> querylinq = dbContext.Courses
                .AsNoTracking()
                .Include(course => course.Lessons)
                .Where(course => course.Id == id)
                .Select(course => CourseDetailViewModel.FromEntity(course))  //.FirstOrDefaultAsync() gli va bene tutto
                                                                             //.FirstAsync() primo elemento anche se è una lista
                                                                             //.SingleOrDefault()può essere vuoto e se è vuoto è null
                 ; //primo elemento. se è 0 o 1+ solleva una exception



            CourseDetailViewModel viewmodel = await querylinq.SingleAsync();

            return viewmodel;
        }

        public async Task<ListViewModel<CourseViewModel>> GetCoursesAsync(string search, int page, string orderby, bool ascending)
        {


            page = Math.Max(page, 1);
            int limit = coursesoptions.CurrentValue.PerPage;

            int offset = limit * (page - 1);

            IQueryable<Course> baseQuery = dbContext.Courses;


            var orderOptions = coursesoptions.CurrentValue.Order;

            if (!orderOptions.Allow.Contains(orderby))
            {
                orderby = orderOptions.By;
                ascending = orderOptions.Ascending;
            }

            switch (orderby)
            {

                case "Title":
                    if (ascending)
                    {
                        baseQuery = baseQuery.OrderBy(x => x.Title);
                    }
                    else
                    {
                        baseQuery = baseQuery.OrderByDescending(x => x.Title);
                    }
                    break;
                case "Rating":
                    if (ascending)
                    {
                        baseQuery = baseQuery.OrderBy(x => x.Rating);
                    }
                    else
                    {
                        baseQuery = baseQuery.OrderByDescending(x => x.Rating);
                    }
                    break;
                case "CurrentPrice":

                    if (ascending)
                    {
                        baseQuery = baseQuery.OrderBy(x => x.CurrentPrice);
                    }
                    else
                    {
                        baseQuery = baseQuery.OrderByDescending(x => x.CurrentPrice);
                    }
                    break;
                default:
                    break;
            }





            IQueryable<CourseViewModel> querylinq = baseQuery
                .Where(x => x.Title.Contains(search))
                .AsNoTracking()
                .Select(course => CourseViewModel.FromEntity(course));
            try
            {
                List<CourseViewModel> courses = await querylinq
                     .Skip(offset)
                     .Take(limit)
                     .ToListAsync();

                int totalcount = await querylinq.CountAsync();

                ListViewModel<CourseViewModel> result = new ListViewModel<CourseViewModel>
                {
                    List = courses,
                    TotalCount = totalcount
                };

                return result;
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        public async Task<List<CourseViewModel>> getBestRatingCourses()
        {

            var result = await GetCoursesAsync("", 1, "Rating", false);

            return result.List;
        }

        public async Task<List<CourseViewModel>> getMostRecentCourses()
        {
            var result = await GetCoursesAsync("", 1, "Id", false);

            return result.List;

        }
    }
}
