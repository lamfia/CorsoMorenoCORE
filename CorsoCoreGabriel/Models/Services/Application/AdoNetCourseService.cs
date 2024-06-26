﻿using CorsoCoreGabriel.Models.Exceptions;
using CorsoCoreGabriel.Models.Options;
using CorsoCoreGabriel.Models.Services.Infrastructure;
using CorsoCoreGabriel.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static CorsoCoreGabriel.Models.Services.Infrastructure.SqliteDatabaseAccessor;

namespace CorsoCoreGabriel.Models.Services.Application
{
    public class AdoNetCourseService : ICachedCourseService
    {
        private readonly IDatabaseAccessor db;
        private readonly IOptionsMonitor<CoursesOptions> coursesoptions;

        public IMemoryCache MemoryCache { get; }
        private ILogger<AdoNetCourseService> logger { get; }

        public AdoNetCourseService(IMemoryCache memoryCache, ILogger<AdoNetCourseService> logger, IDatabaseAccessor db, IOptionsMonitor<CoursesOptions> coursesoptions)
        {
            this.coursesoptions = coursesoptions;
            MemoryCache = memoryCache;
            this.logger = logger;
            this.db = db;
        }



        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {

            logger.LogInformation("Course {id} requested", id);

            FormattableString query = $"SELECT * FROM Courses where Id={id} ; SELECT * FROM Lessons where CourseId={id}";

            DataSet dataSet = await db.QueryAsync(query);

            //Course
            var dt = dataSet.Tables[0];
            if (dt.Rows.Count != 1)
            {
                logger.LogWarning("Course id: {id} not found", id);
                throw new CourseNotFoundException(id);
            }
            var courseRow = dt.Rows[0];
            var courseDetailViewModel = CourseDetailViewModel.FromDataRow(courseRow);

            //Lessons
            var lessonDataTable = dataSet.Tables[1];
            foreach (DataRow dataRow in lessonDataTable.Rows)
            {
                LessonViewModel lessonViewModel = LessonViewModel.FromDataRow(dataRow);

                courseDetailViewModel.Lessons.Add(lessonViewModel);
            }

            return courseDetailViewModel;
        }

        public async Task<ListViewModel<CourseViewModel>> GetCoursesAsync(string search, int page, string orderby, bool ascending)
        {


            int limit = coursesoptions.CurrentValue.PerPage;

            int offset = limit * (page - 1);

            if (orderby == "CurrentPrice")
            {
                orderby = "CurrentPrice_Amount";
            }

            string direction = ascending ? "ASC" : "DESC";

            FormattableString query = $"SELECT * FROM Courses WHERE Title LIKE {"%" + search + "%"} ORDER BY {(Sql)orderby} {(Sql)direction} LIMIT {limit} OFFSET {offset} ; SELECT COUNT(*) FROM Courses WHERE Title LIKE {"%" + search + "%"}  ";



            DataSet dataSet = await db.QueryAsync(query);

            var datatable = dataSet.Tables[0];

            var courseList = new List<CourseViewModel>();
            foreach (DataRow row in datatable.Rows)
            {
                CourseViewModel course = CourseViewModel.FromDataRow(row);

                courseList.Add(course);

            }


            ListViewModel<CourseViewModel> result = new ListViewModel<CourseViewModel>
            {
                List = courseList,
                TotalCount = Convert.ToInt32(dataSet.Tables[1].Rows[0][0])
            };

            return result;
        }

        public async Task<List<CourseViewModel>> getBestRatingCourses()
        {

            var result = await GetCoursesAsync("", 1, "Rating", false);

            return result.List.Take(3).ToList();
        }

        public async Task<List<CourseViewModel>> getMostRecentCourses()
        {
            var result = await GetCoursesAsync("", 1, "Id", false);

            return result.List.Take(3).ToList();

        }
    }



}
