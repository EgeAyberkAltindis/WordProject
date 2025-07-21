namespace MVC.Models.QuizModels
{
    public class QuizQuestionViewModel
    {
        public int WordId { get; set; }
        public string EnglishText { get; set; }
        public List<string> TurkishOptions { get; set; } // Çoktan seçmeli seçenekler
        public string CorrectTurkish { get; set; } // Doğru cevap (kontrol için)



        // 🆕 Yeni Alanlar
        public int QuizInShown { get; set; } = 0;         // Bu quizde kaç defa gösterildi
        public int CorrectCount { get; set; } = 0;        // Kaç kez doğru yapıldı
        public int WrongCount { get; set; } = 0;
    }
}
