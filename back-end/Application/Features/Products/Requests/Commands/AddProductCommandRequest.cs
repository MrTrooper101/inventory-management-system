﻿using back_end.Application.Features.Products.Dtos;
using MediatR;

namespace back_end.Application.Features.Products.Requests.Commands
{
    public class AddProductCommandRequest : IRequest<bool>
    {
        public AddProductDto AddProduct { get; set; }
    }
}
