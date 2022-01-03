using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.ViewModels
{
    public class CommandBase<T> : IRequest<T> where T : class
    {
        public Guid CreatedBy { get; set; }
        public Guid ShopIndex { get; set; }
    }
}
