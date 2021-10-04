using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Model;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly DevcanDbContext _dbContext;
        private IWebHostEnvironment _webHostEnvironment;
        public HomeController( DevcanDbContext devcanDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = devcanDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<bool> DeleteAllRecord()
        {
            try
            {
                var allImages = await _dbContext.NewImages.ToListAsync();
                _dbContext.NewImages.RemoveRange(allImages);
                await _dbContext.SaveChangesAsync();

                var rootDirectory = _webHostEnvironment.ContentRootPath;
                
                var folder_path = @"C:\ExtractedImages";
                if (System.IO.Directory.Exists(folder_path))
                {
                    System.IO.Directory.Delete(folder_path, true);
                }




                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }



        [HttpGet]
        public async Task<bool> ExtractAll()
        {
            try
            {
                var oldImages = await _dbContext.OldImages.ToListAsync();

                var fileNameI = 1;
                List<string> existedNames = new List<string>();
                List<ImageObj> newImages = new List<ImageObj>();

                foreach (var item in oldImages)
                {

                    var newImage = new ImageObj();

                    newImage.application_id = item.application_id;
                    newImage.description = item.description;
                    newImage.document_type_id = item.document_type_id;

                    if (oldImages.Where(a => a.filename == item.filename).Count() > 1)
                    {
                        fileNameI = newImages.Where(a => a.oldFileName == item.filename).Count() + 1;
                        var splitNames = item.filename.Split(".", StringSplitOptions.RemoveEmptyEntries);
                        if (splitNames.Length > 1)
                        {
                            newImage.filename = splitNames[0] + $"({fileNameI})" + splitNames[1];
                        }
                        else
                        {
                            newImage.filename = splitNames[0] + $"({fileNameI})";
                        }
                    }
                    else
                    {
                        newImage.filename = item.filename;
                    }

                    //newImage.image_id = item.image_id;
                    newImage.last_download_obua_id = item.last_download_obua_id;
                    newImage.last_download_timestamp = item.last_download_timestamp;
                    newImage.oldFileName = item.filename;
                    newImage.Record_Create_Date = item.Record_Create_Date;
                    newImage.upload_date = item.upload_date;
                    newImage.upload_obua_id = item.upload_obua_id;
                    newImage.image = item.image;
                    newImages.Add(newImage);

                }



                foreach (var item in newImages)
                {
                    var rootDirectory = _webHostEnvironment.ContentRootPath;
                    var folder_path = @$"C:\ExtractedImages\{item.application_id}";

                   
                    if (!Directory.Exists(folder_path))
                    {
                        Directory.CreateDirectory(folder_path);
                    }

                    using (var stream = new FileStream(folder_path + $"/{item.filename}", FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        stream.Write(item.image);
                    }

                    item.path = folder_path + $"/{item.filename}";
                }


                await _dbContext.NewImages.AddRangeAsync(newImages);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
           

        }
    }
}
