using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Persistence.Dapper.Teacher
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly IFactoryConnection _factoryConnection;

        public TeacherRepository(IFactoryConnection factoryConnection)
        {
            _factoryConnection = factoryConnection;
        }

        public async Task<int> DeleteTeacher(Guid id)
        {
            string storeProcedure = "sp_Delete_Teacher";
            try
            {
                var connection = _factoryConnection.GetConnection();
                return await connection.ExecuteAsync(
                    storeProcedure,
                    new { TeacherId = id },
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo eliminar el instructor ", e);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
        }

        public async Task<Teacher> GetTeacherById(Guid id)
        {
            string storeProcedure = "sp_Get_Teacher_ById";
            try
            {
                var connection = _factoryConnection.GetConnection();
                return await connection.QueryFirstAsync<Teacher>(
                    storeProcedure,
                    new { TeacherId = id },
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo obtenere el instructor", e);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
        }

        public async Task<IEnumerable<Teacher>> GetTeachers()
        {
            IEnumerable<Teacher> teachers = null;
            var storeProcedure = "sp_Get_Teachers";

            try
            {
                var connection = _factoryConnection.GetConnection();
                teachers = await connection.QueryAsync<Teacher>(storeProcedure, null, commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                throw new Exception("Error en la consulta de datos: ", e);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }

            return teachers;
        }

        public async Task<int> InsertTeacher(Teacher teacher)
        {
            try
            {
                var connection = _factoryConnection.GetConnection();
                string storeProcedure = "sp_Insert_Teacher";

                return await connection.ExecuteAsync(
                    storeProcedure,
                    new
                    {
                        TeacherId = Guid.NewGuid(),
                        FirstName = teacher.FirstName,
                        LastName = teacher.LastName,
                        JobTitle = teacher.JobTitle
                    },
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception e)
            {

                throw new Exception("No se pudo guardar el nuevo instructor", e);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
        }

        public async Task<int> UpdateTeacher(Teacher teacher)
        {
            string storeProcedure = "sp_Update_Teacher";

            try
            {
                var connection = _factoryConnection.GetConnection();
                return await connection.ExecuteAsync(
                    storeProcedure,
                    new
                    {
                        TeacherId = teacher.TeacherId,
                        FirstName = teacher.FirstName,
                        LastName = teacher.LastName,
                        JobTitle = teacher.JobTitle
                    },
                    commandType: CommandType.StoredProcedure
                );

            }
            catch (Exception e)
            {
                throw new Exception("No se pudo actualizar el instructor ", e);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
        }
    }
}
