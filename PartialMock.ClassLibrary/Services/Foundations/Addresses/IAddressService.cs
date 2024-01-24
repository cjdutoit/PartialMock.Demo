// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

namespace PartialMock.ClassLibrary.Services.Foundations.Addresses
{
    public interface IAddressService
    {
        ValueTask<string> CleanAddress(string address);
    }
}
