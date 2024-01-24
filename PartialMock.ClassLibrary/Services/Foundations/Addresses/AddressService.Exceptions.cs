// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using PartialMock.ClassLibrary.Models.Foundations.Addresses.Exceptions;
using Xeptions;

namespace PartialMock.ClassLibrary.Services.Foundations.Addresses
{
    public partial class AddressService
    {
        private delegate ValueTask<string> ReturningStringFunction();

        private async ValueTask<string> TryCatch(ReturningStringFunction returningStringFunction)
        {
            try
            {
                return await returningStringFunction();
            }
            catch (InvalidAddressException invalidAddressException)
            {
                throw CreateAndLogValidationException(invalidAddressException);
            }
            catch (Exception exception)
            {
                var failedAddressServiceException =
                    new FailedAddressServiceException(
                        message: "Failed address service error occurred, contact support.",
                        exception);

                throw CreateAndLogServiceException(failedAddressServiceException);
            }
        }

        private AddressValidationException CreateAndLogValidationException(Xeption exception)
        {
            var addressValidationException =
                new AddressValidationException(
                    message: "Address validation errors occurred, please try again.",
                    innerException: exception);

            return addressValidationException;
        }

        private AddressServiceException CreateAndLogServiceException(Xeption exception)
        {
            var addressServiceException = new
                AddressServiceException(
                message: "Address service error occurred, contact support.",
                innerException: exception);

            return addressServiceException;
        }
    }
}
