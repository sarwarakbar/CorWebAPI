using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Model;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore.Query;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using System.Data;

namespace CoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileTransfer : ControllerBase
    {
        private async Task<string> WriteFile(IFormFile file)
        {
            string fileName = "";
            try
            {
                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");

                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                //Get file extension              
                fileName = file.FileName;

                var fileNameWithPath = Path.Combine(pathBuilt, fileName);

                if (System.IO.File.Exists(fileNameWithPath) == true)
                {
                    return "File already exist.";
                }
                else
                {

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }

            catch (Exception)
            {
            }

            return fileName + " is uploaded successfully";
        }


        [HttpPost]
        [Route("UploadFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken cancellationToken)
        {
            var result = await WriteFile(file);
            return Ok(result);
        }

        [HttpGet]
        [Route("DownloadFile")]
        public async Task<ActionResult> DownloadFile(string Namefile)
        {
            //...Code for validation and get the file
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files", Namefile);
            if (System.IO.File.Exists(filePath))
            {
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(filePath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }
                var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
                return File(bytes, contentType, Path.GetFileName(filePath));
            }
            else
            {
                return Ok("File not found.");
            }
        }

    }
}
