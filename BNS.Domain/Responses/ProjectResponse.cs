using System;
using System.Collections.Generic;
using static BNS.Utilities.Enums;

namespace BNS.Domain.Responses
{
    public class ProjectResponse
    {
        public List<ProjectResponseItem> Items { get; set; }
    }
    public class ProjectResponseItem : BaseResponseModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public EProjectType Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<Guid> Teams { get; set; }
        public List<Guid> Members { get; set; }
        public List<SprintReponse> Sprints { get; set; }
    }

    public class SprintReponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? ParentId { get; set; }
        public bool Active { get; set; }
        public List<SprintReponse> Childs { get; set; }
    }
}
