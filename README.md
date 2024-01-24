# PartialMock

This is a demo project to show how you can test exception handling on self-fulfilling / dead-end services by doing a partial mock with the Moq framework.

The tests look the same until we get to the exception tests.  Here is some code snippets to show what I have done.

###1) Make the project internal visible to the unit test

```
	<ItemGroup>
		<InternalsVisibleTo Include="PartialMock.ClassLibrary.Tests.Unit" />
		<InternalsVisibleTo Include="DynamicProxyGenAssembly2" />
	</ItemGroup>
```
###2) Create the service as normal
####AddressService.cs
```cs
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
```

####AddressService.Validations.cs   
Instead of using `private static void ValidateAddress(string address)` we change this to `virtual internal void ValidateAddress(string address)`.  With the library internally visible to the unit test project, Moq will now be able to see this, allowing us to explicitly setup alternate actions.
```cs
    public partial class AddressService
    {
        virtual internal void ValidateAddress(string address)
        {
            Validate((Rule: IsInvalid(address), Parameter: "Address"));
        }

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidAddressException =
                new InvalidAddressException(
                    message: "Invalid address. Please correct the errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidAddressException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidAddressException.ThrowIfContainsErrors();
        }
    }
```

###3) AddressServiceTests.Exceptions.CleanAddress.cs
Here we create a mock of the concrete implementation with an initializer block that sets the CallBase property of the mock object to true. 
When `CallBase` is set to `true`, it means that the base implementation of the methods in the mocked class (AddressService in this case) will be called if a method is not explicitly set up in the mock.
Next we use this line `mock.Setup(x => x.ValidateAddress(It.IsAny<string>())).Throws(serviceException);` to specify that the `ValidateAddress` method should throw an exception so we can test the TryCatch.

```cs
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
```

In summary, this code creates a mock object for the AddressService class with the added behavior that, by default, it will call the actual implementation of the methods if no specific setup is provided for a method call on the mock. This can be useful when you want to **partially** mock a class, allowing some methods to execute their real implementation while mocking others for testing purposes.
