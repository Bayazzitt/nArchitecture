using Core.Application.Dtos;

namespace Application.Features.FindeksCreditRates.Dtos;

public class CreatedFindeksCreditRateDto : IDto
{
    public int Id { get; set; }
    public int Score { get; set; }
}