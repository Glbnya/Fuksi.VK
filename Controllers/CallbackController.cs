using Fuksi.VK.Constants;
using Fuksi.VK.Models;
using Fuksi.VK.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Reflection;
using VkNet.Abstractions;
using VkNet.Enums.StringEnums;
using VkNet.Model;
using Fuksi.Common.Queue.DI;

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

                case CallbackType.JoinedGroup:
                case CallbackType.LeftGroup:
                    await _vkApiService.UserFuksiMembership(update);
                    return Ok("ok");

                case CallbackType.AllowedMessages:
                    //await _vkApiService.GetLeaveOrJoinUserCallback();
                    return Ok("ok");

                case CallbackType.DeniedMessages:
                   // await _vkApiService.GetLeaveOrJoinUserCallback();
                    return Ok("ok");

                default: return Ok("ok");
            }

        }
    }
}
