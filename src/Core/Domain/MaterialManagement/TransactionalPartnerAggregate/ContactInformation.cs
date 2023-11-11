using System.Text.RegularExpressions;
using Domain.SharedKernel.Base;

namespace Domain.MaterialManagement.TransactionalPartnerAggregate;

public class ContactInformation : ValueObject
{
    private static byte EmailMaxLength => 200;
    private static byte TelNoMaxLength => 20;

    //https://frugalcafe.beehiiv.com/p/reuse-regular-expressions
    //https://www.youtube.com/watch?v=RSFiiKUvzLI&ab_channel=NickChapsas
    private static readonly Regex EmailPattern = new(@"^(.+)@(.+)\.\w{2,}$", 
        RegexOptions.Compiled,
        //88.69 ns From Benchmark.RegexBenchmarks
        TimeSpan.FromMilliseconds(100));

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

            if (!EmailPattern.IsMatch(email))
                return DomainErrors.ContactPersonInformation.InvalidEmail;
        }

        return new ContactInformation(telNo, email);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return TelNo;
        yield return Email;
    }
}