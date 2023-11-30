using AutoMapper;
using Meat_Station.Domain.DTOs.LocationDTO;
using Meat_Station.Domain.DTOs.ProductDTO;
using Meat_Station.Domain.DTOs.Roles;
using Meat_Station.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meat_Station.DataAccess.Mapping_Profiles
{
    internal class Mapping_Profiles :Profile
    {
        public Mapping_Profiles()
        {
            CreateMap<LocationCreateDTO, Location>().ReverseMap();
            CreateMap<RolesCreateDTO, Role>().ReverseMap(); 
            CreateMap<ProductCreateDTO, Product>().ReverseMap();
            CreateMap<Product, ProductGetDTO>().ReverseMap();
        }
    }
}
