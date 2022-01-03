using System;
namespace BNS.ViewModels.Responses
{
   public class CF_EmployeeResponseModel: BaseResultModel
    {
        public string EmployeeName { get; set; }

        public string EmployeeCode { get; set; }
        public int? Gender { get; set; }
        public string Nric { get; set; }
        public DateTime? DateOfNric { get; set; }
        public string PlaceOfNric { get; set; }
        public DateTime? BrithDate { get; set; }
        public DateTime? JoinedDate { get; set; }
        public string PermanentAddress { get; set; }
        public string TemporaryAddress { get; set; }
        public bool? Active { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string EmployeeImage { get; set; }
        public bool? IsMainAccount { get; set; }
        public int? Region { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime? WorkingDate { get; set; }
        public string DepartmentIndex { get; set; }
        public string PositionIndex { get; set; }
        public string DepartmentIndexSearch { get; set; }
        public string PositionIndexSearch { get; set; }

    }
}
