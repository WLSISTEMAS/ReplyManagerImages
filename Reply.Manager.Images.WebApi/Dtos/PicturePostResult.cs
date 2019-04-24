using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reply.Manager.Images.WebApi.Dtos
{
    public class PicturePostResult
    {
        public Guid Id { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public string File { get; set; }
    }
}
