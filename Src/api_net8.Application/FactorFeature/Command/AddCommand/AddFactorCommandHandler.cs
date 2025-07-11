using api_.net9.Common.Dto;
using api_net9.Application.Context;
using api_net9.Domain.Models;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_net9.Application.FactorFeature.Command.AddCommand
{
    public class AddFactorCommandHandler : IRequestHandler<AddFactorCommand, ServiceResponseDto<int>>
    {
        private readonly IDataContext _context;
        private readonly IMapper _mapper;
        public AddFactorCommandHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponseDto<int>> Handle(AddFactorCommand request, CancellationToken cancellationToken)
        {
            var serviceResponse = new ServiceResponseDto<int>();
            var factor = _mapper.Map<Factor>(request);
            _context.Factors.Add(factor);
            await _context.SaveChangesAsync();
            serviceResponse.Message = $"با موفقیت ایجاد شد {factor.FactorNo} فاکنور با شماره";
            serviceResponse.Data = factor.FactorNo;
            return serviceResponse;
        }

    }
}
