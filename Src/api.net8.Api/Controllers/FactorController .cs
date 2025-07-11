using Azure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using api_net9.Application.FactorFeature.Command.EditCommand;
using api_net9.Application.FactorFeature.Command.AddCommand;

namespace api_net9.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FactorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FactorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Create(AddFactorCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mediator.Send(command);
            if (result.succsess)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, EditFactorCommand command)
        {
            if (id < 1)
            {
                return BadRequest();
            }
            var result = await _mediator.Send(new EditFactorWithIdCommand
                (id, command.FactorNo, command.FactorDate, command.Customer, command.DelivaryType, 0)
            );
            if (result.succsess)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }

    }
}
