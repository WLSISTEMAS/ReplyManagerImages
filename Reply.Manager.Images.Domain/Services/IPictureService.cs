using Reply.Manager.Images.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reply.Manager.Images.Domain.Services
{
    public interface IPictureService
    {
        Task<Picture> UploadPictureAsync(Picture picture);
    }
}
