using Reply.Manager.Images.Domain.Adapters;
using Reply.Manager.Images.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reply.Manager.Images.PictureAdapter
{
    public class PictureAdapter : IPictureAdapter
    {
        private readonly PictureAdapterConfiguration pictureAdapterConfiguration;

        public PictureAdapter(PictureAdapterConfiguration pictureAdapterConfiguration)
        {
            this.pictureAdapterConfiguration = pictureAdapterConfiguration ?? throw new ArgumentNullException(nameof(pictureAdapterConfiguration));
        }

        public async Task<Picture> GetPictureAsync(Picture picture)
        {
            return await Task.Run(() => new Picture { });
        }
    }
}
