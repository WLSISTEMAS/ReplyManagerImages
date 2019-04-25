using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache memoryCache;        

        public PictureAdapter(IMemoryCache memoryCache)
        {            
            this.memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public async Task<Picture> CreatePictureAsync(Picture picture)
        {
            var cacheEntry = await memoryCache.GetOrCreateAsync(Cache.CacheKeys.CachePicture, entry => 
            {
                return Task.Run(() => new Picture
                {
                    Description = picture.Description,
                    File = picture.File,
                    Id = picture.Id,
                    ImageName = picture.ImageName
                });
            });

            return cacheEntry;
        }

    }
}
