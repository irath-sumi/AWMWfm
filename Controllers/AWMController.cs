using AWM.Core.Models;
using AWMWfm.Interface;
using AWMWfm.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWMWfm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AWMController : ControllerBase
    {
        IAWMService _awmService;
        public AWMController(IAWMService awmService)
        {
            _awmService = awmService;
        }
        [HttpPost]
        [Route("SaveQuery")]
        public IActionResult SaveQuery([FromBody] Concern query)
        {
            _awmService.SaveQueries(query);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpGet]
        [Route("GetQuery")]
        public List<QuerwithDeadline> GetQuery(string userId)
        {
            return _awmService.GetQuery(userId); ;
        }

        [HttpDelete]
        [Route("DeleteQuery")]
        public IActionResult DeleteQuery(string queryID)
        {
            _awmService.DeleteQuery(queryID);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPost]
        [Route("SaveType")]
        public IActionResult SaveQueryType([FromBody] ConcernType type)
        {
            _awmService.SaveQueryType(type);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPost]
        [Route("SaveCustomer")]
        public IActionResult SaveCustomer([FromBody] Customer customer)
        {
            _awmService.SaveCustomer(customer);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPost]
        [Route("PlatformSetting")]
        public IActionResult AddPlatformSetting([FromBody] PlatformSettings setting)
        {
            //testing
            setting.Id = Guid.NewGuid().ToString();
            _awmService.AddPlatfromSetting(setting);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
