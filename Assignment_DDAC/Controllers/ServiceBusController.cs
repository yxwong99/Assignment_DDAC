using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;
using Assignment_DDAC.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Azure.ServiceBus.Core;

namespace Assignment_DDAC.Controllers
{
    public class ServiceBusController : Controller
    {

        const string ServiceBusConnectionString = "Endpoint=sb://enquiry123.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=/nCBigzIxdWka3h/6rk++YcctAJ0IePyEkshBVDW+Nw=";
        const string QueueName = "testQueue";

        public async Task<ActionResult> Index()
        {
            var managementClient = new ManagementClient(ServiceBusConnectionString);
            var queue = await managementClient.GetQueueRuntimeInfoAsync(QueueName);
            ViewBag.MessageCount = queue.MessageCount;
            return View();
        }

        [HttpPost] // after fill in the form
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(Enquiry order)
        {
            QueueClient queue = new QueueClient(ServiceBusConnectionString, QueueName);
            if (ModelState.IsValid)
            {
                var orderJSON = JsonConvert.SerializeObject(order);
                var message = new Message(Encoding.UTF8.GetBytes(orderJSON))
                {
                    MessageId = Guid.NewGuid().ToString(),
                    ContentType = "application/json"
                };
                await queue.SendAsync(message);
                return RedirectToAction("Index", "servicebus", new { });
            }
            return View(order);
        }

        private static async Task CreateQueueFunctionAsync()
        {
            var managementClient = new ManagementClient(ServiceBusConnectionString);
            bool queueExists = await managementClient.QueueExistsAsync(QueueName);
            if (!queueExists)
            {
                QueueDescription qd = new QueueDescription(QueueName);
                qd.MaxSizeInMB = 1024;
                qd.MaxDeliveryCount = 3;
                await managementClient.CreateQueueAsync(qd);
            }
        }

        public static void Initialize()
        {
            CreateQueueFunctionAsync().GetAwaiter().GetResult();
        }

        public async Task<ActionResult> ReceivedMessage()
        {
            var managementClient = new ManagementClient(ServiceBusConnectionString);
            var queue = await managementClient.GetQueueRuntimeInfoAsync(QueueName);
            List<Enquiry> messages = new List<Enquiry>();
            List<long> sequence = new List<long>();
            MessageReceiver messageReceiver = new MessageReceiver(ServiceBusConnectionString,
            QueueName);
            for (int i = 0; i < queue.MessageCount; i++)
            {
                Message message = await messageReceiver.PeekAsync();
                Enquiry result = JsonConvert.DeserializeObject<Enquiry>(Encoding.UTF8.GetString(message.Body));
                sequence.Add(message.SystemProperties.SequenceNumber);
                messages.Add(result);
            }
            ViewBag.sequence = sequence;
            ViewBag.messages = messages;
            return View();
        }

        public async Task<ActionResult> Approve(long sequence)
        {
            //connect to the same queue
            var managementClient = new ManagementClient(ServiceBusConnectionString);
            var queue = await managementClient.GetQueueRuntimeInfoAsync(QueueName);
            //receive the selected message
            MessageReceiver messageReceiver = new MessageReceiver(ServiceBusConnectionString, QueueName);
            Enquiry result = null;
            for (int i = 0; i < queue.MessageCount; i++)
            {
                Message message = await messageReceiver.ReceiveAsync();
                string token = message.SystemProperties.LockToken;
                //to find the selected message - read and remove from the queue
                if (message.SystemProperties.SequenceNumber == sequence)
                {
                    result = JsonConvert.DeserializeObject<Enquiry>(Encoding.UTF8.GetString(message.Body));
                    await messageReceiver.CompleteAsync(token);
                    break;
                }
            }
            return RedirectToAction("ReceiveMessaged", "ServiceBus");
        }
    }

    
}
