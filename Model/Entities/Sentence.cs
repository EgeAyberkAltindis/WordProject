using Model.Abstract;
using Model.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Sentence:BaseEntity
    {

        
        public string Text { get; set; }

        //mapping 
        public ICollection<EnglishSentence> EnglishSentence { get; set; }
    }
}
