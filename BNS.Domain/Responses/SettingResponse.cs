using static BNS.Utilities.Enums;

namespace BNS.Domain.Responses
{
    public class SettingResponse
    {
        public TaskSetting TaskSetting { get; set; } = new TaskSetting();
        public ProjectSetting ProjectSetting { get; set; } = new ProjectSetting();
    }
    public class TaskSetting
    {
        public EViewMode ViewMode { get; set; }
        public bool IsFullScreen { get; set; }
    }
    public class ProjectSetting
    {
        public string Current { get; set; }
        public string CurrentId { get; set; }
    }
}
