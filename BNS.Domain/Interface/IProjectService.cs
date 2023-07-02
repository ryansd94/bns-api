using BNS.Data.Entities.JM_Entities;
using BNS.Domain.Commands;
using System;
using System.Collections.Generic;

namespace BNS.Domain.Interface
{
    public interface IProjectService
    {
        public  List<JM_ProjectPhase> GetAllChilds(Guid? parentId, SprintRequest projectPhase, Guid userId, Guid projectId, Guid companyId);
    }
}
