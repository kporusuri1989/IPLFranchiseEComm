using API;
using Application.Commands;
using Application.Interfaces;
using Application.Mappings;
using Application.Models;
using Application.Queries;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Infrastructure;
using IPLFranchiseEcommApp;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
.AddJsonOptions(x =>
{
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
}); 

builder.Services.AddSwaggerGen(SwaggerOptions => SwaggerOptions.DocumentFilter<SwaggerDocumentFilter>());

builder.Services.AddMediatR(cfg =>
cfg.RegisterServicesFromAssemblies(typeof(SwaggerDocumentFilter).Assembly));

builder.Services.AddMediatR(cfg =>
cfg.RegisterServicesFromAssemblies(typeof(GetProductsQuery).Assembly));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(typeof(ApplicationMappingProfile));
});
//var mappingConfig = new MapperConfiguration(mc =>
//{
//    mc.AddProfile(new MappingProfile());
//});

//IMapper mapper = mappingConfig.CreateMapper();

//builder.Services.AddSingleton<>(mapper);

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddMediatR(cfg =>
cfg.RegisterServicesFromAssemblies(typeof(OrderDetail).Assembly));


//foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
//{
//    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
//}
builder.Services.AddSingleton<Mediator>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>, GetProductsQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetProductDetailsQuery, ProductDetailsDto>, GetProductDetailsQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetProductsOnSearchQuery, List<ProductDetailsDto>>, GetProductsOnSearchQueryHandler>();
builder.Services.AddTransient<IRequestHandler<CreateCartCommand, ServiceResponse<CustomerCartDto>>, CreateCartCommandHandler>();
builder.Services.AddTransient<IRequestHandler<CreateOrderCommand, ServiceResponse<OrderDto>>, CreateOrderCommandHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
