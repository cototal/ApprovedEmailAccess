namespace Cototal.AspNetCore.ApprovedEmailAccess.Middleware
{
    public interface IVerifyAdminEmailOptions
    {
        string[] AdminEmails { get; set; }
    }
}
