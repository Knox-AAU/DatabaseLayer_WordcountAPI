using WordCount.JsonModels;
using WordCount.Models;

namespace WordCount.Controllers
{
    public static class JsonDbUtility
    {
        public static FileListModel ArticleToFileList(Article article, long sourceId)
        {
            return new FileListModel
            {
                SourceId = sourceId,
                ArticleTitle = article.ArticleTitle,
                FilePath = article.FilePath,
                TotalWordsInArticle = article.TotalWordsInArticle
            };
        }

        public static WordListModel WordDataToWordList(WordData wordData)
        {
            return new WordListModel
            {
                WordName = wordData.Word,
            };
        }

        public static AppearsInModel ArticleWordDataToAppearsIn(Article article, WordData wordData)
        {
            return new AppearsInModel
            {
                Amount = wordData.Amount,
                WordName = wordData.Word,
                ArticleTitle = article.ArticleTitle,
                FilePath = article.FilePath
            };
        }
    }
}