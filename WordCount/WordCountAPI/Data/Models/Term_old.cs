using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WordCount.Data.DataAccess;

namespace WordCount.Data.Models
{
    [Table("Term")]
<<<<<<< Updated upstream:WordCount/WordCountAPI/Data/Models/Term_old.cs
    public sealed class Term_OLD : DatabaseEntityModel<CompositeKeyPair<long, string>>
=======
    public sealed class Term : DatabaseEntityModel<string>
>>>>>>> Stashed changes:WordCount/WordCountAPI/Data/Models/Term.cs
    {
        [Key]
        public string Literal { get; set; }
        public List<WordOccurance> Occurances { get; set; }

        public override string PrimaryKey => Literal;
    }
}