namespace VolunteerPlatform.Domain.Common;

public record Error(string Code, string Message);

public static class Errors
{
    public static class User
    {
        public static Error PhoneNumberIsTaken(string phoneNumber) =>
            new("user.phoneNumber.is.taken", $"User phone number '{phoneNumber} is taken");
    }

    public static class General
    {
        public static Error NotFound(Guid? id = null)
        {
            var forId = id == null ? "" : $"for Id '{id}'";
            return new("record.not.found", $"Record not found for Id '{forId}'");
        }

        public static Error ValueIsInvalid() =>
            new("value.is.invalid", "Value is invalid");

        public static Error ValueIsRequired() =>
            new("value.is.required", "Value is required");

        public static Error InvalidLength(string? name = null)
        {
            var label = name == null ? " " : " " + name + " ";
            return new("invalid.string.length", $"Invalid{label}length");
        }
    }
}