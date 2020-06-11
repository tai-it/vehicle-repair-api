namespace VehicleRepairs.Api.Infrastructure.ActionResults
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using VehicleRepairs.Api.Infrastructure.Common;

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
