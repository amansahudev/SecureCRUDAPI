namespace StudentManagementAPI.Models
{
    #region Request Response for Create API 
    public class CreateStudentRequest
    {
        public string StudentName { get; set; }
        public string StudentEmail { get; set; }
        public string StudentDOB { get; set; }
        public string Branch { get; set; }
        public string MobileNumber { get; set; }
        public bool IsActive { get; set; }

    }
    #endregion

    #region Response for Create API
    public class CreateStudentResponse
    {
        public string message { get; set; }
        public int code { get; set; }
        public bool success { get; set; }
        public string StudentName { get; set; }
        public string StudentEmail { get; set; }
        public string StudentDOB { get; set; }
        public string Branch { get; set; }
        public string MobileNumber { get; set; }
        public bool IsActive { get; set; }
    }
    #endregion

    #region Common Response For Delete and Update API
    public class CommonResponse
    {
        public string message { get; set; }
        public bool success { get; set; }
        public int code { get; set; }
    }

    #endregion 

    #region Response for Get Student By ID API
    public class StudentData
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentEmail { get; set; }
        public string StudentDOB { get; set; }
        public string Branch { get; set; }
        public string MobileNumber { get; set; }
        public bool IsActive { get; set; }

        // For common API response
        public string message { get; set; }
        public int code { get; set; }
        public bool success { get; set; }
    }


    #endregion

    #region Response for Get All Student API
    public class StudentListResponse
    {
        public string message { get; set; }
        public int code { get; set; }
        public bool success { get; set; }
        public List<OneStudentData> StudentData { get; set; }
    }
    #endregion

    #region Request for Update Student API
    public class OneStudentData
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentEmail { get; set; }
        public string StudentDOB { get; set; }
        public string Branch { get; set; }
        public string MobileNumber { get; set; }
        public bool IsActive { get; set; }
    }
    #endregion
}
