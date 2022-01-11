using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.ViewModels
{
    public class CommandRequest<T> : IRequest<T> where T : class
    {
        public Guid ShopIndex { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public bool isAdd { get; set; }
        public bool isEdit { get; set; }

        public string fieldSort { get; set; }
        public string sort { get; set; }
    }
    public class CommandByIdRequest<T> : IRequest<T> where T : class
    {
        public Guid Id { get; set; }
    }

}
