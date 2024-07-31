using Tekton.Services.Interface;

namespace Tekton.Services
{
    public class LogService: ILogService
    {
        private readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        
        public void Write(string message)
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, "ExcutionTime.txt"), true))
            {
                outputFile.WriteLine(message);
            }
        }
    }
}
