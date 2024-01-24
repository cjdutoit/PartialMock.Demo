// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

namespace PartialMock.ClassLibrary.Services.Foundations.Addresses
{
    public partial class AddressService : IAddressService
    {
        public ValueTask<string> CleanAddress(string address) =>
            TryCatch(async () =>
            {
                ValidateAddress(address);

                return await Task.Run(() =>
                {
                    var cleanedAddress = address.Trim();

                    return cleanedAddress;
                });
            });
    }
}
