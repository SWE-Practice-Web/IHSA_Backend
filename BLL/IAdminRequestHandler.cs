using IHSA_Backend.Models;

namespace IHSA_Backend.BLL
{
    public interface IAdminRequestHandler : 
        IBaseRequestHandler<AdminRequestModel, AdminResponseModel>
    {
        public AdminRequestModel VerifyAdminRequest(AdminRequestModel request);
        public AdminResponseModel MapUser(AdminModel? user);
    }
}
