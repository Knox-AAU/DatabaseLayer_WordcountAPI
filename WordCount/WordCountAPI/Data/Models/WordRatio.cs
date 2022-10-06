namespace WordCount.Data.Models;

public class WordRatio
{
    public WordRatio()
    {
    }

    public WordRatio(int documentId, string word)
    {
        DocumentId = documentId;
        Word = word;
    }

    public WordRatio(int amount, int documentId, double percent, Ranks rank, string word)
    {
        Amount = amount;
        DocumentId = documentId;
        Percent = percent;
        Rank = rank;
        Word = word;
    }

    public int Amount { get; init; }
    public int DocumentId { get; init; }
    public double Percent { get; init; }
    public Ranks Rank { get; init; }
    public string Word { get; init; }
}

public enum Ranks
{
    Body, Synopsis, Subtitle, Title
}
