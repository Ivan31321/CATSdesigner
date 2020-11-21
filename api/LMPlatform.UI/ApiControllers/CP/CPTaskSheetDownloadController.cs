﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Application.Infrastructure.Export;
using LMPlatform.Data.Infrastructure;
using System.Data.Entity;

namespace LMPlatform.UI.ApiControllers.CP
{
    public class CPTaskSheetDownloadController : ApiController
    {
        public HttpResponseMessage Get(int courseProjectId)
        {
            var courseProject =
                new LmPlatformModelsContext().CourseProjects
                    .Include(x =>
                        x.AssignedCourseProjects.Select(y => y.Student.Group.Secretary.CoursePercentagesGraphs))
                    .Single(x => x.CourseProjectId == courseProjectId);

            string docName;
            if (courseProject.AssignedCourseProjects.Count == 1)
            {
                var stud = courseProject.AssignedCourseProjects.Single().Student;
                docName = $"{stud.LastName}_{stud.FirstName}";
            }
            else
            {
                docName = $"{courseProject.Theme}";
            }


            return WordCourseProject.CourseProjectToWord(docName, courseProject);
        }

        public HttpResponseMessage Get(int groupId, int subjectId)
        {
            var courseProjects = new LmPlatformModelsContext().CourseProjects
                                .Where(x => x.SubjectId == subjectId)
                                .Where(x => x.AssignedCourseProjects.Count() == 1)
                                .Include(x =>
                        x.AssignedCourseProjects.Select(y => y.Student.Group.Secretary.CoursePercentagesGraphs))
                                .Where(x => x.AssignedCourseProjects.FirstOrDefault().Student.GroupId == groupId);

            string fileName = "NoTaskSheet.zip";
            if (courseProjects.Count() > 0)
            {
                fileName = courseProjects.FirstOrDefault().AssignedCourseProjects.FirstOrDefault().Student.Group.Name + ".zip";
                return WordCourseProject.CourseProjectsToArchive(fileName, courseProjects);
            }
            else
            {
                return WordCourseProject.CourseProjectsToArchive(fileName, courseProjects);
            }
        }
    }
}