namespace BNS.Data.Entities.JM_Entities
{
    public class JM_File: BaseJMEntity
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public decimal Size { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
    }
}
