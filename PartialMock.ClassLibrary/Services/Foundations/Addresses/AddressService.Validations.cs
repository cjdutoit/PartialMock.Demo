// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using PartialMock.ClassLibrary.Models.Foundations.Addresses.Exceptions;

namespace PartialMock.ClassLibrary.Services.Foundations.Addresses
{
    public partial class AddressService
    {
        virtual internal void ValidateAddress(string address)
        {
            Validate((Rule: IsInvalid(address), Parameter: "Address"));
        }

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidAddressException =
                new InvalidAddressException(
                    message: "Invalid address. Please correct the errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidAddressException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidAddressException.ThrowIfContainsErrors();
        }
    }
}
