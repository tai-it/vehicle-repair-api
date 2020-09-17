using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VehicleRepairs.Database.Domain.Contexts;
using VehicleRepairs.Database.Domain.Entities;
using VehicleRepairs.Shared.Common;

namespace VehicleRepairs.Api.Services.Messaging
{
    public interface IFCMService
    {
        Task SendNotifications(List<Notification> notifications);

        List<Notification> GetNotificationsByOrder(Order order);
    }

    public class FCMService : IFCMService
    {
        private readonly ApplicationDbContext db;

        public FCMService(ApplicationDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public List<Notification> GetNotificationsByOrder(Order order)
        {
            switch (order.Status)
            {
                case CommonConstants.OrderStatus.WAITING:
                    return new List<Notification>()
                    {
                        new Notification()
                        {
                            Title = "Đặt cuốc xe thành công",
                            Body = "Vui lòng chờ cửa hàng xác nhận cuốc xe của bạn. Nếu sau 5' vẫn chưa nhận được phải hồi, vui lòng huỷ và đặt lại cuốc xe mới",
                            Type = CommonConstants.NotificationTypes.ORDER_TRACKING,
                            Data = order.Id.ToString(),
                            User = order.User
                        },
                        new Notification()
                        {
                            Title = "Cuốc xe mới",
                            Body = "Bạn có một cuốc mới cách đây " + (order.Distance / 1000) + " km. Địa chỉ: " + order.Address,
                            Type = CommonConstants.NotificationTypes.ORDER_TRACKING,
                            Data = order.Id.ToString(),
                            User = order.Station.User
                        }
                    };
                case CommonConstants.OrderStatus.ACCEPTED:
                    return new List<Notification>()
                    {
                        new Notification()
                        {
                            Title = "Cuốc xe của bạn đã được chấp nhận",
                            Body = "Vui lòng chờ trong giây lát, chúng tôi sẽ liên lạc với bạn ngay",
                            Type = CommonConstants.NotificationTypes.ORDER_TRACKING,
                            Data = order.Id.ToString(),
                            User = order.User
                        },
                        new Notification()
                        {
                            Title = "Bạn đã nhận cuốc xe",
                            Body = "Vui lòng liên hệ với khách hàng để xác nhận thông tin",
                            Type = CommonConstants.NotificationTypes.ORDER_TRACKING,
                            Data = order.Id.ToString(),
                            User = order.Station.User
                        }
                    };
                case CommonConstants.OrderStatus.REJECTED:
                    return new List<Notification>()
                    {
                        new Notification()
                        {
                            Title = "Cuốc xe của bạn đã bị từ chối",
                            Body = "Cuốc xe bị huỷ vì cửa hàng đang bận hoặc bạn ở quá xe. Vui lòng chọn cửa hàng khác",
                            Type = CommonConstants.NotificationTypes.ORDER_TRACKING,
                            Data = order.Id.ToString(),
                            User = order.User
                        }
                    };
                case CommonConstants.OrderStatus.CANCLED:
                    return new List<Notification>()
                    {
                        new Notification()
                        {
                            Title = "Bạn đã huỷ cuốc xe thành công",
                            Body = "Bạn vẫn còn nhiều tiệm xe khác để lựa chọn",
                            Type = CommonConstants.NotificationTypes.ORDER_TRACKING,
                            Data = order.Id.ToString(),
                            User = order.User
                        },
                        new Notification()
                        {
                            Title = "Cuốc xe của bạn đã bị huỷ",
                            Body = "Cuốc xe của bạn đã bị huỷ do khách hàng không nhận được phản hồi từ bạn",
                            Type = CommonConstants.NotificationTypes.ORDER_TRACKING,
                            Data = order.Id.ToString(),
                            User = order.Station.User
                        }
                    };
                case CommonConstants.OrderStatus.DONE:
                    return new List<Notification>()
                    {
                        new Notification()
                        {
                            Title = "Cuốc xe hoàn thành",
                            Body = "Cảm ơn bạn đã tin dùng dịch vụ của chúng tôi. Vui lòng dành vài giây để đánh giá cuốc xe của bạn",
                            Type = CommonConstants.NotificationTypes.ORDER_TRACKING,
                            Data = order.Id.ToString(),
                            User = order.User
                        },
                        new Notification()
                        {
                            Title = "Cuốc xe hoàn thành",
                            Body = "Cảm ơn bạn đã tin dùng dịch vụ của chúng tôi",
                            Type = CommonConstants.NotificationTypes.ORDER_TRACKING,
                            Data = order.Id.ToString(),
                            User = order.Station.User
                        }
                    };
                default:
                    throw new ArgumentException();
            }
        }

        public async Task SendNotifications(List<Notification> notifications)
        {
            foreach (var notify in notifications)
            {
                if (!string.IsNullOrEmpty(notify.User.DeviceToken))
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
                                            notify.IsSent = true;
                                            String sResponseFromServer = tReader.ReadToEnd();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            this.db.Notifications.AddRange(notifications);

            await this.db.SaveChangesAsync();
        }
    }
}