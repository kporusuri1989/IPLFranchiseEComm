using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        { 
            services.AddDbContext<IplfranchiseEcommDbContext>(opt=> opt.UseSqlServer(config.GetConnectionString("IPLECommerceDB")));
            services.AddTransient<IIplfranchiseEcommDbContext, IplfranchiseEcommDbContext>();
            return services;
        }
    }
}
