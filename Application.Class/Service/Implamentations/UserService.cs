using Application.Class.Service.Interfaces;
using Data.Class.DTOs.Account;
using Data.Class.entities.Account;
using Data.Class.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace Application.Class.Service.Implamentations
{
    public class UserService : IUserService
    {
        #region ctor
        private readonly IgenricRepository<User> _userRepository;
        private readonly ISmsService _SmsService;
        private readonly ILogger<UserService> _logger;
        public UserService(IgenricRepository<User> userReposoitory, ISmsService smsService, ILogger<UserService> logger)
        {
            _userRepository = userReposoitory;
            _SmsService = smsService;
            _logger = logger;
            
        }
        public async ValueTask DisposeAsync()
        {
            await _userRepository.DisposeAsync();
        }
        #endregion
        #region Register Methods
        public async Task<bool> CheckUserExistMobile(string Mobile)
        {
            return await _userRepository.GetQuery().AnyAsync(u => u.MobileNumber == Mobile);
        }



        public async Task EditUserDetails(EditUserInfoDTOs dto)
        {
            var user = await _userRepository.GetEntityById(dto.userId);
            user.Adress = dto.Adress;
            user.Email = dto.Email;
            user.FullName = dto.FullName;
            user.postCode = dto.postCode;

            _userRepository.EditEntity(user);
            await _userRepository.SaveAsync();
        }

        public async Task<EditUserInfoDTOs> GetEditUserDetails(long UserId)
        {
            var user = await _userRepository.GetEntityById(UserId);
            return new EditUserInfoDTOs
            {
                userId = UserId,
                Adress = user.Adress,
                Email = user.Email,
                FullName = user.FullName,
                postCode = user.postCode


                

            };

           

        }

        public async Task<UserDetailDTOs> GetUserDetails(long UserId)
        {
            var user = await _userRepository.GetEntityById(UserId);
            return new UserDetailDTOs
            {
                RoleId = UserId,
                Adress = user.Adress,
                Email = user.Email,
                FullName = user.FullName,
                postCode = user.postCode,
                MobileNumber = user.MobileNumber,
                CreatedTime = user.CreatedTime,
                UpdatedTime = user.UpdatedTime,
            };
        }

        public async Task<bool> RegisterOrLoginUser(registerUserDto dto)
        {
            var result = await RegisterOrLoginUserWithCode(dto);
            return result.SmsSent;
        }

        public async Task<(bool SmsSent, string? DevelopmentCode)> RegisterOrLoginUserWithCode(registerUserDto dto)
        {
            var checkUser = await CheckUserExistMobile(dto.PhoneNumber);
            if(checkUser)
            {
                var user = await _userRepository.GetQuery().FirstAsync(u => u.MobileNumber == dto.PhoneNumber);
                user.MobileActivationNumber = GenerateActivationCode();
                _userRepository.EditEntity(user);
                await _userRepository.SaveAsync();
                var smsSent = await TrySendActivationSms(dto.PhoneNumber, user.MobileActivationNumber);
                return (smsSent, smsSent ? null : user.MobileActivationNumber);
            }
            var newUser = new User
            {
                MobileNumber = dto.PhoneNumber,
                MobileActivationNumber = GenerateActivationCode()
            };
            await _userRepository.Addentity(newUser);
            await _userRepository.SaveAsync();
            var newUserSmsSent = await TrySendActivationSms(dto.PhoneNumber, newUser.MobileActivationNumber);
            return (newUserSmsSent, newUserSmsSent ? null : newUser.MobileActivationNumber);
        }

        public async Task<bool> SendActivationSms(string Mobile)
        {
            var result = await SendActivationSmsWithCode(Mobile);
            return result.SmsSent;
        }

        public async Task<(bool SmsSent, string? DevelopmentCode)> SendActivationSmsWithCode(string Mobile)
        {
            var user = await _userRepository.GetQuery().FirstOrDefaultAsync(u => u.MobileNumber == Mobile);
            if(user == null)
            {
                return (false, null);
            }
            user.MobileActivationNumber = GenerateActivationCode();
            _userRepository.EditEntity(user);
            await _userRepository.SaveAsync();
            var smsSent = await TrySendActivationSms(Mobile, user.MobileActivationNumber);
            return (smsSent, smsSent ? null : user.MobileActivationNumber);
        }

        public async Task<bool> CheckMobileAuthorization(MobileActivationDTOs dto)
        {
            var activationCode = string.Concat(
                dto.activationCodePart1,
                dto.activationCodePart2,
                dto.activationCodePart3,
                dto.activationCodePart4,
                dto.activationCodePart5,
                dto.activationCodePart6);
            if (activationCode.Length != 6 || activationCode.Any(c => c < '0' || c > '9'))
            {
                return false;
            }

            var user = await GetUSerByMobile(dto.Mobile);
            if (user == null || !string.Equals(user.MobileActivationNumber, activationCode, StringComparison.Ordinal))
            {
                return false;
            }

            user.MobileActivationNumber = string.Empty;
            _userRepository.EditEntity(user);
            await _userRepository.SaveAsync();
            return true;
        }

        public async Task<User?> GetUSerByMobile(string mobile)
        {
            return await _userRepository.GetQuery()
                .Include(user => user.Role)
                .FirstOrDefaultAsync(user => user.MobileNumber == mobile);
        }

        private static string GenerateActivationCode()
        {
            return RandomNumberGenerator.GetInt32(0, 1_000_000).ToString("D6");
        }

        private async Task<bool> TrySendActivationSms(string mobile, string code)
        {
            try
            {
                await _SmsService.SendSmsServioce(mobile, code);
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Activation SMS delivery failed.");
                return false;
            }
        }
        #endregion

    }
}
