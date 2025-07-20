
using BLL.Services.Abstract;
using DAL.Context;
using DAL.Repository.Abstract;
using DAL.Repository.Concretes;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Entity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Concretes
{
    public class EnglishWordService:ServiceManager<EnglishWord>,IEnglishWordService
    {
        private readonly IRepository<EnglishWord> _englishRepo;
        private readonly IRepository<TurkıshWord> _turkishRepo;
        private readonly IRepository<Sentence> _sentenceRepo;
        private readonly IRepository<WordMeaning> _engTrRepo;
        private readonly IRepository<EnglishSentence> _engSentRepo;
        private readonly IEnglishWordRepository _englishWordRepo;

     

        private readonly IRepository<EnglishWord> _repository;
        public EnglishWordService(IRepository<EnglishWord> repository, IRepository<EnglishWord> englishRepo,
        IRepository<TurkıshWord> turkishRepo,
        IRepository<Sentence> sentenceRepo,
        IRepository<WordMeaning> engTrRepo,
        IRepository<EnglishSentence> engSentRepo
        ,IEnglishWordRepository englishWordRepo) :base(repository) 
        {
            _repository = repository;
            _englishRepo = englishRepo;
            _turkishRepo = turkishRepo;
            _sentenceRepo = sentenceRepo;
            _engTrRepo = engTrRepo;
            _engSentRepo = engSentRepo;
           
            _englishWordRepo=englishWordRepo;

        }



        public async Task<bool> EnglishWordExistsAsync(string word)
        {
            return await _repository.AnyAsync(x => x.Text.ToLower() == word.ToLower());
        }

        public IQueryable<EnglishWord> GetAllWithIncludes()
        {
            return _englishWordRepo.GetAllWithIncludes();
        }

        



    }
}


