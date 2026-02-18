using StudentManagementAPI.Models;

namespace StudentManagementAPI.Interface
{
    public interface IStudentInterface
    {
        Task<CreateStudentResponse> CreateStudent(CreateStudentRequest request);
        Task<CommonResponse> DeleteStudent(int? StudetnID);
        Task<StudentListResponse> GetAllStudent();
        Task<StudentData> GetStudentByID(int? StudentID);
        Task<CommonResponse> UpdateStudent(OneStudentData request);
    }
}
