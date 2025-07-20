using Model.Entity;

namespace MVC.Models.QuizModels.SessionModels
{
    public class QuizSessionModel
    {
        public List<QuizQuestionViewModel> Questions { get; set; }
        public int CurrentQuestionIndex { get; set; }


        public List<EnglishWord> QuestionsObject { get; set; } = new();
        public int CorrectCount { get; set; } = 0;
        public int WrongCount { get; set; } = 0;

        // Geri bildirim için:
        public bool LastAnswerCorrect { get; set; }
        public string LastSelectedAnswer { get; set; }

        
    }
}
