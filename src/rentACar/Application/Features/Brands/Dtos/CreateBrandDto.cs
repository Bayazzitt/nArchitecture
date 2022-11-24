﻿using Core.Application.Dtos;

namespace Application.Features.Brands.Dtos;

public class CreatedBrandDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}