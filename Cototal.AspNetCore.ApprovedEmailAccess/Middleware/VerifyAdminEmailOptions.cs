namespace Cototal.AspNetCore.ApprovedEmailAccess.Middleware
{
    /// <summary>
    /// Options for the VerifyAdminEmail service
    /// </summary>
    public class VerifyAdminEmailOptions : IVerifyAdminEmailOptions
    {
        /// <param name="adminEmails">List of emails you want to allow access</param>
        /// <param name="separator">Separator used between emails in the list</param>
        public VerifyAdminEmailOptions(string adminEmails, char separator = ';')
        {
            AdminEmails = adminEmails.Split(separator);
        }

        public string[] AdminEmails { get; set; }
    }
}
