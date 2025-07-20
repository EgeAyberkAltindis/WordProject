namespace MVC.Models.QuizModels
{
    public class QuizQuestionViewModel
    {
        public int WordId { get; set; }
        public string EnglishText { get; set; }
        public List<string> TurkishOptions { get; set; } // Çoktan seçmeli seçenekler
        public string CorrectTurkish { get; set; } // Doğru cevap (kontrol için)
    }
}
