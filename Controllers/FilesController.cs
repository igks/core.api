using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CORE.API.Controllers
{
    [Route("api/[controller]")]
    [RequestSizeLimit(100_000_000)]
    public class FilesController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public FilesController(IHostingEnvironment environment)
        {
            this.hostingEnvironment = environment;
        }

        [HttpPost("create")]
        public IActionResult Create()
        {
            var fileCreated = DateTime.Now.ToString(@"dd-MM-yyyy, HH-mm-ss");
            var fileName = "backup file " + fileCreated + ".txt";
            var uploads = Path.Combine(hostingEnvironment.WebRootPath, "Uploads");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }
            var filePath = Path.Combine(uploads, fileName);
            using (FileStream newFile = System.IO.File.Create(filePath))
            {
                byte[] fileContent = new UTF8Encoding(true).GetBytes("This is the backup file created by system");
                newFile.Write(fileContent, 0, fileContent.Length);
            }


            return Ok();
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var uploads = Path.Combine(hostingEnvironment.WebRootPath, "Uploads");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }
            if (file.Length > 0)
            {
                var filePath = Path.Combine(uploads, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            return Ok();
        }

        [HttpGet("download")]
        public async Task<IActionResult> Download([FromQuery] string file)
        {
            var uploads = Path.Combine(hostingEnvironment.WebRootPath, "Uploads");
            var filePath = Path.Combine(uploads, file);
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, GetContentType(filePath), file);
        }

        [HttpGet("file-list")]
        public IActionResult Files()
        {
            var result = new List<string>();

            var uploads = Path.Combine(hostingEnvironment.WebRootPath, "Uploads");
            if (Directory.Exists(uploads))
            {
                var provider = hostingEnvironment.ContentRootFileProvider;
                foreach (string fileName in Directory.GetFiles(uploads))
                {
                    var fileInfo = provider.GetFileInfo(fileName);
                    result.Add(fileInfo.Name);
                }
            }
            return Ok(result);
        }

        [HttpDelete("delete")]
        public IActionResult Remove([FromQuery] string file)
        {
            var uploads = Path.Combine(hostingEnvironment.WebRootPath, "Uploads");
            var filePath = Path.Combine(uploads, file);
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            System.IO.File.Delete(filePath);

            return Ok();
        }

        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}