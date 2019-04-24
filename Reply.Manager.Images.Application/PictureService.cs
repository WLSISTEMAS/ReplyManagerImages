using Reply.Manager.Images.Domain.Adapters;
using Reply.Manager.Images.Domain.Models;
using Reply.Manager.Images.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reply.Manager.Images.Application
{
    public class PictureService : IPictureService
    {
        private readonly IPictureAdapter pictureAdapter;

        public PictureService(IPictureAdapter pictureAdapter)
        {
            this.pictureAdapter = pictureAdapter ?? throw new ArgumentNullException(nameof(pictureAdapter));
        }

        public async Task<Picture> UploadPictureAsync(Picture picture)
        {                   
            return await Task.Run(() => picture);
        }


    }
}
