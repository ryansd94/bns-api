using System;

namespace BNS.Models.Responses.Category
{
   public class CategoryResponseModel : BaseResultModel
    {
        public string Name { get; set; }
        public int? Number { get; set; }
        public string Note { get; set; }

    }
}
