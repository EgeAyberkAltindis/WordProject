using BLL.Services.Abstract;
using DAL.Repository.Abstract;
using DAL.Repository.Concretes;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Concretes
{
    public class QuizService :IQuizService
    {
        private readonly IEnglishWordRepository _wordRepository;

        public QuizService(IEnglishWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public async Task<List<EnglishWord>> GetQuizWordsAsync(int count)
        {
            // 1. En az gösterilmiş kelimeleri getir
            var words = await _wordRepository.GetLeastShownWordsAsync(count);

            // 2. TimesShown değerini artır (yalnızca quiz başında bir defa)
            foreach (var word in words)
            {
                word.TimesShown++;
            }

            // 3. Değişiklikleri veritabanına kaydet
            await _wordRepository.SaveChangesAsync();

            // 4. Quiz'e döndür
            return words;
        }

        public async Task<List<string>> GetAllTurkishMeaningsAsync()
        {
            return await _wordRepository.GetAllTurkishMeaningsAsync();
        }

        
        

        
    }
    
}
