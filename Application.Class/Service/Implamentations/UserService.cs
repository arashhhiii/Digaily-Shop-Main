using Application.Class.Service.Interfaces;
using Data.Class.DTOs.Account;
using Data.Class.entities.Account;
using Data.Class.Repository;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace Application.Class.Service.Implamentations
{
    public class UserService : IUserService
    {
        #region ctor
        private readonly IgenricRepository<User> _userRepository;
        private readonly ISmsService _SmsService;
        public UserService(IgenricRepository<User> userReposoitory, ISmsService smsService)
        {
            _userRepository = userReposoitory;
            _SmsService = smsService;
            
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
                id = UserId,
                Adress = user.Adress,
                Email = user.Email,
                FullName = user.FullName,
                postCode = user.postCode,
                MobileNumber = user.MobileNumber,
                CreatedTime = user.CreatedTime,
                UpdatedTime = user.UpdatedTime,
            };
        }

        public async Task RegisterOrLoginUser(registerUserDto dto)
        {
            var checkUser = await CheckUserExistMobile(dto.PhoneNumber);
            if(checkUser)
            {
                var user = await _userRepository.GetQuery().FirstAsync(u => u.MobileNumber == dto.PhoneNumber);
                user.MobileActivationNumber = new Random().Next(100000, 999999).ToString();
                _userRepository.EditEntity(user);
                await _userRepository.SaveAsync();
                await _SmsService.SendSmsServioce(dto.PhoneNumber, user.MobileActivationNumber);
            }
            var newUser = new User
            {
                MobileNumber = dto.PhoneNumber,
                MobileActivationNumber = new Random().Next(100000, 999999).ToString()
            };
            await _userRepository.Addentity(newUser);
            await _userRepository.SaveAsync();
            await _SmsService.SendSmsServioce(dto.PhoneNumber, newUser.MobileActivationNumber);
        }

        public async Task<bool> SendActivationSms(string Mobile)
        {
            var user = await _userRepository.GetQuery().FirstOrDefaultAsync(u => u.MobileNumber == Mobile);
            if(user == null)
            {
                return false;
            }
            user.MobileActivationNumber = new Random().Next(100000, 999999).ToString();
            await _SmsService.SendSmsServioce(Mobile, user.MobileActivationNumber);
            return true;
        }

        public async Task<bool> CheckMobileAuthorization(MobileActivationDTOs dto)
        {
            var user = await GetUSerByMobile(dto.Mobile);
            if (user == null) return false;
            var activationCode = $"{dto.activationCodePart1}{dto.activationCodePart2}{dto.activationCodePart3}{dto.activationCodePart4}{dto.activationCodePart5}{dto.activationCodePart6}";
            return dto.activationCode == user.MobileActivationNumber;
        }

        public async Task<User?> GetUSerByMobile(string mobile)
        {
            return await _userRepository.GetQuery().FirstOrDefaultAsync(u => u.MobileNumber == mobile);
        }
        #endregion

    }
}
