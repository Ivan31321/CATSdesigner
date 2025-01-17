﻿using LMPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.LabsManagement
{
    public interface ILabsManagementService
    {
        IEnumerable<UserLabFiles> GetUserLabFiles(int userId, int labId);
        bool HasSubjectProtection(int groupId, int subjectId);

        List<UserLabFiles> GetGroupLabFiles(int subjectId, int groupId);
        List<UserLabFiles> GetStudentLabFiles(int subjectId, int studentId);

    }
}
