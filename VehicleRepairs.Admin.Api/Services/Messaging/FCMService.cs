namespace VehicleRepairs.Admin.Api.Services.Messaging
{
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using VehicleRepairs.Database.Domain.Contexts;
    using VehicleRepairs.Database.Domain.Entities;

    public interface IFCMService
    {
        Task SendNotification(Notification notification);
    }

    public class FCMService : IFCMService
    {
        private readonly ApplicationDbContext db;

        public FCMService(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task SendNotification(Notification notify)
        {
            try
            {
                WebRequest request = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                request.Method = "post";
                request.Headers.Add(string.Format("Authorization: key={0}", "AAAAY5RUzHw:APA91bGd8rzR7qIsHARftOQrnhiotHrrbfyj0F9bdOXNxdU4ZjS2tGDnQ1xDv7wKFXujvmKGB-4r6KUZ84mr0dqog-dYdEiP7rDVLZEXOCHR_oqUoC870Cyy-klOtLH2P5tiCBXx0pnU"));
                request.Headers.Add(string.Format("Sender: id={0}", "427690347644"));
                request.ContentType = "application/json";

                var payload = new
                {
                    to = notify.Target,
                    priority = "high",
                    content_available = true,
                    notification = new
                    {
                        title = notify.Title,
                        body = notify.Body,
                        sound = "default"
                    },
                    data = new
                    {
                        id = notify.Id.ToString()
                    }
                };

                string postbody = JsonConvert.SerializeObject(payload).ToString();
                Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
                request.ContentLength = byteArray.Length;
                await using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse response = request.GetResponse())
                    {
                        using (Stream dataStreamResponse = response.GetResponseStream())
                        {
                            if (dataStreamResponse != null)
                            {
                                using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    notify.IsSent = true;
                                    String sResponseFromServer = tReader.ReadToEnd();
                                }
                            }
                        }
                    }
                }
            } catch (Exception e)
            {
                throw e;
            }
            this.db.Add(notify);
            await this.db.SaveChangesAsync();
        }
    }
}
