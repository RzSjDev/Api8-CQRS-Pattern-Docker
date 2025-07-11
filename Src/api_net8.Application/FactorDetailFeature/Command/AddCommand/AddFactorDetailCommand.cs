using api_.net9.Common.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_net9.Application.FactorDetailFeature.Command.AddCommand
{
    public record AddFactorDetailCommand(int FactorId, int ProductId, string? ProductDescription, decimal Count, int UnitPrice) : IRequest<ServiceResponseDto<int>>;
}
