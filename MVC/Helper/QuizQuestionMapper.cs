using Model.Entity;
using MVC.Models.QuizModels;

namespace MVC.Helper
{
    public static class QuizQuestionMapper
    {
        public static List<QuizQuestionViewModel> MapToViewModel(
        List<EnglishWord> words, List<string> allMeanings)
        {
            var random = new Random();
            var result = new List<QuizQuestionViewModel>();

            foreach (var word in words)
            {
                var correct = word.WordMeaning.FirstOrDefault()?.TurkıshWord?.Text ?? "Bilinmiyor";

                var wrong = allMeanings.Where(x => x != correct).OrderBy(_ => random.Next()).Take(3).ToList();
                wrong.Add(correct);

                var options = wrong.OrderBy(_ => random.Next()).ToList();

                result.Add(new QuizQuestionViewModel
                {
                    WordId = word.Id,
                    EnglishText = word.Text,
                    TurkishOptions = options,
                    CorrectTurkish = correct
                });
            }

            return result;
        }
    }
}
