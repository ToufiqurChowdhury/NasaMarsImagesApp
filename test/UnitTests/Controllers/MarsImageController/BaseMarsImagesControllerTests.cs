using WebSpa.Controllers.V1;

namespace UnitTests.Controllers
{
    public class BaseMarsImagesControllerTests
    {
        public MarsImageController _marsImageController;

        public BaseMarsImagesControllerTests()
        {
            var mockILogger = Config.MockLoggerService().Object;
            var mockImageDownloadService = Config.MockImageDownloadService().Object;
            var mockIFileOperationService = Config.MockFileOperationService().Object;

            _marsImageController = new MarsImageController(mockILogger, mockImageDownloadService, mockIFileOperationService);
        }
    }
}
