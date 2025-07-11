﻿using Azure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using api_net9.Application.ProductFeature.Command;
using api_net9.Application.ProductFeature.Queries.GetAllProduct;
using api_net9.Application.ProductFeature.Queries.FindProduct;

namespace api_net9.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var products = await _mediator.Send(new GetAllProductQuery());
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var products = await _mediator.Send(new FindProductQuery(id));
            if (products is null)
            {
                return NotFound(products);
            }
            return Ok(products);
        }
        [HttpPost]
        public async Task<ActionResult> Create(AddProductCommand command)
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

    }
}
