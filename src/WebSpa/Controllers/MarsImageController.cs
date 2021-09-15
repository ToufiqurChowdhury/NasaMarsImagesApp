using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using WebSpa.Constants;
using WebSpa.Interfaces;
using WebSpa.Models;

namespace WebSpa.Controllers.V1
{
    [ApiController]
    [Route("v1/[controller]")]
    public class MarsImageController : ControllerBase
    {

        private readonly ILogger<MarsImageController> _logger;
        private readonly IImageDownloadService _imageDownloadService;
        private readonly IFileOperationService _fileOperationService;

        public MarsImageController(ILogger<MarsImageController> logger, IImageDownloadService imageDownloadService, IFileOperationService fileOperationService)
        {
            _logger = logger;
            _imageDownloadService = imageDownloadService;
            _fileOperationService = fileOperationService;
        }

        /// <summary>
        /// Get saved image content information
        /// </summary>
        /// <param name="requestDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces200OKAttribute]
        [Produces400ValidationErrorAttribute]
        public async Task<IActionResult> GetImageOfTheDay([FromQuery]DateTimeOffset requestDate)
        {
            var response = await _imageDownloadService.SaveMarsImageContent(requestDate);

            if (response.statusCode != Grpc.Core.StatusCode.OK)
            {
                return BadRequest(response.message);
            }

            return Ok(response.content);
        }


        /// <summary>
        /// Get image dates as a list
        /// </summary>
        /// <returns></returns>
        [HttpGet("dates")]
        [Produces200OKAttribute]
        [Produces400ValidationError]
        public async Task<IActionResult> GetDates()
        {
            var response = _fileOperationService.getDatesFromFile(MarsImageConstants.Dates_File_Name);

            if (response.statusCode != Grpc.Core.StatusCode.OK)
            {
                return BadRequest(response.message);
            }

            return Ok(response.imageDates);
        }
    }
}
