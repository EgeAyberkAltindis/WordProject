using Model.Concretes;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class WordMeaning:BaseEntity
    {
        public int EnglisWordId { get; set; }
        public EnglishWord EnglishWord { get; set; }
        public int TurkishWordId { get; set; }
        public TurkıshWord TurkıshWord { get; set; }
    }
}
