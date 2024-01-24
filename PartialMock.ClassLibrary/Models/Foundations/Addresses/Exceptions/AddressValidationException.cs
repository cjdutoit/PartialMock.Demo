// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Xeptions;

namespace PartialMock.ClassLibrary.Models.Foundations.Addresses.Exceptions
{
    public class AddressValidationException : Xeption
    {
        public AddressValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}