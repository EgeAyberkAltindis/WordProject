using Microsoft.EntityFrameworkCore;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Abstract
{
    public interface IEnglishWordRepository
    {
       
        IQueryable<EnglishWord> GetAllWithIncludes();
        Task<List<string>> GetAllTurkishMeaningsAsync();
        Task<List<EnglishWord>> GetWordsByIdsAsync(IEnumerable<int> wordIds);
        Task<List<EnglishWord>> GetLeastShownWordsAsync(int count);
        Task SaveChangesAsync();


    }
}
