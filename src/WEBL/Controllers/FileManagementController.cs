using DevExtreme.AspNet.Mvc.FileManagement;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    public class FileManagementController : Controller
    {
        public FileManagementController(IHostingEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;
        }
        public IHostingEnvironment HostingEnvironment { get; }
        public object FileSystem(FileSystemCommand command, string arguments)
        {
        
            var config = new FileSystemConfiguration
            {
                Request = Request,
               
                FileSystemProvider = new PhysicalFileSystemProvider(
                   HostingEnvironment.WebRootPath + @"/Manage"  //this creates a folder under the root the wwwrootfolder
                ),
                AllowCopy = true,
                AllowCreate = true,
                AllowMove = true,
                AllowDelete = true,
                AllowRename = true,
                AllowUpload = true,
                AllowDownload = true,
                AllowedFileExtensions = new[] { ".csv", ".txt", ".tif",".gif" ,".ai",".psd",".svg" ,".docx", ".PNG", ".jpeg", ".jpg", ".pdf" }
            };
            var processor = new FileSystemCommandProcessor(config);
            var result = processor.Execute(command, arguments);
            return result.GetClientCommandResult();
        }
        string GetFileItemUrl(FileSystemInfo fileSystemItem)
        {
            var relativeUrl = fileSystemItem.FullName
                .Replace(HostingEnvironment.WebRootPath, "")
                .Replace(Path.DirectorySeparatorChar, '/');
            return $"{Request.Scheme}://{Request.Host}{Request.PathBase}{relativeUrl}";
        }
    }
}


