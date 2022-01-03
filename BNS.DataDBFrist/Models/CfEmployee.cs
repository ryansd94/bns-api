using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class CfEmployee
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
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public int? ShopIndex { get; set; }
        public string Code { get; set; }
        public bool? IsMainAccount { get; set; }
    }
}
