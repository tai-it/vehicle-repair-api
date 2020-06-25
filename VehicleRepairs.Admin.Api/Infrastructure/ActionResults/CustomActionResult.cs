namespace VehicleRepairs.Admin.Api.Infrastructure.ActionResults
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using VehicleRepairs.Shared.Common;

    public class CustomActionResult : IActionResult
    {
        private readonly ResponseModel _responseModel;

        public CustomActionResult(ResponseModel responseModel)
        {
            _responseModel = responseModel;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            ObjectResult objectResult;
            switch (_responseModel.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                case System.Net.HttpStatusCode.Created:
                    objectResult = new ObjectResult(_responseModel.Data)
                    {
                        StatusCode = (int)_responseModel.StatusCode
                    };
                    break;
                case System.Net.HttpStatusCode.NotFound:
                    objectResult = new ObjectResult(_responseModel.Message)
                    {
                        StatusCode = (int)_responseModel.StatusCode
                    };
                    break;
                case System.Net.HttpStatusCode.Unauthorized:
                    objectResult = new ObjectResult("Vui lòng đăng nhập để tiếp tục")
                    {
                        StatusCode = (int)System.Net.HttpStatusCode.Unauthorized,
                    };
                    break;
                case System.Net.HttpStatusCode.Forbidden:
                    objectResult = new ObjectResult(_responseModel.Message)
                    {
                        StatusCode = (int)System.Net.HttpStatusCode.Forbidden,
                    };
                    break;
                default:
                    objectResult = new ObjectResult(_responseModel.Message)
                    {
                        StatusCode = (int)_responseModel.StatusCode
                    };
                    break;
            }
            await objectResult.ExecuteResultAsync(context);
        }
    }
}
