// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Xeptions;

namespace PartialMock.ClassLibrary.Models.Foundations.Addresses.Exceptions
{
    public class FailedAddressServiceException : Xeption
    {
        public FailedAddressServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}