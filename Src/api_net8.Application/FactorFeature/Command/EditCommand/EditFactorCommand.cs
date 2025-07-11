using api_.net9.Common.Dto;
using api_.net9.Common.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_net9.Application.FactorFeature.Command.EditCommand
{
    public record EditFactorCommand(int FactorNo, DateOnly FactorDate, string? Customer, DelivaryType? DelivaryType) : IRequest<ServiceResponseDto<int>>;

}
