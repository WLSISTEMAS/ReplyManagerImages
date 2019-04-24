using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reply.Manager.Images.WebApi.Dtos
{
    public class PicturePost 
    {
        public string ImageName { get; set; }
        public string Description { get; set; }
        public List<IFormFile> File { get; set; }
    }
}
