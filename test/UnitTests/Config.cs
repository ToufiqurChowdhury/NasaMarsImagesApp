using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpa.Interfaces;
using Moq;
using WebSpa.Models;
using Castle.Core.Logging;
using Grpc.Core;
using WebSpa.Controllers.V1;
using Microsoft.Extensions.Logging;

namespace UnitTests
{
    public static class Config
    {
        public static readonly DateTimeOffset validImageDate = DateTimeOffset.UtcNow.Date;
        public static readonly DateTimeOffset invalidImageDate = DateTimeOffset.MinValue;
        public static readonly DateTimeOffset futureImageDate = DateTimeOffset.Parse("2025-01-01");

        public static Mock<ILogger<MarsImageController>> MockLoggerService()
        {
            var mockILogger = new Mock<ILogger<MarsImageController>> (MockBehavior.Strict);

            return mockILogger;
        }

        public static Mock<IImageDownloadService> MockImageDownloadService()
        {
            var mockImageDownloadService = new Mock<IImageDownloadService>(MockBehavior.Strict);

            mockImageDownloadService.Setup(x => x.SaveMarsImageContent(validImageDate))
                .ReturnsAsync((DateTimeOffset requestDate) => new SaveImageContentResponse
                {
                    content = new ImageContentResponse
                    {
                        date = requestDate,
                        imageName = "SampleImage",
                        imagePath = "SampleImage.jpg",
                        title = "NASA Mars Rover Image"
                    },
                    statusCode = StatusCode.OK,
                });

            mockImageDownloadService.Setup(x => x.SaveMarsImageContent(invalidImageDate))
                .ReturnsAsync((DateTimeOffset requestDate) => new SaveImageContentResponse
                {
                    statusCode = StatusCode.InvalidArgument,
                    message = "Invalid Date!"
                });

            mockImageDownloadService.Setup(x => x.SaveMarsImageContent(futureImageDate))
                .ReturnsAsync((DateTimeOffset requestDate) => new SaveImageContentResponse
                {
                    statusCode = StatusCode.InvalidArgument,
                    message = "Invalid Date!"
                });

            return mockImageDownloadService;
        }
        public static Mock<IFileOperationService> MockFileOperationService()
        {
            var mockFileOperationService = new Mock<IFileOperationService>(MockBehavior.Strict);

            List<String> resultDates = new List<string>{"06-02-2028", "06-02-2028" };
            mockFileOperationService.Setup(x => x.getDatesFromFile(It.IsAny<string>()))
                .Returns(new ReadFromDatesFileResponse
                {
                   imageDates = resultDates,
                   statusCode = StatusCode.OK
                });

            return mockFileOperationService;
        }
    }
}
