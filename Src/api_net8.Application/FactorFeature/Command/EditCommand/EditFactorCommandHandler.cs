using api_.net9.Common.Dto;
using api_net9.Application.Context;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_net9.Application.FactorFeature.Command.EditCommand
{
    public class EditFactorCommandHandler : IRequestHandler<EditFactorWithIdCommand, ServiceResponseDto<int>>
    {
        private readonly IDataContext _context;
        private readonly IMapper _mapper;
        public EditFactorCommandHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponseDto<int>> Handle(EditFactorWithIdCommand request, CancellationToken cancellationToken)
        {
            var serviceResponse = new ServiceResponseDto<int>();
            var factor = await _context.Factors.FindAsync(request.FactorId, cancellationToken);
            if (factor == null)
                throw new Exception("فاکتوری پیدا نشد");
            _mapper.Map(request, factor);
            _context.Factors.Entry(factor).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            serviceResponse.Message = $"با موفقیت ویرایش شد {factor.FactorNo} فاکنور با شماره";
            serviceResponse.Data = factor.FactorNo;
            return serviceResponse;
        }

    }
}
