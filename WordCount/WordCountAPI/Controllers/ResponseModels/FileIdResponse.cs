namespace WordCount.Controllers
{
    public sealed class FileIdResponse
    {
        public string FilePath { get; set; }
        
        public FileIdResponse(string filePath)
        {
            FilePath = filePath;
        }
    }
}