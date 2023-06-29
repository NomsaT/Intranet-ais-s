using DevExtreme.AspNet.Mvc.FileManagement;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    public class ImportFilesController : Controller
    {
        public ImportFilesController(IHostingEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;
        }
        public IHostingEnvironment HostingEnvironment { get; }
        public object FileSystem(FileSystemCommand command, string arguments)
        {
            var config = new FileSystemConfiguration
            {
                Request = Request,
                FileSystemProvider = new PhysicalFileSystemProvider(HostingEnvironment.WebRootPath + @"/Manage"),
                AllowCopy = false,
                AllowCreate = false,
                AllowMove = false,
                AllowDelete = false,
                AllowRename = false,
                AllowUpload = true,
                AllowDownload = true,
                AllowedFileExtensions = new[] { ".csv", ".txt" }
            };
            var processor = new FileSystemCommandProcessor(config);
            var result = processor.Execute(command, arguments);
            return result.GetClientCommandResult();
        }
    }
}


