using api_.net9.Common.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_net9.Application.ProductFeature.Command
{
    public record AddProductCommand(string ProductCode, string ProductName, string? Unit, DateTime ChangeDate) : IRequest<ServiceResponseDto<string>>;

}
