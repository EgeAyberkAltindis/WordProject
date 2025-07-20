using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstract
{
     public interface IQuizService
    {
       

        Task<List<EnglishWord>> GetQuizWordsAsync(int wordCount);
        
        Task<List<string>> GetAllTurkishMeaningsAsync();
        

        


    }
}
