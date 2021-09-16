using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Controllers
{
    public class GetImageOfTheDayTests : BaseMarsImagesControllerTests
    {
        public GetImageOfTheDayTests() : base()
        {
        }

        [Fact]
        public async Task GetImageOfTheDayTest_Success()
        {
            var requestDate = Config.validImageDate;
            await ExecuteValidation(requestDate, "");
        }

        [Fact]
        public async Task GetImageOfTheDayTest_InvalidDate()
        {
            var requestDate = Config.invalidImageDate;
            await ExecuteValidation(requestDate, "Invalid Date!");
        }

        [Fact]
        public async Task GetImageOfTheDayTest_FutureDate()
        {
            var requestDate = Config.futureImageDate;
            await ExecuteValidation(requestDate, "Invalid Date!");
        }

        private async Task ExecuteValidation(DateTimeOffset requestDate, string expectedErrorMsg = "")
        {
            //Init in base constructor

            //Act
            Func<Task> method = async () => await _marsImageController.GetImageOfTheDay(requestDate);
            
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
