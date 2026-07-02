using AutoMapper;
using CoachApp.Entities;
using CoachApp.Payments.Dto;

namespace CoachApp.Payments
{
    public class PaymentMapProfile : Profile
    {
        public PaymentMapProfile()
        {
            CreateMap<Payment, PaymentDto>();
            CreateMap<CreatePaymentDto, Payment>();
            CreateMap<PaymentDto, Payment>();
        }
    }
}
