using EasyMoq.Tests.TestModelClasses;
using FluentAssertions;
using Xunit;

namespace EasyMoq.Tests
{
    public class RelatedMockTests : BaseServiceTest<IInterface2, Class2>
    {
        [Fact]
        public void TestedService_TestOriginalClassMethodCalling()
        {
            var result = GetTestedService().Method1_UsingClass1Method1();

            result.Should().Be($"{nameof(Class1)}.Method1");
        }

        [Fact]
        public void TestedService_TestMockedClassMethodCalling()
        {
            const string testValue = "MockTest";
            var testedService = GetTestedService();

            GetRelatedMock<Class1>().Setup(x => x.Method1()).Returns(testValue);

            var result = testedService.Method1_UsingClass1Method1();

            result.Should().Be(testValue);
        }

        [Fact]
        public void TestedService_TestOriginalInterfaceMethodCalling()
        {
            var result = GetTestedService().Method3_UsingClass3Method1();

            // When calling an un-mocked method on a mocked interface, it should not run, and return the default (string -> null).
            // This will change when using CoupleInterfaceWithClass() in the Prepare() method. Will be tested separately. 
            result.Should().BeNull();
        }

        [Fact]
        public void TestedService_TestMockedInterfaceMethodCalling()
        {
            const string testValue = "MockTest";
            var testedService = GetTestedService();

            GetRelatedMock<IInterface3>().Setup(x => x.Method1()).Returns(testValue);

            var result = testedService.Method3_UsingClass3Method1();

            result.Should().Be(testValue);
        }

    }
}
