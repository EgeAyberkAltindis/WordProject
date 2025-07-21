using BLL.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Helper;
using MVC.Models.QuizModels;
using MVC.Models.QuizModels.SessionModels;



namespace MVC.Controllers
{
    public class QuizController : Controller
    {
        private readonly IQuizService _quizService;

        // Quiz session bilgilerini tutacağız
        private const string QuizSessionKey = "QuizSession";

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        // Quiz başlat
        [HttpGet]
        public async Task<IActionResult> Start()
        {
            
            var words = await _quizService.GetQuizWordsAsync(10);

            // 2. Türkçe anlamları al (şıklar için)
            var meanings = await _quizService.GetAllTurkishMeaningsAsync();

            // 3. Kelimeleri soru formatına çevir (ViewModel'e mapleme)
            var questions = QuizQuestionMapper.MapToViewModel(words, meanings);

            // 4. Quiz oturumunu başlat
            var session = new QuizSessionModel
            {
                Questions = questions,
                CurrentQuestionIndex = 0
            };
            
            HttpContext.Session.Set("QuizSession", session);

            // 6. Önizleme sayfasına yönlendir
            return RedirectToAction("Preview");

        }
        [HttpGet]
        public IActionResult Preview()
        {
            var session = HttpContext.Session.Get<QuizSessionModel>("QuizSession");
            if (session == null)
                return RedirectToAction("StartQuiz");

            return View(session);
        }
        [HttpPost]
        public IActionResult StartQuiz()
        {
            var session = TempData.Get<QuizSessionModel>("QuizSession");
            if (session == null)
                return RedirectToAction("Start");

            session.CurrentQuestionIndex = 0;
            TempData.Put("QuizSession", session);

            return RedirectToAction("Question");
        }
        [HttpGet]
        public IActionResult Question()
        {
            var session = HttpContext.Session.Get<QuizSessionModel>("QuizSession");

            // ❗ Session boşsa -> Start'a yönlendir
            if (session == null || session.CurrentQuestionIndex >= session.Questions.Count)
                return RedirectToAction("Result");
            // 🔍 1. Minimum QuizInShown değerini bul

            int minShown = session.Questions.Min(q => q.QuizInShown);

            // 🎯 2. Bu değere sahip tüm soruları al
            var candidates = session.Questions
                                    .Where(q => q.QuizInShown == minShown)
                                    .ToList();

            // 🔀 3. Adaylardan rastgele birini seç
            var random = new Random();
            var selected = candidates[random.Next(candidates.Count)];

            // ➕ 4. Bu sorunun gösterim sayısını artır
            selected.QuizInShown++;

            // 🔁 5. Seçilen soruyu View'e gönder
            HttpContext.Session.SetObjectAsJson("QuizSession", session);
            return View(selected);

            
        }
        public IActionResult Feedback()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Answer(QuizAnswerInputModel answer)
        {
            var session = HttpContext.Session.GetObjectFromJson<QuizSessionModel>("QuizSession");
            if (session == null)
                return RedirectToAction("Start");

            // ✅ 1. Cevaplanan soruyu bul
            var current = session.Questions.FirstOrDefault(q => q.WordId == answer.WordId);
            if (current == null)
                return RedirectToAction("Question"); // Hatalı yönlendirme

            // ✅ 2. Cevap kontrolü
            bool isCorrect = string.Equals(
                answer.SelectedAnswer?.Trim(),
                current.CorrectTurkish?.Trim(),
                StringComparison.OrdinalIgnoreCase
            );

            if (isCorrect)
            {
                session.CorrectCount++;
                current.CorrectCount++;
            }
            else
            {
                session.WrongCount++;
                current.WrongCount++;
            }

            session.LastAnswerCorrect = isCorrect;
            session.LastSelectedAnswer = answer.SelectedAnswer;
            session.LastAnsweredWordId = current.WordId;

            // ✅ 3. Güncellenmiş session'ı sakla
            HttpContext.Session.SetObjectAsJson("QuizSession", session);

            return RedirectToAction("Feedback");

        }

        [HttpPost]
        public IActionResult Finish()
        {
            var session = HttpContext.Session.GetObjectFromJson<QuizSessionModel>("QuizSession");
            if (session == null)
                return RedirectToAction("Start");

            return RedirectToAction("Result");
        }
        public IActionResult Result()
        {
            var session = HttpContext.Session.GetObjectFromJson<QuizSessionModel>("QuizSession");
            if (session == null)
                return RedirectToAction("Start");

            var result = new QuizResultViewModel
            {
                TotalQuestions = session.Questions.Count,
                CorrectCount = session.CorrectCount,
                WrongCount = session.WrongCount,
                WordResults = session.Questions
                    .Select(q => new QuizResultItemViewModel
                    {
                        English = q.EnglishText,
                        Correct = q.CorrectCount,
                        Wrong = q.WrongCount,
                        TimesShown = q.QuizInShown
                    }).ToList()
            };

            HttpContext.Session.Remove("QuizSession");

            return View(result);
        }









    }
}

