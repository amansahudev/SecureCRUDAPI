using Dapper;
using StudentManagementAPI.Interface;
using StudentManagementAPI.Models;
using System.Data;
using Microsoft.Data.SqlClient;



namespace StudentManagementAPI.Repository
{
    public class StudentRepository:IStudentInterface
    {
        #region Connection String
        private readonly string _ConnectionString;
        public StudentRepository(IConfiguration configuration)
        {
            _ConnectionString = configuration.GetConnectionString("constr");
        }
        #endregion

        #region Create Student API
        public async Task<CreateStudentResponse> CreateStudent(CreateStudentRequest request)
        {
            var response = new CreateStudentResponse();
            try
            {

                #region Adding Paramenters
                var paramenters =new DynamicParameters();
                paramenters.Add("StudentName", request.StudentName,DbType.String);
                paramenters.Add("StudentEmail", request.StudentEmail, DbType.String);
                paramenters.Add("StudentDOB", request.StudentDOB, DbType.String);
                paramenters.Add("Branch", request.Branch, DbType.String);
                paramenters.Add("MobileNumber", request.MobileNumber, DbType.String);
                paramenters.Add("IsActive", request.IsActive, DbType.Boolean);
               
                #endregion
                using (IDbConnection con = new SqlConnection(_ConnectionString))
                {
                    response = await con.QueryFirstAsync<CreateStudentResponse>("API_CreateStudent", paramenters,null,null,commandType:CommandType.StoredProcedure);
                }

            }
            catch (Exception ex)
            {
                response.message=ex.Message;
                response.code = 999;
                response.success = false;

            }
            return response;

        }

        #endregion

        #region Delete Student API
        public async Task<CommonResponse> DeleteStudent(int? StudetnID)
        {
            var response = new CommonResponse();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("StudentID", StudetnID, DbType.Int32);

                using (IDbConnection con = new SqlConnection(_ConnectionString))
                {
                    response = await con.QueryFirstAsync<CommonResponse>("API_deletestudent", parameters, null, null, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.code = 999;
                response.success = false;
            }
            return response;
        }
        #endregion

        #region Select One Student API
        public async Task<StudentData> GetStudentByID(int? StudetnID)
        {
            var response = new StudentData();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("StudentID", StudetnID, DbType.Int32);

                using (IDbConnection con = new SqlConnection(_ConnectionString))
                {
                    response = await con.QueryFirstAsync<StudentData>("API_SelectSingleStudent", parameters, null, null, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.code = 999;
                response.success = false;
            }
            return response;
        }
        #endregion

        #region Select All Student API
        public async Task<StudentListResponse> GetAllStudent()
        {
            var response = new StudentListResponse();
            response.StudentData = new List<OneStudentData>();
            try
            {
                var parameters = new DynamicParameters();
                
                using (IDbConnection con = new SqlConnection(_ConnectionString))
                {
                    //response = await con.QueryAsync<List<StudentData>>("API_SelectSingleStudent", null, null, null, commandType: CommandType.StoredProcedure);
                    var responseAllData =await con.QueryAsync<StudentData>("API_SelectAllStudent",null,null,null,commandType:CommandType.StoredProcedure);
                    bool RunOnce = true;
                    foreach(var item in responseAllData)
                    {
                        if (RunOnce)
                        {
                            response.message = item.message;
                            response.code = item.code;
                            response.success = item.success;
                            RunOnce = false;
                        }
                        OneStudentData data = new OneStudentData();
                        data.StudentName = item.StudentName;
                        data.StudentEmail = item.StudentEmail;
                        data.StudentDOB = item.StudentDOB;
                        data.StudentId = item.StudentId;
                        data.MobileNumber = item.MobileNumber;
                        data.Branch = item.Branch;
                        data.IsActive = item.IsActive;

                        response.StudentData.Add(data);

                    }
                    //return response;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.code = 999;
                response.success = false;
            }
            return response;
        }
        #endregion

        #region Update Student API
        public async Task<CommonResponse> UpdateStudent(OneStudentData request)
        {
            var response = new CommonResponse();
            try
            {

                #region Adding Paramenters
                var paramenters = new DynamicParameters();
                paramenters.Add("StudentId", request.StudentId, DbType.Int32);
                paramenters.Add("StudentName", request.StudentName, DbType.String);
                paramenters.Add("StudentEmail", request.StudentEmail, DbType.String);
                paramenters.Add("StudentDOB", request.StudentDOB, DbType.String);
                paramenters.Add("Branch", request.Branch, DbType.String);
                paramenters.Add("MobileNumber", request.MobileNumber, DbType.String);
                paramenters.Add("IsActive", request.IsActive, DbType.Boolean);

                #endregion
                using (IDbConnection con = new SqlConnection(_ConnectionString))
                {
                    response = await con.QueryFirstAsync<CommonResponse>("API_UpdateStudent", paramenters, null, null, commandType: CommandType.StoredProcedure);
                }

            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.code = 999;
                response.success = false;

            }
            return response;

        }

        #endregion
    }
}
