namespace BNS.Data.Entities.JM_Entities
{
    public class JM_Status : BaseJMEntityActive
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public bool IsStatusStart { get; set; }
        public bool IsStatusEnd { get; set; }
        public bool IsAutomaticAdd { get; set; }
        public bool IsApplyAll { get; set; }
    }
}
