using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using static BNS.Utilities.Common;

namespace BNS.Domain
{
    public class CommandRequest<T> : IRequest<T> where T : class
    {
        public Guid CompanyId { get; set; }
        public Guid UserId { get; set; }
        
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public bool isAdd { get; set; }
        public bool isEdit { get; set; }

        public string filters { get; set; }
        public string fieldSort { get; set; }
        public string sort { get; set; }
        public bool isGetAll { get; set; } = false;

    }
    public class CommandByIdRequest<T> : IRequest<T> where T : class
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
    }

}
