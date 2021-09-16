using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Controllers
{
    public class GetDatesTests : BaseMarsImagesControllerTests
    {
        public GetDatesTests() : base()
        {
        }

        [Fact]
        public async Task GetDatesTest_Success()
        {
            await ExecuteValidation("");
        }

        private async Task ExecuteValidation(string expectedErrorMsg = "")
        {
            //Init in base constructor            

            //Act
            Func<Task> method = async () => await _marsImageController.GetDates();

            //Assert
            if (!string.IsNullOrEmpty(expectedErrorMsg))
            {
                var ex = await method.Should().ThrowAsync<Exception>();
                ex.Which.Message.Should().Contain(expectedErrorMsg);
            }
            else
            {
                await method.Should().NotThrowAsync<Exception>();
            }
        }

    }
}
