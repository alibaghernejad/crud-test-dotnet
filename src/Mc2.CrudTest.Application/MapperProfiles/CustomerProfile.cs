using AutoMapper;
using Mc2.CrudTest.Application.Customers.Get;
using Mc2.CrudTest.Domain.CustomerAggregate;

namespace Mc2.CrudTest.Application.MapperProfiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerDto>();
    }
}