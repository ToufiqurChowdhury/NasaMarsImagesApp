using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using WebSpa.Constants;
using WebSpa.Interfaces;
using WebSpa.Models;

namespace WebSpa.Services
{
    public class FileOperationService : IFileOperationService
    {

        public FileOperationService()
        {
        }

        public ReadFromDatesFileResponse getDatesFromFile(string dataFileName)
        {
            //init datetime list for log entries
            List<string> recordDates = new List<string>();

            //Define Date formats
            string[] formats = {"MM/d/yy", "MM/dd/yy",
                                "MMM-d-yyyy", "MMM-dd-yyyy",
                                "MMMM d, yyyy", "MMMM dd, yyyy",
                                "MMMMM d, yyyy", "MMMMM dd, yyyy"};


            var datesFileName = dataFileName.Trim();
            var datesFileInfo = new FileInfo(datesFileName);
            var datesFilePath = datesFileInfo.FullName;

            string[] stringDates = Array.Empty<string>();
            try
            {
                if (!System.IO.File.Exists(datesFilePath))
                {
                    return new ReadFromDatesFileResponse
                    {
                        statusCode = StatusCode.ResourceExhausted,
                        message = "File not found!",
                    };
                }
                // Read file content
                stringDates = System.IO.File.ReadAllLines(datesFilePath);
            }
            catch (Exception ex)
            {
                return new ReadFromDatesFileResponse
                {
                    statusCode = StatusCode.Internal,
                    message = ex.Message,
                };
            }

            foreach (var stringDate in stringDates)
            {
                DateTime dateValue;
                if (DateTime.TryParseExact(stringDate.Trim(), formats,
                              new CultureInfo("en-US"),
                              DateTimeStyles.None,
                              out dateValue))
                {
                    var validDate = new DateTimeOffset(dateValue);
                    recordDates.Add(validDate.Date.ToString("yyyy-MM-dd"));
                }
            }

            return new ReadFromDatesFileResponse
            {
                imageDates = recordDates,
                statusCode = StatusCode.OK
            };
        }
    }
}
