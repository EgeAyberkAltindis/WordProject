using Model.Concretes;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class TurkıshWord:BaseEntity
    {

        public string Text { get; set; }

        
        //mapping 
        public int WordMeaningId { get; set; }
        public ICollection<WordMeaning> WordMeaning { get; set; }
    }
}
