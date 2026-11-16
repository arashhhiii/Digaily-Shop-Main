using Data.Class.DTOs.Account;
using Data.Class.entities.Account;

namespace Application.Class.Service.Interfaces
{
    public interface IUserService : IAsyncDisposable
    {
        #region Register & Login
        Task<bool> RegisterOrLoginUser(registerUserDto dto);
        Task<(bool SmsSent, string? DevelopmentCode)> RegisterOrLoginUserWithCode(registerUserDto dto);
        Task<bool> CheckUserExistMobile(string Mobile);
        Task<bool> CheckMobileAuthorization(MobileActivationDTOs dto);
        Task<User?> GetUSerByMobile(string mobile);
        Task<EditUserInfoDTOs> GetEditUserDetails(long UserId);
        Task EditUserDetails(EditUserInfoDTOs dto);
        Task<UserDetailDTOs> GetUserDetails(long UserId);
        Task<bool> SendActivationSms(string Mobile);
        Task<(bool SmsSent, string? DevelopmentCode)> SendActivationSmsWithCode(string Mobile);
        
        #endregion
    }
}
