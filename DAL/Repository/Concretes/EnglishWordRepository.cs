
using DAL.Context;
using DAL.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using Model.Entity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace DAL.Repository.Concretes
{
    public class EnglishWordRepository:IEnglishWordRepository
    {
        private readonly WordContext _context;

        public EnglishWordRepository(WordContext context)
        {
            _context = context;
        }

        public IQueryable<EnglishWord> GetAllWithIncludes()
        {
            
            return _context.EnglishWords.Include(x => x.WordMeaning).ThenInclude(x => x.TurkıshWord).Include(X => X.EnglishSentence).ThenInclude(x => x.Sentence);
       
        }

        public async Task<List<string>> GetAllTurkishMeaningsAsync()
        {
            return await _context.TurkıshWords.Select(t => t.Text).Distinct().ToListAsync();
        }

        public async Task<List<EnglishWord>> GetWordsByIdsAsync(IEnumerable<int> wordIds)
        {
            return await _context.EnglishWords
                .Where(w => wordIds.Contains(w.Id))
                .ToListAsync();
        }

        public async Task<List<EnglishWord>> GetLeastShownWordsAsync(int count)
        {
            return await _context.EnglishWords
                .OrderBy(w => w.TimesShown)
                .ThenBy(_ => Guid.NewGuid()) // Aynı TimesShown varsa karıştır
                .Take(count)
                .Include(w => w.WordMeaning)
                    .ThenInclude(wm => wm.TurkıshWord)
                .Include(w => w.EnglishSentence)
                    .ThenInclude(es => es.Sentence)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    } 
    
}
