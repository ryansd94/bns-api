using BNS.Domain;
using BNS.Domain.Interface;
using BNS.Domain.Responses;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BNS.Service.Implement
{
    public class NotifyService : INotifyService
    {
        private readonly MyConfiguration _config;

        public NotifyService(IOptions<MyConfiguration> config)
        {
            _config = config.Value;
        }

        public async Task SendNotify(List<NotifyResponse> notifyResponses)
        {
            if (notifyResponses == null || notifyResponses.Count == 0)
                return;
            PostData(notifyResponses);
        }

        private async Task PostData(object requestData)
        {
            using var httpClient = new HttpClient();


            // Chuyển đổi dữ liệu thành chuỗi JSON
            var jsonContent = JsonSerializer.Serialize(requestData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Gửi yêu cầu POST
            var response = await httpClient.PostAsync(string.Format("{0}/api/notify", _config.Default.NotifyUrl), content);

            // Xử lý kết quả
            if (response.IsSuccessStatusCode)
            {
            }
            else
            {
            }
        }
    }
}
