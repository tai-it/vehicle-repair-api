using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VehicleRepairs.Api.Domain.Contexts;
using VehicleRepairs.Api.Domain.Entities;
using VehicleRepairs.Api.Infrastructure.Common;
using VehicleRepairs.Api.Services.Ordering.Models;

namespace VehicleRepairs.Api.Services.Messaging
{
    public interface IFCMService
    {
        Task SendToDevice(Order order);

        Notification GetNotificationByOrder(Order order);
    }

    public class FCMService : IFCMService
    {
        private readonly ApplicationDbContext db;

        public FCMService(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public Notification GetNotificationByOrder(Order order)
        {
            switch (order.Status)
            {
                case CommonConstants.OrderStatus.WAITING:
                    return new Notification()
                    {
                        Title = "Cuốc xe mới",
                        Body = "Bạn có một cuốc mới cách đây " + (order.Distance / 1000) + " km. Địa chỉ: " + order.Address,
                        Order = order,
                        User = order.Station.User,
                        Targets = new string[] { order.Station.User.DeviceToken }
                    };
                case CommonConstants.OrderStatus.ACCEPTED:
                    return new Notification()
                    {
                        Title = "Cuốc xe của bạn đã được chấp nhận",
                        Body = "Vui lòng chờ trong giây lát, chúng tôi sẽ liên lạc với bạn ngay",
                        Order = order,
                        User = order.User,
                        Targets = new string[] { order.User.DeviceToken }
                    };
                case CommonConstants.OrderStatus.REJECTED:
                    return new Notification()
                    {
                        Title = "Cuốc xe của bạn đã bị từ chối",
                        Body = "Cuốc xe bị huỷ vì cửa hàng đang bận. Vui lòng chọn cửa hàng khác",
                        Order = order,
                        User = order.User,
                        Targets = new string[] { order.User.DeviceToken }
                    };
                case CommonConstants.OrderStatus.CANCLED:
                    return new Notification()
                    {
                        Title = "Bạn đã huỷ cuốc xe thành công",
                        Body = "Bạn vẫn còn nhiều tiệm xe khác để lựa chọn",
                        Order = order,
                        User = order.User,
                        Targets = new string[] { order.User.DeviceToken }
                    };
                case CommonConstants.OrderStatus.DONE:
                    return new Notification()
                    {
                        Title = "Cuốc xe hoàn thành",
                        Body = "Cảm ơn bạn đã tin dùng dịch vụ của chúng tôi. Vui lòng dành vài giây để đánh giá cuốc xe của bạn",
                        Order = order,
                        User = order.User,
                        Targets = new string[] { order.User.DeviceToken, order.Station.User.DeviceToken }
                    };
                default:
                    throw new ArgumentException();
            }
        }

        public async Task SendToDevice(Order order)
        {
            var notify = this.GetNotificationByOrder(order);

            if (notify.Targets.Any())
            {
                WebRequest request = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                request.Method = "post";
                request.Headers.Add(string.Format("Authorization: key={0}", "AAAAY5RUzHw:APA91bGd8rzR7qIsHARftOQrnhiotHrrbfyj0F9bdOXNxdU4ZjS2tGDnQ1xDv7wKFXujvmKGB-4r6KUZ84mr0dqog-dYdEiP7rDVLZEXOCHR_oqUoC870Cyy-klOtLH2P5tiCBXx0pnU"));
                request.Headers.Add(string.Format("Sender: id={0}", "427690347644"));
                request.ContentType = "application/json";

                var payload = new
                {
                    registration_ids = notify.Targets,
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
                using (Stream dataStream = request.GetRequestStream())
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
                                }
                            }
                        }
                    }
                }
            }

            this.db.Notifications.Add(notify);

            await this.db.SaveChangesAsync();
        }
    }
}
