using IHSA_Backend.Models;

namespace IHSA_Backend.BLL
{
    public interface IRiderRequestHandler : IBaseRequestHandler<RiderRequestModel, RiderResponseModel>
    {
        public Task DeleteByRiderId(int riderId);
        public Task<RiderResponseModel?> GetByRiderId(int riderId);
        public Task<RiderResponseModel?> UpdateByRiderId(int riderId, RiderRequestModel riderRequest);
    }
}
