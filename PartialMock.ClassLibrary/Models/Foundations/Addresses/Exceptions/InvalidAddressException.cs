// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Xeptions;

namespace PartialMock.ClassLibrary.Models.Foundations.Addresses.Exceptions
{
    public class InvalidAddressException : Xeption
    {
        public InvalidAddressException(string message)
            : base(message)
        { }
    }
}