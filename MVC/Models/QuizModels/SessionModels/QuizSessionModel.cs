using Model.Entity;

namespace MVC.Models.QuizModels.SessionModels
{
    public class QuizSessionModel
    {
        public List<QuizQuestionViewModel> Questions { get; set; }
        public int CurrentQuestionIndex { get; set; }
        public int CorrectCount { get; set; } = 0;
        public int WrongCount { get; set; } = 0;
        public int? LastAnsweredWordId { get; set; } // feedback için

        // Geri bildirim için:
        public bool LastAnswerCorrect { get; set; }
        public string LastSelectedAnswer { get; set; }

        
    }
}
