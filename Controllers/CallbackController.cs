using Fuksi.VK.Constants;
using Fuksi.VK.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using VkNet.Abstractions;
using Fuksi.VK.Models.VkUpdate;

namespace Fuksi.VK.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IVkApiService _vkApiService;
        private readonly IVkApi _vkApi;
        public CallbackController(IConfiguration configuration, IVkApiService vkApiService, IVkApi vkApi)
        {
            _configuration = configuration;
            _vkApiService = vkApiService;
            _vkApi = vkApi;
        }

        [HttpPost]
        public async Task<IActionResult> Callback([FromBody] Update update)
        {
            switch (update.Type)
            {
                case CallbackType.Confirmation:
                    var code = await _vkApi.Groups.GetCallbackConfirmationCodeAsync(221861505);
                    return Ok(code);
                default:
                    await _vkApiService.HandleUserAction(update);
                    return Ok("ok");
            }

        }
    }
}
