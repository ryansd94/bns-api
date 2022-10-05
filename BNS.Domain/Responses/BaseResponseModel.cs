using System;

namespace BNS.Domain
{
    public class BaseResponseModel
    {
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedDateStr
        {
            get { return UpdatedDate != null ? UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : string.Empty; }
        }
        public Guid UpdatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateStr
        {
            get { return CreatedDate.ToString("dd/MM/yyyy HH:mm:ss"); }
        }
        public Guid CreatedUserId { get; set; }
        public Guid Id { get; set; }
    }
}
