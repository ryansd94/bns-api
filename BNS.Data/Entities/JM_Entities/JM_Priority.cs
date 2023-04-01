namespace BNS.Data.Entities.JM_Entities
{
    public class JM_Priority: BaseJMEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int Order { get; set; }
    }
}