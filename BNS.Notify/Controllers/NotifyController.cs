﻿using BNS.Domain;
using BNS.Domain.Interface;
using BNS.Domain.Responses;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BNS.Notify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifyController : ControllerBase
    {
        protected readonly INotifytHub _notifyHub;
        protected readonly INotifyGateway _notifyService;
        public NotifyController(INotifytHub notifyHub, INotifyGateway notifyService)
        {
            _notifyHub = notifyHub;
            _notifyService = notifyService;
        }

        
        [HttpGet]
        public string Get()
        {
            return "aaaaaaa";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task Post([FromBody] List<NotifyResponse> request)
        {
            foreach (var item in request)
            {
                _notifyHub.SendNotify(item.UserReceivedId.ToString(), item);
            }
        }
    }
}
