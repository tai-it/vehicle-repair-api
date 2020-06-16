using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using VehicleRepairs.Api.Domain.Entities;
using VehicleRepairs.Api.Infrastructure.Common;
using VehicleRepairs.Api.Services.Ordering.Models;

namespace VehicleRepairs.Api.Services.Notification
{
    public interface IFCMService
    {
        void SendToDevice(Order order);

        object GetNotificationByOrder(Order order);
    }

    public class FCMService : IFCMService
    {
        public object GetNotificationByOrder(Order order)
        {
            switch (order.Status)
            {
                case CommonConstants.OrderStatus.WAITING:
                    return new
                    {
                        to = order.Station.User.DeviceToken,
                        priority = "high",
                        content_available = true,
                        notification = new
                        {
                            title = "Cuốc xe mới",
                            body = "Bạn có một cuốc mới cách đây " + (order.Distance / 1000) + " km. Địa chỉ: " + order.Address,
                            sound = "default"
                        },
                        data = new
                        {
                            order = JsonConvert.SerializeObject(new OrderDetailViewModel(order)).ToString()
                        }
                    };
                case CommonConstants.OrderStatus.ACCEPTED:
                    return new
                    {
                        to = order.User.DeviceToken,
                        priority = "high",
                        content_available = true,
                        notification = new
                        {
                            title = "Cuốc xe của bạn đã được chấp nhận",
                            body = "Vui lòng chờ trong giây lát, chúng tôi sẽ liên lạc với bạn ngay",
                            sound = "default"
                        },
                        data = new
                        {
                            order = JsonConvert.SerializeObject(new OrderDetailViewModel(order)).ToString()
                        }
                    };
                case CommonConstants.OrderStatus.REJECTED:
                    return new
                    {
                        to = order.User.DeviceToken,
                        priority = "high",
                        content_available = true,
                        notification = new
                        {
                            title = "Cuốc xe của bạn đã bị từ chối",
                            body = "Cuốc xe bị huỷ vì cửa hàng đang bận. Vui lòng chọn cửa hàng khác",
                            sound = "default"
                        },
                        data = new
                        {
                            order = JsonConvert.SerializeObject(new OrderDetailViewModel(order)).ToString()
                        }
                    };
                case CommonConstants.OrderStatus.CANCLED:
                    return new
                    {
                        to = order.User.DeviceToken,
                        priority = "high",
                        content_available = true,
                        notification = new
                        {
                            title = "Bạn đã huỷ cuốc xe thành công",
                            body = "Bạn vẫn còn nhiều tiệm xe khác để lựa chọn",
                            sound = "default"
                        },
                        data = new
                        {
                            order = JsonConvert.SerializeObject(new OrderDetailViewModel(order)).ToString()
                        }
                    };
                case CommonConstants.OrderStatus.DONE:
                    return new
                    {
                        to = new string[] { order.User.DeviceToken, order.Station.User.DeviceToken },
                        priority = "high",
                        content_available = true,
                        notification = new
                        {
                            title = "Cuốc xe hoàn thành",
                            body = "Cảm ơn bạn đã tin dùng dịch vụ của chúng tôi",
                            sound = "default"
                        },
                        data = new
                        {
                            order = JsonConvert.SerializeObject(new OrderDetailViewModel(order)).ToString()
                        }
                    };
                default:
                    throw new ArgumentException();
            }
        }

        public void SendToDevice(Order order)
        {
            if (order.User.DeviceToken == null || order.Station.User.DeviceToken == null)
            {
                return;
            }

            WebRequest request = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            request.Method = "post";
            request.Headers.Add(string.Format("Authorization: key={0}", "AAAAY5RUzHw:APA91bGd8rzR7qIsHARftOQrnhiotHrrbfyj0F9bdOXNxdU4ZjS2tGDnQ1xDv7wKFXujvmKGB-4r6KUZ84mr0dqog-dYdEiP7rDVLZEXOCHR_oqUoC870Cyy-klOtLH2P5tiCBXx0pnU"));
            request.Headers.Add(string.Format("Sender: id={0}", "427690347644"));
            request.ContentType = "application/json";

            var payload = this.GetNotificationByOrder(order);

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
                        if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                            }
                    }
                }
            }
        }
    }
}
