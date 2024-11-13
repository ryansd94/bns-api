using MediatR;
using System;

namespace BNS.Domain
{
    public class CommandBase<T> : IRequest<T> where T : class
    {
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid AccountCompanyId { get; set; }
    }
}
