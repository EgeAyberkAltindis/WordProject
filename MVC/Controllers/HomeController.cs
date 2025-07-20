using BLL.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Entity;
using MVC.Helper;
using MVC.Models;
using MVC.Models.RequestModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEnglishWordService _englishWordService;
        private readonly IServiceManager<EnglishWord> _englishService;
        private readonly IServiceManager<TurkýshWord> _turkishService;
        private readonly IServiceManager<Sentence> _sentenceService;
        private readonly IServiceManager<WordMeaning> _wordMeaningService;
        private readonly IServiceManager<EnglishSentence> _englishSentenceService;
      





        public HomeController(ILogger<HomeController> logger, IEnglishWordService englishWordService, IServiceManager<EnglishWord> englishServiceManager, IServiceManager<TurkýshWord> turkishServiceManager,IServiceManager<Sentence>sentenceServiceManager,IServiceManager<WordMeaning> meaningServiceManager,IServiceManager<EnglishSentence> englishSentenceServiceManager)
        {
            _logger = logger;
            _englishWordService = englishWordService;
            _englishService = englishServiceManager;
            _turkishService= turkishServiceManager;
            _sentenceService = sentenceServiceManager;
            _wordMeaningService = meaningServiceManager;
            _englishSentenceService = englishSentenceServiceManager;
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult CreateWord() 
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateWord(AddFullWordRequestModel requestModel)
        {
            var input = WordInputParser.Parse(requestModel.RawInput);

            var englishWordResponse = input.English;
            var turkishWordResponseList =input.TurkishList;
            var sentenceResponseList = input.Sentences;

            var enlishWord = new EnglishWord { Text =input.English.Trim()};
            await _englishService.CreateAsync(enlishWord);
       
            foreach (var t in input.TurkishList)
            {
                var turkishWord = new TurkýshWord { Text = t.Trim() };
                    await _turkishService.CreateAsync(turkishWord);

                var relation = new WordMeaning
                {
                    EnglisWordId = enlishWord.Id,
                    TurkishWordId = turkishWord.Id,
                };
                await _wordMeaningService.CreateAsync(relation);
            }

            foreach (var s in input.Sentences)
            {
                
                var sentence = new Sentence { Text = s.Trim() };
                await _sentenceService.CreateAsync(sentence);

                var relation = new EnglishSentence
                {
                    EnglishWordId = enlishWord.Id,
                    SentenceId = sentence.Id,
                };

                await _englishSentenceService.CreateAsync(relation);
            
            }
            TempData["Message"] = "Kelime baþarýyla eklendi!";
            return RedirectToAction("CreateWord");
        }

        public async Task<IActionResult> GetWord()
        {
            var query = _englishWordService.GetAllWithIncludes();
            var words = await query.ToListAsync();

            return View(words);
        }



        public IActionResult CreateRangeWord()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRangeWord(string rawInput)
        {
            var wordBlocks = WordInputParser.ParseMultiple(rawInput);

            foreach (var model in wordBlocks)
            {
                // 1. Ýngilizce kelimeyi ekle
                var enlishWord = new EnglishWord { Text = model.English.Trim() };
                await _englishService.CreateAsync(enlishWord);

                // 2. Türkçe anlamlarý ekle ve iliþkilendir
                foreach (var t in model.TurkishList)
                {
                    var turkishWord = new TurkýshWord { Text = t.Trim() };
                    await _turkishService.CreateAsync(turkishWord);

                    var relation = new WordMeaning
                    {
                        EnglisWordId = enlishWord.Id,
                        TurkishWordId = turkishWord.Id,
                    };
                    await _wordMeaningService.CreateAsync(relation);
                }

                // 3. Cümleleri ekle ve iliþkilendir
                foreach (var s in model.Sentences)
                {

                    var sentence = new Sentence { Text = s.Trim() };
                    await _sentenceService.CreateAsync(sentence);

                    var relation = new EnglishSentence
                    {
                        EnglishWordId = enlishWord.Id,
                        SentenceId = sentence.Id,
                    };

                    await _englishSentenceService.CreateAsync(relation);

                }


            }
            TempData["Message"] = "Kelime baþarýyla eklendi!";
            return RedirectToAction("CreateRangeWord");
        }
    }
    
}
