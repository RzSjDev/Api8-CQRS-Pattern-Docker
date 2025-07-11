using api_.net9.Common.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_net9.Application.FactorDetailFeature.Command.EditCommand
{
    public record EditFactorDetailWithIdCommand(int FactorDetailId, int FactorId, int ProductId, string? ProductDescription, decimal Count, int UnitPrice, long SumPrice) : IRequest<ServiceResponseDto<int>>;

}
