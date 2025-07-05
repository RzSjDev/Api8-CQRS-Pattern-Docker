using MediatR;
using Src.api_.net8.Common.Dto;
using Src.api_.net8.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.api_net8.Application.FactorFeature.Command.EditCommand
{
    public record EditFactorCommand(int FactorNo, DateOnly FactorDate, string? Customer, DelivaryType? DelivaryType) : IRequest<ServiceResponseDto<int>>;

}
