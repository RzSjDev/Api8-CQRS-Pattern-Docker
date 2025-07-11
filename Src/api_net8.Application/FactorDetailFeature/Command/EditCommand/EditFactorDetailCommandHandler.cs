﻿using api_.net9.Common.Dto;
using api_.net9.Common.Enum;
using api_net9.Application.Context;
using api_net9.Application.FactorFeature.Command.EditCommand;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace api_net9.Application.FactorDetailFeature.Command.EditCommand
{
    public class EditFactorDetailCommandHandler : IRequestHandler<EditFactorDetailWithIdCommand, ServiceResponseDto<int>>
    {
        private readonly IDataContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public EditFactorDetailCommandHandler(IDataContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<ServiceResponseDto<int>> Handle(EditFactorDetailWithIdCommand request, CancellationToken cancellationToken)
        {
            var serviceResponse = new ServiceResponseDto<int>();
            var factorDetail = await _context.FactorDetails.FindAsync(request.FactorDetailId, cancellationToken);
            if (factorDetail == null)
                throw new Exception("ریز فاکتوری پیدا نشد");
            _mapper.Map(request, factorDetail);
            _context.FactorDetails.Entry(factorDetail).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            var SumOfFactors = _context.FactorDetails.Where(f => f.FactorId == factorDetail.FactorId)?.Select(f => f.SumPrice).Sum();
            var Factor = await _context.Factors.Where(x => x.FactorId == factorDetail.FactorId).FirstOrDefaultAsync();
            if (Factor == null)
                throw new Exception(" فاکتوری پیدا نشد");
            Factor.TotalPrice = SumOfFactors;
            await _mediator.Send(new EditFactorWithIdCommand(
                Factor.FactorId, Factor.FactorNo, Factor.FactorDate, Factor.Customer, (DelivaryType?)Factor.DelivaryType, Factor.TotalPrice));
            serviceResponse.Message = $"با موفقیت تغییر یافت {factorDetail.UnitPrice} ریز فاکتور با مبلغ واحد";
            serviceResponse.Data = factorDetail.UnitPrice;
            return serviceResponse;
        }

    }
}
