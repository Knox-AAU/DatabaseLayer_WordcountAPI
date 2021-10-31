namespace WordCount.Controllers.ResponseModels
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