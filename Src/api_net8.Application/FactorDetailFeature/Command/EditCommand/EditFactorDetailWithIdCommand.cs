using MediatR;
using Src.api_.net8.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.api_net8.Application.FactorDetailFeature.Command.EditCommand
{
    public record EditFactorDetailWithIdCommand(int FactorDetailId, int FactorId, int ProductId, string? ProductDescription, decimal Count, int UnitPrice, long SumPrice) : IRequest<ServiceResponseDto<int>>;

}
