using IHSA_Backend.Models;

namespace IHSA_Backend.BLL
{
    public interface IAdminRequestHandler : 
        IBaseRequestHandler<AdminRequestModel, AdminResponseModel>
    {
    }
}
