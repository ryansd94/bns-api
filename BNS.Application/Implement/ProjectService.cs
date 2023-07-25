using BNS.Data.Entities.JM_Entities;
using BNS.Domain.Commands;
using BNS.Domain.Interface;
using System;
using System.Collections.Generic;

namespace BNS.Service.Implement
{
    public class ProjectService : IProjectService
    {
        public List<JM_ProjectPhase> GetAllChilds(Guid? parentId, SprintRequest projectPhase, Guid userId, Guid projectId, Guid companyId)
        {
            var childs = new List<JM_ProjectPhase>();
            if (projectPhase.Childs != null && projectPhase.Childs.Count > 0)
            {
                foreach (var child in projectPhase.Childs)
                {
                    var phase = new JM_ProjectPhase
                    {
                        ProjectId = projectId,
                        CreatedUserId = userId,
                        Name = child.Name,
                        StartDate = child.StartDate,
                        EndDate = child.EndDate,
                        ParentId = parentId,
                        CompanyId = companyId,
                        Active = child.Active
                    };
                    childs.Add(phase);
                    childs.AddRange(GetAllChilds(phase.Id, child, userId, projectId, companyId));
                }
            }
            return childs;
        }
    }
}
