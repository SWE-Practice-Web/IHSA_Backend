using IHSA_Backend.Models;

namespace IHSA_Backend.BLL
{
    public interface IEventRequestHandler : IBaseRequestHandler<EventRequestModel, EventResponseModel>
    {
        public new Task<EventResponseModel?> Get(int id);
        public new Task<IList<EventResponseModel>?> GetAll();
        public Task<IList<EventElementOrderResponseModel>?> AddEventOrder(
            int id, EventElementOrderRequestModel request);
        public Task<IList<EventElementOrderResponseModel>?> BatchAddEventOrder(
            int id, IList<EventElementOrderRequestModel> request);
    }
}
