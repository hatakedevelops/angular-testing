namespace EmailService.Models
{
    public class ContactForm
    {
        public required string FirstName {get; set;}
        public required string LastName {get; set;}
        public required string Email {get; set;}
        public required string Subject {get; set;}
        public required string Message {get; set;}
    }
}