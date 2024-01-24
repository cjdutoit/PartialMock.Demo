// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using FluentAssertions;
using Xunit;

namespace PartialMock.ClassLibrary.Tests.Unit.Services.Foundations.Addresses
{
    public partial class AddressServiceTests
    {
        [Fact]
        public async Task ShouldCleanAddressAsync()
        {
            // given
            string randomAddress = GetRandomString();
            string inputAddress = randomAddress;
            string expectedAddress = inputAddress;

            // when
            string actualAddress = await this.addressService
                .CleanAddress(inputAddress);

            // then
            actualAddress.Should().BeEquivalentTo(expectedAddress);
        }
    }
}