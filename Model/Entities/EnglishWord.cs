using Model.Concretes;
using Model.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class EnglishWord:BaseEntity
    {

        public string Text { get; set; }
        public int InQuizShown { get; set; }
       
        public int TimesShown { get; set; } = 0;
        public string? WordType { get; set; }
        public ICollection<WordMeaning> WordMeaning { get; set; }
        public ICollection<EnglishSentence> EnglishSentence { get; set; }
    }
}
