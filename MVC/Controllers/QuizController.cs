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

            var question = session.Questions[session.CurrentQuestionIndex];
            return View(question);
        }
        public IActionResult Feedback()
        {
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> Answer(QuizAnswerInputModel answer)
        {

            var session = HttpContext.Session.GetObjectFromJson<QuizSessionModel>("QuizSession");
            if (session == null)
                return RedirectToAction("Start");

            var current = session.Questions[session.CurrentQuestionIndex];
            bool isCorrect = string.Equals(answer.SelectedAnswer?.Trim(), current.CorrectTurkish?.Trim(), StringComparison.OrdinalIgnoreCase);


            if (isCorrect)
                session.CorrectCount++;
            else
                session.WrongCount++;

            session.LastAnswerCorrect = isCorrect;
            session.LastSelectedAnswer = answer.SelectedAnswer;
            session.CurrentQuestionIndex++;

            // 🔁 Tüm sorular gösterildiyse
            if (session.CurrentQuestionIndex >= session.Questions.Count)
            {
                    // Soruları tekrar başlat
                    session.CurrentQuestionIndex = 0;
                    session.Questions = ShuffleHelper.Shuffle(session.Questions); // opsiyonel

                    HttpContext.Session.SetObjectAsJson("QuizSession", session); // tekrar yaz
                    return RedirectToAction("Question");
               
            }

            HttpContext.Session.SetObjectAsJson("QuizSession", session);
            return RedirectToAction("Feedback");

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
                WrongCount = session.WrongCount
                // AverageScore çıkarıldı
            };

            HttpContext.Session.Remove("QuizSession");

            return View(result);
        }









    }
}

