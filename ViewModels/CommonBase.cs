using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Models
{
    public class CommandBase<T> : IRequest<T> where T : class
    {
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
    }
}
