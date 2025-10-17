using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Common.Servicies.Attachments
{
    public  interface IAttachmentService
    {   
              //3shan htrg3 esm el sora 
        public string Upload(IFormFile File ,string FolderName );
        public bool Delete(string FilePath );
    }


}
