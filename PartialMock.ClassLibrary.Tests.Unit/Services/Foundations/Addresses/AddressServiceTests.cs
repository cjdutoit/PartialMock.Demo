// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using PartialMock.ClassLibrary.Services.Foundations.Addresses;
using Tynamix.ObjectFiller;

namespace PartialMock.ClassLibrary.Tests.Unit.Services.Foundations.Addresses
{
    public partial class AddressServiceTests
    {
        private readonly IAddressService addressService;

        public AddressServiceTests()
        {
            this.addressService = new AddressService();
        }

        private static string GetRandomString() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();


        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();
    }
}