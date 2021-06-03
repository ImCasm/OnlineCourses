using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Dapper.Teacher
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teacher>> GetTeachers();
        Task<Teacher> GetTeacherById(Guid id);
        Task<int> InsertTeacher(Teacher teacher);
        Task<int> UpdateTeacher(Teacher teacher);
        Task<int> DeleteTeacher(Guid id);
    }
}
