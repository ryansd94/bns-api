using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Domain.Interface;
using BNS.Resource;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BNS.Service.Implement
{
    public class AttachedFileService : IAttachedFileService
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AttachedFileService(
            IStringLocalizer<SharedResource> sharedLocalizer,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _sharedLocalizer = sharedLocalizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Guid> AddAttachedFiles(List<CreateAttachedFilesRequest> attachedFiles)
        {
            foreach (var attachedFile in attachedFiles)
            {
                var file = _mapper.Map<JM_File>(attachedFile.File);
                file.Id = Guid.NewGuid();
                file.Path = attachedFile.File.Path;
                file.Name = attachedFile.File.Path;
                file.Url = attachedFile.Url;
                file.CompanyId = attachedFile.CompanyId;
                file.CreatedDate = DateTime.UtcNow;
                file.CreatedUserId = attachedFile.UserId;

                _unitOfWork.Repository<JM_File>().Add(file);
                _unitOfWork.Repository<JM_AttachedFiles>().Add(new JM_AttachedFiles
                {
                    CreatedDate = DateTime.UtcNow,
                    CreatedUserId = attachedFile.UserId,
                    Id = Guid.NewGuid(),
                    EntityId = attachedFile.EntityId,
                    FileId = file.Id,
                    CompanyId = attachedFile.CompanyId,
                });
            }

            await _unitOfWork.SaveChangesAsync();
            return Guid.NewGuid();
        }

        public async Task<Guid> RemoveAttachedFiles(List<Guid> ids)
        {
            var attachedFiles = await _unitOfWork.Repository<JM_AttachedFiles>().Where(s => ids.Contains(s.Id)).ToListAsync();
            _unitOfWork.Repository<JM_AttachedFiles>().RemoveRange(attachedFiles);
            await _unitOfWork.SaveChangesAsync();
            return Guid.NewGuid();
        }
    }
}
