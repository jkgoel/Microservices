namespace JKTech.Common.Events
{
    public class UserAuthenticated : IEvent
    {
        public UserAuthenticated(string email)
        {
            Email = email;

        }
        public string Email { get; set; }
        public UserAuthenticated()
        {

        }
    }
}