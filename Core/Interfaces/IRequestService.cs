using Core.Dtos;

namespace Core.Interfaces
{
    public interface IRequestService
    {
        List<RequestDto> GetRequests(string userId);
        void Create(string userId);
    }
}