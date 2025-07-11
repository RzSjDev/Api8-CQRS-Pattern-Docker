﻿using Azure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using api_net9.Application.FactorDetailFeature.Command.AddCommand;
using api_net9.Application.FactorDetailFeature.Command.EditCommand;

namespace api_net9.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FactorDetailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FactorDetailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Create(AddFactorDetailCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Response = await _mediator.Send(command);
            if (Response.succsess)
                return Ok(Response.Message);

            return BadRequest(Response.Message);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, EditFactorDetailCommand command)
        {
            if (id < 1)
            {
                return BadRequest();
            }
            var Response = await _mediator.Send(new EditFactorDetailWithIdCommand
                (id, command.FactorId, command.ProductId, command.ProductDescription, command.Count, command.UnitPrice, 0));
            if (Response.succsess)
                return Ok(Response.Message);

            return BadRequest(Response.Message);
        }

    }
}
