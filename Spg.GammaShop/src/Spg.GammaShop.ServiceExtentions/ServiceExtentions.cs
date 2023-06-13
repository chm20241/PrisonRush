using Spg.GammaShop.Infrastructure;
using Spg.GammaShop.Application.Services;
using Spg.GammaShop.Application.Filter;
using Microsoft.Extensions.DependencyInjection;
using Spg.GammaShop.Domain.Interfaces.ProductServiceInterfaces;
using Spg.GammaShop.Domain.Interfaces.UserInterfaces;
using Spg.GammaShop.Domain.Interfaces.UserMailConfirmInterface;
using Spg.GammaShop.Repository2.Repositories;
using Spg.GammaShop.Domain.Interfaces.Catagory_Interfaces;
using Spg.GammaShop.Domain.Interfaces.ShoppingCart_Interfaces;
using Spg.GammaShop.Domain.Interfaces.ShoppingCartItem_Interface;
using Microsoft.AspNetCore.Mvc.Filters;
using FluentValidation;
using Spg.GammaShop.Application.Validators;
using Spg.GammaShop.Domain.DTO;
using FluentValidation.AspNetCore;
using Spg.GammaShop.Domain.Interfaces;
using Spg.GammaShop.Domain;
using Spg.GammaShop.Domain.Models;
using Spg.GammaShop.Repository2;
using Spg.GammaShop.Domain.Interfaces.Generic_Repository_Interfaces;
using Spg.GammaShop.Application.Services.CQS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Spg.GammaShop.ServiceExtentions
{
    public static class ServiceExtentions
    {
        public static void AddAllTransient(this IServiceCollection serviceCollection)
        {
            //Product Controller
            serviceCollection.AddTransient<IAddUpdateableProductService, ProductService>();
            serviceCollection.AddTransient<IReadOnlyProductService, ProductService>();
            serviceCollection.AddTransient<IDeletableProductService, ProductService>();
            serviceCollection.AddTransient<IProductRepositroy, ProductRepository>();

            //Register Controller
            serviceCollection.AddTransient<IUserRegistrationService, UserRegistServic>();
            serviceCollection.AddTransient<IUserMailRepo, UserMailRepo>();
            serviceCollection.AddTransient<IUserMailService, UserMailService>();
            serviceCollection.AddTransient<IUserRepository, UserRepository>();


            //User Controller:
            serviceCollection.AddTransient<IAddUpdateableUserService, UserService>();
            serviceCollection.AddTransient<IReadOnlyUserService, UserService>();
            serviceCollection.AddTransient<IDeletableUserService, UserService>();
            serviceCollection.AddTransient<IUserRepository, UserRepository>();


            //Catagory Controller
            serviceCollection.AddTransient<IAddUpdateableCatagoryService, CatagoryService>();
            serviceCollection.AddTransient<IReadOnlyCatagoryService, CatagoryService>();
            serviceCollection.AddTransient<IDeletableCatagoryService, CatagoryService>();
            serviceCollection.AddTransient<ICatagoryRepository, CatagoryRepository>();

            //ShoppingCart Controller
            serviceCollection.AddTransient<IAddUpdateableShoppingCartService, ShoppingCartService>();
            serviceCollection.AddTransient<IReadOnlyShoppingCartService, ShoppingCartService>();
            serviceCollection.AddTransient<IDeletableShoppingCartService, ShoppingCartService>();
            serviceCollection.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();

            //ShoppingCartItem Controller
            serviceCollection.AddTransient<IAddUpdateableShoppingCartItemService, ShoppingCartItemService>();
            serviceCollection.AddTransient<IReadOnlyShoppingCartItemService, ShoppingCartItemService>();
            serviceCollection.AddTransient<IDeleteAbleShoppingCartItemService, ShoppingCartItemService>();
            serviceCollection.AddTransient<IShoppingCartItemRepository, ShoppingCartItemRepository>();

            //Filter User
            serviceCollection.AddTransient<IActionFilter, HasRoleFilterAttribute>();
            //Fluent Validation
            serviceCollection.AddFluentValidationAutoValidation();
            serviceCollection.AddTransient<IValidator<ProductDTO>, NewProductDtoValidator>();



            //Mediator & CommandHandler
            serviceCollection.AddTransient<IMediator, Mediator>();
            serviceCollection.AddTransient(serviceProvider =>
            {
                // Zugriff auf den IServiceProvider
                var scopedServiceProvider = serviceProvider.GetRequiredService<IServiceProvider>();

                // Erstellung des Mediators mit dem IServiceProvider
                return new Mediator(scopedServiceProvider);
            });
        }
    }
}