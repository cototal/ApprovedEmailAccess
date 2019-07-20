namespace Cototal.AspNetCore.ApprovedEmailAccess.Middleware
{
    public class VerifyAdminEmailOptions : IVerifyAdminEmailOptions
    {
        public VerifyAdminEmailOptions(string adminEmails)
        {
            AdminEmails = adminEmails.Split(';');
        }

        public string[] AdminEmails { get; set; }
    }
}
