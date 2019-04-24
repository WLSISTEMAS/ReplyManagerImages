using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Otc.AspNetCore.ApiBoot;

namespace Reply.Manager.Images.WebApi.Controllers
{
    [ApiVersion("1.0")]
    public class FilesController : ApiController
    {
        private readonly string _uploadFolder;

        public FilesController(IHostingEnvironment hostingEnvironment)
        {
            _uploadFolder = $"{hostingEnvironment.WebRootPath}\\Upload";
        }


        [Route("upload")]
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Post(List<IFormFile> files, string name, string description)
        {
            var size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream($"{_uploadFolder}\\{formFile.FileName}", FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            return Ok(new { count = files.Count, size });
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