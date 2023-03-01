using System.Collections.Generic;

namespace BNS.Domain.Responses
{
    public class CommentResponse
    {
        public List<CommentResponseItem> Items { get; set; }
    }
    public class CommentResponseItem : BaseResponseModel
    {
        public User User { get; set; }
        public string UpdatedTime { get; set; }
        public string Value { get; set; }
        public bool IsAddNew { get; set; }
        public int CountReply { get; set; }
        public bool IsDelete { get; set; }
        public int Level { get; set; }
        public List<CommentResponseItem> Childrens { get; set; }
    }
}
