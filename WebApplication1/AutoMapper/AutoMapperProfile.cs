using AutoMapper;
using System.Linq;
using WebApplication1.Data.DataBaseModels;
using WebApplication1.DTO;
using WebApplication1.DTO_s.Order;
using WebApplication1.DTO_s.Products;
using WebApplication1.DTO_s.User;
using WebApplication1.Models;

namespace Day2.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Products, ProductReadDto>();
            CreateMap<ProductGroupingOutput, ProductGroupingOutputReadDto>();
            CreateMap<ProductWriteDto, Products>();
            CreateMap<ProductUpdateDto, Products>();


            CreateMap<Vendor, VendorReadDto>();
            CreateMap<VendorWriteDto, Vendor>();
            CreateMap<VendorUpdateDto, Vendor>();

            CreateMap<Categories, CategoryReadDto>();
            CreateMap<CategoryWriteDto, Categories>();
            CreateMap<CategoryUpdateDto, Categories>();

            CreateMap<ParentCategory, ParentCategoryReadDto>();
            CreateMap<ParentCategoryWriteDto, ParentCategory>();
            CreateMap<ParentCategoryUpdateDto, ParentCategory>();

            CreateMap<Order, OrderReadDto>();
            CreateMap<OrderHistory, OrderHistoryReadDto>();
            CreateMap<OrderWriteDto, Order>();
            CreateMap<OrderUpdateDto, Order>();

            CreateMap<Cart, CartReadDto>();
            CreateMap<IQueryable<Cart>, CartReadDto>();
            CreateMap<CartWriteDto, Cart>();
            CreateMap<CartUpdateDto, Cart>();

            CreateMap<ProductInCart, ProductInCartReadDto>();
            CreateMap<ProductInCart, ProductInCartReadDto>().ForMember(p => p.Product, o => o.MapFrom(p => p.Product));



            CreateMap<ProductInCartWriteDto, ProductInCart>();
            CreateMap<ProductInCartUpdateDto, ProductInCart>();

            CreateMap<ChildCategoryReadDto, Categories>();
            CreateMap<ChildVendorReadDto, Vendor>();
            CreateMap<ChildCartReadDto, Cart>();
            CreateMap<ChildParentCategoryReadDto, ParentCategory>();
            CreateMap<ChildProductInCartReadDto, ProductInCart>();
            CreateMap<ChildProductReadDto, Products>();
            CreateMap<ChildApplicationUserReadDto, ApplicationUser>();


            CreateMap<Categories, ChildCategoryReadDto>();
            CreateMap<Vendor, ChildVendorReadDto>();
            CreateMap<Cart, ChildCartReadDto>();
            CreateMap<ParentCategory, ChildParentCategoryReadDto>();
            CreateMap<ProductInCart, ChildProductInCartReadDto>();
            CreateMap<Products, ChildProductReadDto>();
            CreateMap<ApplicationUser, ChildApplicationUserReadDto>();

        }
    }
}
