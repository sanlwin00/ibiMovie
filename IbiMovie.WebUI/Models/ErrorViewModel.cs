namespace IbiMovie.WebUI.Models
{
    public class ErrorViewModel
    {
        public Exception? Exception { get; set; }

        public ErrorViewModel(Exception? exception)
        { 
            this.Exception = exception; 
        }
        
    }
}