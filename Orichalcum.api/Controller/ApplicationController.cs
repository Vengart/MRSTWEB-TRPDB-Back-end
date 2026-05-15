using Orichalcum.BusinessLogic.Interface;
using Orichalcum.Domains.Entities.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Orichalcum.Api.Controller
{
    [Route("api/applications")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        public IApplicationActions _applicationActions;

        public ApplicationController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _applicationActions = bl.GetApplicationActions();
        }

        // GameMaster видит все заявки на свою сессию
        [Authorize(Roles = "GameMaster,Admin")]
        [HttpGet("session/{sessionId}")]
        public IActionResult GetApplicationsBySession(int sessionId)
        {
            var apps = _applicationActions.GetApplicationsBySessionAction(sessionId);
            return Ok(apps);
        }

        // Игрок подаёт заявку
        [Authorize(Roles = "Player,GameMaster,Admin")]
        [HttpPost]
        public IActionResult CreateApplication([FromBody] ApplicationData application)
        {
            var created = _applicationActions.CreateApplicationAction(application);
            if (created == null) return BadRequest(new { Message = "Application already exists or session is full" });
            return Ok(created);
        }

        // Игрок отменяет заявку
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteApplication(int id)
        {
            var deleted = _applicationActions.DeleteApplicationAction(id);
            if (!deleted) return NotFound();
            return Ok(new { Message = "Application cancelled" });
        }

        // GameMaster одобряет или отклоняет заявку
        [Authorize(Roles = "GameMaster,Admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateApplication(int id, [FromBody] ApplicationData application)
        {
            var updated = _applicationActions.UpdateApplicationAction(id, application);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
        [Authorize(Roles = "GameMaster,Admin,Moderator")]
        [HttpPut("{id}/status")]
        public IActionResult UpdateApplicationStatus(int id, [FromBody] int status)
        {
            var application = new Orichalcum.Domains.Entities.Application.ApplicationData
            {
                Status = (Orichalcum.Domains.Enums.ApplicationStatus)status
            };
            var updated = _applicationActions.UpdateApplicationAction(id, application);
            if (updated == null) return NotFound();
            return Ok(updated);
        }


    }
}