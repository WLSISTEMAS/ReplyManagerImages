using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Otc.AspNetCore.ApiBoot;
using Reply.Manager.Images.Domain.Models;
using Reply.Manager.Images.Domain.Services;
using Reply.Manager.Images.WebApi.Dtos;

namespace Reply.Manager.Images.WebApi.Controllers
{
    [ApiVersion("1.0")]
    public class FilesController : ApiController
    {
        private readonly IPictureService pictureService;

        public FilesController(IPictureService pictureService)
        {
            this.pictureService = pictureService ?? throw new ArgumentNullException(nameof(pictureService));
        }

        [Route("upload")]
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Post(List<IFormFile> files, string name, string description)
        {
            var file = files.FirstOrDefault();

            byte[] fileBytes = CreateImageByte(file);

            PicturePost picturePost = new PicturePost
            {
                ImageName = name,
                Description = description,
                File = Convert.ToBase64String(fileBytes)
            };

            Picture pictureMap = Mapper.Map<PicturePost, Picture>(picturePost);

            Picture picture = await pictureService.UploadPictureAsync(pictureMap);

            PicturePostResult pictureGetResults =
                Mapper.Map<Picture, PicturePostResult>(picture);

            return Ok(pictureGetResults);
        }

       

        private static byte[] CreateImageByte(IFormFile file)
        {
            MemoryStream stream = new MemoryStream();

            file.CopyTo(stream);

            var fileBytes = stream.ToArray();

            return fileBytes;
        }

        [Route("download")]
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot/upload", filename);

            var memory = new MemoryStream();

            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;

            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                 {".png", "image/png"},
            };
        }

    }
}