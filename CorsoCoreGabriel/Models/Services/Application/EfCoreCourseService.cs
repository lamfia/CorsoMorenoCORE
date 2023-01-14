using CorsoCoreGabriel.Models.Services.Infrastructure;
using CorsoCoreGabriel.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorsoCoreGabriel.Models.Services.Application
{
    public class EfCoreCourseService : ICourseService
    {
        private readonly MyCourseDbContext dbContext;

        public EfCoreCourseService(MyCourseDbContext dbContext)
        {
            this.dbContext = dbContext;
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

        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            IQueryable<CourseViewModel> querylinq = dbContext.Courses
                .AsNoTracking()
                .Select(course => CourseViewModel.FromEntity(course));

            List<CourseViewModel> courses = await querylinq.ToListAsync();

            return courses;
        }
    }
}
