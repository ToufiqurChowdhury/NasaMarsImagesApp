using Apod;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using WebSpa.Constants;
using WebSpa.Interfaces;
using WebSpa.Models;

namespace WebSpa.Services
{
    public class ImageDownloadService : IImageDownloadService
    {
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1);
        private const int threadTimeout = 1000;

        public ImageDownloadService()
        {
        }

        public async Task<SaveImageContentResponse> SaveMarsImageContent(DateTimeOffset requestDate)
        {
            if (string.IsNullOrEmpty(MarsImageConstants.NASA_API_Key))
            {
                return new SaveImageContentResponse
                {
                    statusCode = StatusCode.InvalidArgument,
                    message = "Mising API Key, NASA API Key is required!"
                };
            }

            var _client = new ApodClient(MarsImageConstants.NASA_API_Key);

            try
            {
                var response = await _client.FetchApodAsync(requestDate.Date);

                if (response == null)
                {
                    return new SaveImageContentResponse
                    {
                        statusCode = StatusCode.InvalidArgument,
                        message = "Invalid Response from NASA.",
                    };
                }

                if (response.StatusCode != ApodStatusCode.OK)
                {
                    return new SaveImageContentResponse
                    {
                        statusCode = StatusCode.InvalidArgument,
                        message = response.Error.ErrorMessage,
                    };
                }
                else
                {
                    var savedImageContentResponse = await SaveImageContentAsync(response.Content);

                    if (savedImageContentResponse == null)
                    {
                        return new SaveImageContentResponse
                        {
                            statusCode = StatusCode.ResourceExhausted,
                            message = "The process cannot access resource.",
                        };
                    }

                    return new SaveImageContentResponse
                    {
                        content = savedImageContentResponse,
                        statusCode = StatusCode.OK,
                    };
                }
            }
            catch (Exception ex)
            {
                return new SaveImageContentResponse
                {
                    statusCode = StatusCode.Internal,
                    message = ex.Message,
                };
            }
            finally
            {
                _client.Dispose();
            }
        }
        private async Task<ImageContentResponse> SaveImageContentAsync(ApodContent content)
        {
            if (content.MediaType != MediaType.Image)
            {
                return null;
            }

            var contentUrl = content.ContentUrl;
            var fileName = MarsImageConstants.Image_Name_Prefix + content.Date.ToString("yyyy-MM-dd") + ".jpg";
            var filePath = MarsImageConstants.Image_Folder_Name + fileName;
            filePath = Path.GetFullPath(filePath);

            // Optimization to download image single time
            var imageFileExits = File.Exists(filePath);
            Console.WriteLine("Image file exists: {0}", imageFileExits);

            if (imageFileExits || await ImageDownloadAsync(contentUrl, filePath))
            {
                var savedImageContentResponse = new ImageContentResponse
                {
                    title = content.Title,
                    imageName = fileName,
                    imagePath = filePath,
                    date = content.Date,
                };

                return savedImageContentResponse;
            }
            else
            {
                return null;
            }
        }
        private async Task<bool> ImageDownloadAsync(string imageUrl, string filePathToSave)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePathToSave));

                // "TryEnter" with a wait of up to 1 seconds
                if (File.Exists(filePathToSave) && this.semaphore.Wait(threadTimeout))
                {                    
                    try
                    {
                        File.Delete(filePathToSave);
                    }
                    finally
                    {
                        this.semaphore.Release();
                    }
                }
                using (WebClient webClient = new WebClient())
                {
                    var fileUrl = new Uri(imageUrl);
                    Console.WriteLine("Downloading image to file path: {0}", filePathToSave);
                    webClient.DownloadProgressChanged += WebClientDownloadProgressChanged;
                    webClient.DownloadFileCompleted += WebClientDownloadCompleted;
                    await webClient.DownloadFileTaskAsync(fileUrl, filePathToSave);
                    return File.Exists(filePathToSave);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Image downloaed is not completed!");
                Console.Write(e);
                return false;
            }
        }

        private void WebClientDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.Write("\r   -->    {0}%.", e.ProgressPercentage);
        }

        private void WebClientDownloadCompleted(object sender, AsyncCompletedEventArgs args)
        {
            var result = !args.Cancelled;
            if (!result)
            {
                Console.Write(args.Error.ToString());
            }
            Console.WriteLine(Environment.NewLine + "Download finished!");
        }
    }
}
