namespace JKTech.Common.Events
{
    public class CreateUserRejected : IRejectedEvent
    {
        public CreateUserRejected(string email, string code, string reason)
        {
            Email = email;
            Reason = reason;
            Code = code;

        }
        public string Email { get; }
        public string Reason { get; }
        public string Code { get; }

        protected CreateUserRejected()
        {

        }
    }
}