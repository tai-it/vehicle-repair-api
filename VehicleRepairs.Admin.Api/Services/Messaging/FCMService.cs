namespace VehicleRepairs.Admin.Api.Services.Messaging
{
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using VehicleRepairs.Database.Domain.Entities;

    public interface IFCMService
    {
        Task<bool> SendNotification(Notification notification);
    }

    public class FCMService : IFCMService
    {
        public async Task<bool> SendNotification(Notification notify)
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
                    to = notify.User.DeviceToken,
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
                                    String sResponseFromServer = tReader.ReadToEnd();
                                    return true;
                                }
                            }
                        }
                    }
                }
            } catch (Exception e)
            {
                throw e;
            }
            return false;
        }
    }
}
