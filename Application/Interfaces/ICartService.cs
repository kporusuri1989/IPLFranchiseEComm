using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICartService
    {
        Task<ServiceResponse<CustomerCartDto>> CreateCartAsync(CreateCartDto createCartDto);
    }
}
