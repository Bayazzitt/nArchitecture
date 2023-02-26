﻿using Application.Features.Brands.Constants;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;
using static Application.Features.Brands.Constants.BrandsOperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Brands.Commands.Delete;

public class DeleteBrandCommand : IRequest<DeletedBrandResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int Id { get; set; }

    public bool BypassCache { get; }
    public string CacheKey => "brands-list";
    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, BrandsOperationClaims.Admin, Write, BrandsOperationClaims.Delete };

    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, DeletedBrandResponse>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly BrandBusinessRules _brandBusinessRules;

        public DeleteBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper,
                                         BrandBusinessRules brandBusinessRules)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _brandBusinessRules = brandBusinessRules;
        }

        public async Task<DeletedBrandResponse> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            await _brandBusinessRules.BrandIdShouldExistWhenSelected(request.Id);
            Brand mappedBrand = _mapper.Map<Brand>(request);
            Brand deletedBrand = await _brandRepository.DeleteAsync(mappedBrand);
            DeletedBrandResponse deletedBrandResponse = _mapper.Map<DeletedBrandResponse>(deletedBrand);
            return deletedBrandResponse;
        }
    }
}