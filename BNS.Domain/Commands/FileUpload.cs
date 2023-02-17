using System;

namespace BNS.Domain.Commands
{
    public class FileUpload
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public bool IsAddNew{ get; set; }
        public bool IsDelete{ get; set; }
        public FileItem File { get; set; }
    }

    public class FileItem
    {
        public string Name { get; set; }
        public string Path { get; set; }

    }
}
