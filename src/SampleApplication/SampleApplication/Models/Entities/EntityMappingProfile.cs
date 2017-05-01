using AutoMapper;

namespace SampleApplication.Models.Entities
{
    public class EntityMappingProfile : Profile
    {
        public EntityMappingProfile()
        {
            CreateMap<Customer, CustomerEntity>();
            CreateMap<CustomerEntity, Customer>();
        }
    }
}