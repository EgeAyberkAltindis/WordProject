namespace MVC.Models.QuizModels
{
    public class QuizResultViewModel
    {
        public int TotalQuestions { get; set; }
        public int CorrectCount { get; set; }
        public int WrongCount { get; set; }
        public List<QuizResultItemViewModel> WordResults { get; set; } = new();

    }
}
