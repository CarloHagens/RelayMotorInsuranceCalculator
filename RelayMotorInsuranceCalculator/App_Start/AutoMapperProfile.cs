using AutoMapper;
using RelayMotorInsuranceCalculator.DAL.Entities;
using RelayMotorInsuranceCalculator.ViewModels.MotorInsurance;

namespace RelayMotorInsuranceCalculator
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Policy, PolicyVm>().ReverseMap();
            CreateMap<Driver, DriverVm>().ReverseMap();
            CreateMap<Claim, ClaimVm>().ReverseMap();
        }
    }
}