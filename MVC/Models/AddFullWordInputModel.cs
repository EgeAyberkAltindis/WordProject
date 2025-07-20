namespace MVC.Models
{
    public class AddFullWordInputModel
    {
        //        Influence : etki / etkilemek
        //Social media has a strong influence on teenagers. / His speech influenced many people.
       
        public string English { get; set; }
        
        public List<string> TurkishList { get; set; } 

        public List<string> Sentences { get; set; } 
    }
}
