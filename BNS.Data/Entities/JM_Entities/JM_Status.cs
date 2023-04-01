namespace BNS.Data.Entities.JM_Entities
{
    public class JM_Status : BaseJMEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public bool IsStatusStart { get; set; }
        public bool IsStatusEnd { get; set; }
    }
}
