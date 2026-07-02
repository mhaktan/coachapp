using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CoachApp.Payments.Dto;

namespace CoachApp.Payments
{
    public interface IPaymentAppService : IAsyncCrudAppService<
        PaymentDto,
        long,
        PagedPaymentResultRequestDto,
        CreatePaymentDto,
        PaymentDto>
    {
    }
}
