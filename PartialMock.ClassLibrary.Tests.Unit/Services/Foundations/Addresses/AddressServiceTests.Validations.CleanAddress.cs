// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using FluentAssertions;
using PartialMock.ClassLibrary.Models.Foundations.Addresses.Exceptions;
using Xunit;

namespace PartialMock.ClassLibrary.Tests.Unit.Services.Foundations.Addresses
{
    public partial class AddressServiceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnCleanAddressIfAddressIsNullAndLogItAsync(string invalidText)
        {
            // given
            string invalidAddress = invalidText;

            var invalidAddressException =
                new InvalidAddressException(message: "Invalid address. Please correct the errors and try again.");

            invalidAddressException.AddData(
                key: "Address",
                values: "Text is required");

            var expectedAddressValidationException =
                new AddressValidationException(
                    message: "Address validation errors occurred, please try again.",
                    innerException: invalidAddressException);

            // when
            ValueTask<string> addAddressTask =
                this.addressService.CleanAddress(invalidAddress);

            AddressValidationException actualAddressValidationException =
                await Assert.ThrowsAsync<AddressValidationException>(() =>
                    addAddressTask.AsTask());

            // then
            actualAddressValidationException.Should()
                .BeEquivalentTo(expectedAddressValidationException);
        }
    }
}