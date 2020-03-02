using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebStore.Domain.Dto.Products;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels;
using WebStore.Domain.ViewModels.Identity;

namespace WebStore.Infrastructure.AutoMapper
{
    public class ViewModelMapping : Profile
    {
        public ViewModelMapping()
        {
            CreateMap<RegisterUserViewModel, User>()
                .ForMember(model => model.UserName,
                    opt => opt.MapFrom(ViewModel => ViewModel.UserName));
            CreateMap<ProductDto, ProductViewModel>().ReverseMap();
        }
    }
}
