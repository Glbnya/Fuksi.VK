using Fuksi.VK.Constants;
using Fuksi.VK.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using VkNet.Abstractions;
using Fuksi.VK.Models.VkUpdate;
using Fuksi.VK.Models;
using Microsoft.Extensions.Options;

namespace Fuksi.VK.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        private readonly Settings _settings;
        private readonly IConfiguration _configuration;
        private readonly IVkApiService _vkApiService;
        private readonly IVkApi _vkApi;
        public CallbackController(IConfiguration configuration, IVkApiService vkApiService, IVkApi vkApi, IOptions<Settings> settings)
        {
            _settings = settings.Value;
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
                    var code = await _vkApi.Groups.GetCallbackConfirmationCodeAsync(_settings.GroupId);
                    return Ok(code);
                default:
                    //await _vkApiService.HandleUserAction(update);
                    await _vkApiService.TryMessageUserAsync(66355935, "Привет!");
                    return Ok("ok");
            }

        }
    }
}
