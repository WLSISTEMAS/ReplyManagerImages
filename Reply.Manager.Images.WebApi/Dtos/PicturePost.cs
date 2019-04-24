using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reply.Manager.Images.WebApi.Dtos
{
    public class PicturePost 
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ImageName { get; set; }
        public string Description { get; set; }
        public string File { get; set; }
    } 
    
}
