using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using MusicProject.Models;
using Microsoft.Extensions.FileProviders;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace MusicProject.Controllers
{
    [Authorize]
    public class UploadController : Controller
    {
        
        private readonly UploadContext _context;
        public UploadController(UploadContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index() { return View(await _context.Files.ToListAsync()); }

        [HttpPost]
        public IActionResult Index(IFormFile files)
        {
            if (files != null)
            {
                if (files.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(files.FileName);
                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);
                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                    // Combines two strings into a path.
                    var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadAudioFiles")).Root + $@"\{newFileName}";

                    var objfiles = new Files()
                    {
                        DocumentId = 0,
                        Name = newFileName,
                        FileType = fileExtension,
                        FilePath = "~/UploadAudioFiles/" + newFileName,
                        CreatedOn = DateTime.Now
                    };

                    using (var target = new MemoryStream())
                    {
                        files.CopyTo(target);
                        objfiles.DataFiles = target.ToArray();
                    }

                    _context.Files.Add(objfiles);
                    _context.SaveChanges();

                    using (FileStream fs = System.IO.File.Create(filepath))
                    {
                        files.CopyTo(fs);
                        fs.Flush();
                    }

                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id,string fp)
        {
            var employee = await _context.Files.FindAsync(id);
            _context.Files.Remove(employee);
            await _context.SaveChangesAsync();
            var fullpath = @"C:\Users\Gayathri\source\repos\MusicProject\MusicProject\wwwroot\UploadAudioFiles\" + fp;
            if (System.IO.File.Exists(fullpath)) { 

                System.IO.File.Delete(fullpath);
            }
            return RedirectToAction(nameof(Index));
        }

        
       
    }
}
