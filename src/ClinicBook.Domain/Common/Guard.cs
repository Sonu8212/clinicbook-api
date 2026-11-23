namespace ClinicBook.Domain.Common;

internal static class Guard
{
    public static string NotEmpty(string? value, string name, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException($"{name} is required.");
        }

        value = value.Trim();

        if (value.Length > maxLength)
        {
            throw new DomainException($"{name} must be {maxLength} characters or fewer.");
        }

        return value;
    }
}
