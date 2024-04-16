using AutoMapper;
using Lab_7._1.Data.Entities;
using Lab_7._1.Data.Models;

namespace Lab_7._1.Data
{
    public class DataBaseMappings : Profile
    {
        public DataBaseMappings()
        {
            CreateMap<UserEntity, User>();
        }
    }
}
