using Core.Application.Dtos;

namespace Application.Features.AdditionalServices.Dtos;

public class DeletedAdditionalServiceDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
}