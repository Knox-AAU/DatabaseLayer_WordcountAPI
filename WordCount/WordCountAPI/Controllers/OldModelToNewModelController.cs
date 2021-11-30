using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WordCount.Data;

namespace WordCount.Controllers
{
    public sealed class OldModelToNewModelController : ControllerBase
    {
        public OldModelToNewModelController()
        {
            this.OldContext = new ArticleContextOld();
            this.NewContext = new ArticleContext();
        }

        public ArticleContext NewContext { get; set; }

        public ArticleContextOld OldContext { get; set; }

        public IActionResult Migrate(string pw)
        {
            if (pw != "please_do_migration")
                return BadRequest();

            var oldArticles = OldContext.Articles;
            var oldPublishers = OldContext.Publishers;

            var articles = NewContext.Articles;
            var publishers = NewContext.Publishers;

            oldArticles.AsQueryable().


            return null;
        }
    }
}