using System;
using System.Collections.Generic;
using System.Text;

namespace Reply.Manager.Images.Domain.Models
{
    public class Picture
    {
        public Guid Id { get; set; } 
        public string ImageName { get; set; }
        public string Description { get; set; }
        public string File { get; set; }
    }
}
