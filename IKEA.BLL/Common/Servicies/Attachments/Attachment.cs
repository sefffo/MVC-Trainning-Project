using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Common.Servicies.Attachments
{
    public class Attachment : IAttachmentService
    {
        private readonly List<string> AllowedFiles = new List<string>()
        {
            ".jpg" ,".png"  ,".jpeg", ".pdf"
        };
        const int FileMaxSize = 2097152;
        public string Upload(IFormFile File, string FolderName)
        {
            //ywdi el attachment llfolder el matlob , w yrg3 esmaha 
            //1-get the extennsion then check it and check on its size 
            var fileExtension = Path.GetExtension(File.FileName);
            if (!AllowedFiles.Contains(fileExtension))
            {
                throw new Exception("invalid file extension");
            }
            if (File.Length > FileMaxSize)
            {
                throw new Exception("invalid file Size");
            }
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", FolderName); //genaric by a ertain way 
            if (!Directory.Exists(FolderPath))
            {

                //lw msh mawgood ht3ml el path da w t7ot feeh el sora 
                Directory.CreateDirectory(FolderPath);
            }
            //we need a unique file names ===> using GUID
            var FileName = $"{Guid.NewGuid()}_{File.FileName}";
            var filePath = Path.Combine(FolderPath, FileName); //3shan yb2a m3ak el file path belzbt 
            //unmanged code 
            using var fs = new FileStream(filePath, FileMode.Create);
            File.CopyTo(fs); //3shan t7ot el file fe el stream 
            return FileName;

        }
        public bool Delete(string FilePath)
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
                return true;
            }
            return false;
        }


    }
}
