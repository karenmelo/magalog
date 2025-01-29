using Magalog.Application.Dtos;
using Magalog.Domain.Entitites;
using Microsoft.AspNetCore.Http;

namespace Magalog.Application.Services.Interfaces;

public interface ILegacyProcessingService
{
    Task ProcessLegacyFile(string? lines);
    Task<IEnumerable<UserDto>> GetOrders(int? order_id, DateOnly? startDate, DateOnly? endDate);
}
