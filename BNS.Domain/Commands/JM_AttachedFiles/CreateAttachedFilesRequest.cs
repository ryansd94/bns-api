using System;

namespace BNS.Domain.Commands
{
    public class CreateAttachedFilesRequest : CommandBase<ApiResult<Guid>>
    {
        public Guid Id { get; set; }
        public Guid EntityId { get; set; }
        public string Url { get; set; }
        public bool IsAddNew{ get; set; }
        public bool IsDelete{ get; set; }
        public FileItem File { get; set; }
    }
}
