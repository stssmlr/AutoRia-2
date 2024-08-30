using Core.Dtos;

namespace Core.Interfaces
{
    public interface IRequestService
    {
        List<RequestDto> GetRequests(string userId);
        Task Create(string userId, string userEmail);
    }
}