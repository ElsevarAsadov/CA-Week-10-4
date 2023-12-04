namespace PustokBookStore.Areas.Admin.Services
{
    public static class FileManagerService
    {
        public static readonly string DirPath = Path.Combine(Environment.CurrentDirectory, "wwwroot", "slider_images");
        public static string Save(IFormFile file)
        {
            
            
            string guid = Guid.NewGuid().ToString();
            string fileExtension = new FileInfo(file.FileName).Extension;

            string fileName = guid + fileExtension;

            string filePath = Path.Combine(DirPath, fileName);

            using(FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            //not absolute one!
            return fileName;
        }



        public static void Delete(string fileName)
        {
            string filePath = Path.Combine(DirPath, fileName);

            try
            {
                File.Delete(filePath);
            }
            catch (Exception exp)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[WARNING/IGNORE]::Slider File Cannot Be Deleted - {0}\nPlease Check Folder Privileges and File Is Being Used By Other Processes...");
                Console.ResetColor();

            }
        }

    }
}
