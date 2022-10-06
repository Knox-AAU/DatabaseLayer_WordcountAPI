namespace WordCount.Data.Models;

public class DocumentContents
{
    public DocumentContents()
    {
    }

    public DocumentContents(string content, int documentId)
    {
        Content = content;
        DocumentId = documentId;
    }

    public string Content { get; init; }
    public int DocumentId { get; init; }
}
