using System.ComponentModel.DataAnnotations;
using Domain.Errors;
using System.Text.RegularExpressions;
using Domain.SharedKernel.Base;

namespace Domain.MaterialManagement.TransactionalPartnerAggregate;

public class ContactInformation : ValueObject
{
    private const byte EmailMaxLength = 200;
    private const byte TelNoMaxLength = 20;
    public string TelNo { get; }
    public string Email { get; }

    private ContactInformation(string telNo, string email)
    {
        TelNo = telNo;
        Email = email;
    }

    protected ContactInformation() { }

    public static Result<ContactInformation> Create(string? telNo, string? email)
    {
        telNo ??= string.Empty;
        email ??= string.Empty;
        
        if ((string.IsNullOrEmpty(telNo) || string.IsNullOrWhiteSpace(telNo)) 
            && (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email)))
            return DomainErrors.ContactPersonInformation.EmptyContact;

        telNo = telNo.Trim();
        email = email.Trim();
        
        if (!string.IsNullOrEmpty(telNo))
        {
            if (telNo.Length > TelNoMaxLength)
                return DomainErrors.ContactPersonInformation.TelNoExceedsMaxLength;

            if (!telNo.All(char.IsDigit))
                return DomainErrors.ContactPersonInformation.TelNoIsNotNumbers;
        }

        if (!string.IsNullOrEmpty(email))
        {
            if (email.Length > EmailMaxLength)
                return DomainErrors.ContactPersonInformation.EmailExceedsMaxLength;
            
            if (!Regex.IsMatch(email, @"^(.+)@(.+)\.\w{2,}$"))
                return DomainErrors.ContactPersonInformation.InvalidEmail;
        }

        return new ContactInformation(telNo, email);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return TelNo;
        yield return Email;
    }

    // [GeneratedRegex("^(.+)@(.+)\.\w{2,}$")]
    // private static partial Regex EmailPattern();
}