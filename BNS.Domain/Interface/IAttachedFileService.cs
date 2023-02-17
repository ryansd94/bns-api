using BNS.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BNS.Domain.Interface
{
    public interface IAttachedFileService
    {
        Task<Guid> AddAttachedFiles(List<CreateAttachedFilesRequest> attachedFiles);
        Task<Guid> RemoveAttachedFiles(List<Guid> ids);
    }
}
