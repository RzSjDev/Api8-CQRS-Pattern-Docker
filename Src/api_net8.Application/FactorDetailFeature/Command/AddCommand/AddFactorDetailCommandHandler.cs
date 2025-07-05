using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Src.api_.net8.Common.Dto;
using Src.api_.net8.Common.Enum;
using Src.api_net8.Application.Context;
using Src.api_net8.Application.FactorFeature.Command.EditCommand;
using Src.api_net8.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Src.api_net8.Application.FactorDetailFeature.Command.AddCommand
{
    public class AddFactorDetailCommandHandler : IRequestHandler<AddFactorDetailCommand, ServiceResponseDto<int>>
    {
        private readonly IDataContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public AddFactorDetailCommandHandler(IDataContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<ServiceResponseDto<int>> Handle(AddFactorDetailCommand request, CancellationToken cancellationToken)
        {
            var serviceResponse = new ServiceResponseDto<int>();
            var factorDetail = _mapper.Map<FactorDetail>(request);
            factorDetail.SumPrice = (long)(factorDetail.UnitPrice * factorDetail.Count);
            _context.FactorDetails.Add(factorDetail);
            await _context.SaveChangesAsync();
            var SumOfFactors = _context.FactorDetails.Where(f => f.FactorId == factorDetail.FactorId)?.Select(f => f.SumPrice).Sum();
            var Factor = await _context.Factors.Where(x => x.FactorId == factorDetail.FactorId).FirstOrDefaultAsync();
            if (Factor == null)
                throw new Exception(" فاکتوری پیدا نشد");
            Factor.TotalPrice = SumOfFactors;
            await _mediator.Send(new EditFactorWithIdCommand(
                Factor.FactorId, Factor.FactorNo, Factor.FactorDate, Factor.Customer, (DelivaryType?)Factor.DelivaryType, Factor.TotalPrice));
            serviceResponse.Message = $"با موفقیت ایجاد شد {factorDetail.UnitPrice} ریز فاکتور با مبلغ واحد";
            serviceResponse.Data = factorDetail.UnitPrice;
            return serviceResponse;
        }

    }
}
