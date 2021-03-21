using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_DDAC.Controllers
{
    public class BlobsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private CloudBlobContainer getBlobStorageInformation()
        {
            //step 1: read json
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");
            IConfigurationRoot configure = builder.Build();

            //step2 
            //to get key access
            //once link, time to read the content to get the connectionstring
            CloudStorageAccount objectaccount =
            CloudStorageAccount.Parse(configure["ConnectionString:BlobStorageConnection"]);

            //step3: find container to refer
            CloudBlobClient blobclient = objectaccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobclient.GetContainerReference("testblob");

            return container;
        }

        public ActionResult CreateNewContainer()
        {
            //link the container with correct access key
            CloudBlobContainer container = getBlobStorageInformation();

            //check container exist? 
            ViewBag.result = container.CreateIfNotExistsAsync().Result;

            //get container name so that it can show in the page telling which container is successfully created? 
            ViewBag.result = container.Name;

            return View();
        }

        public string UploadBlob()
        {
            CloudBlobContainer container = getBlobStorageInformation();
            
            for(int i = 1; i <= 1000; i++)
            {
                CloudBlockBlob blob = container.GetBlockBlobReference("testing" + i);

                using (var fileStream = System.IO.File.OpenRead(@"C:\\Users\\ILLEGEAR\\Desktop\\my file\\APU\\
                                                                    4. Year 3\\DDAC\\images\" + i + ".jpg"))
                {
                    blob.UploadFromStreamAsync(fileStream).Wait();
                }
            }
            return "success";
        }

        public ActionResult ListLogo()
        {
            CloudBlobContainer container = getBlobStorageInformation();
            //Step 2: create the empty list to store for the blobslist information
            List<string> blobs = new List<string>();
            //step 3: get the listing record from the blob storage
            BlobResultSegment result =
            container.ListBlobsSegmentedAsync(null).Result;
            //step 4: to read blob listing from the storage
            foreach (IListBlobItem item in result.Results)
            {
                //step 4.1. check the type of the blob : block blob or directory or page block
            if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    blobs.Add(blob.Name + "#" + blob.Uri.ToString());
                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory blob =
                    (CloudBlobDirectory)item;
                    blobs.Add(blob.Uri.ToString());
                }
            }
            return View(blobs);
        
        }
    }
}
