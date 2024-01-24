// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using FluentAssertions;
using Moq;
using PartialMock.ClassLibrary.Models.Foundations.Addresses.Exceptions;
using PartialMock.ClassLibrary.Services.Foundations.Addresses;
using Xunit;

namespace PartialMock.ClassLibrary.Tests.Unit.Services.Foundations.Addresses
{
    public partial class AddressServiceTests
    {
        [Fact]
        public async Task ShouldThrowServiceExceptionOnCleanAddressIfServiceErrorOccursAndLogItAsync()
        {
            // given
            var mock = new Mock<AddressService> { CallBase = true };
            string someAddress = GetRandomString();
            var serviceException = new Exception();
            mock.Setup(x => x.ValidateAddress(It.IsAny<string>())).Throws(serviceException);
            AddressService addressService = mock.Object;

            var failedAddressServiceException =
                new FailedAddressServiceException(
                    message: "Failed address service error occurred, contact support.",
                    innerException: serviceException);

            var expectedAddressServiceException =
                new AddressServiceException(
                    message: "Address service error occurred, contact support.",
                    innerException: failedAddressServiceException);

            // when
            ValueTask<string> addAddressTask =
                addressService.CleanAddress(someAddress);

            AddressServiceException actualAddressServiceException =
                await Assert.ThrowsAsync<AddressServiceException>(
                    addAddressTask.AsTask);

            // then
            actualAddressServiceException.Should()
                .BeEquivalentTo(expectedAddressServiceException);
        }
    }
}