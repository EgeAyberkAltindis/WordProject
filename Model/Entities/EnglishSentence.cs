using Model.Concretes;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class EnglishSentence:BaseEntity
    {
        public int EnglishWordId { get; set; }
        public EnglishWord EnglishWord { get; set; }

        public int SentenceId { get; set; }
        public Sentence Sentence { get; set; }
    }
}
