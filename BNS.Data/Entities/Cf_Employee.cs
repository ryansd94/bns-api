using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_Employee:BaseEntity
    {
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
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

        public Guid? AccountIndex { get; set; }
        public Guid? DepartmentIndex { get; set; }
        public Guid? PositionIndex { get; set; }
        public DateTime? WorkingDate { get; set; }

        public CF_Shop CF_Shop { get; set; }
        public CF_Account CF_Account { get; set; }
        public CF_Department CF_Department { get; set; }
        public CF_Position CF_Position { get; set; }
        public CF_Branch CF_Branch { get; set; }

    }
}
