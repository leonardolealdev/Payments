using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payments.API.Configuration;
using Payments.Domain.Commands;
using Payments.Domain.Query;

namespace Payments.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private IIdentityManager _identityManager;

        public AuthController(IMediator mediator, IIdentityManager identityManager)
        {
            _mediator = mediator;
            _identityManager = identityManager;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command)
        {
            var registered = await _mediator.Send(command);

            if (registered)
            {
                return Ok(registered);
            }

            return BadRequest(registered);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var authenticated = await _mediator.Send(command);

            if (authenticated)
            {
                return Ok(await _identityManager.GenerateJwt(command.Email));
            }

            return Ok(authenticated);
        }

        [Authorize]
        [HttpGet("usuarios")]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _mediator.Send(new GetUserQuery());

            return Ok(usuarios);
        }
    }
}
