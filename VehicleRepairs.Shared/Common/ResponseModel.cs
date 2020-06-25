namespace VehicleRepairs.Shared.Common
{
    using System.Net;

    public class ResponseModel
    {
        public HttpStatusCode StatusCode { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }
}
