﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.AspNetCore.Http;

namespace inz.Controllers
{
    public class BlobsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private CloudBlobContainer GetCloudBlobContainer(string containerName)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            IConfigurationRoot Configuration = builder.Build();
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(Configuration["ConnectionStrings:pracainzynierska_AzureStorageConnectionString"]);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            return container;
        }

        public string UploadBlob(string fileName, IFormFile file, string containerName)
        {
            CloudBlobContainer container = GetCloudBlobContainer(containerName);
            CloudBlockBlob blob = container.GetBlockBlobReference(fileName);
            using (var fileStream = file.OpenReadStream())
            {
                blob.UploadFromStreamAsync(fileStream).Wait();
            }
            return "success!";
        }

        public string DeleteBlob (string fileName, string containerName)
        {
            CloudBlobContainer container = GetCloudBlobContainer(containerName);
            CloudBlockBlob blob = container.GetBlockBlobReference(fileName);
            blob.DeleteIfExistsAsync();
            return "succes!";
        }
    }
}