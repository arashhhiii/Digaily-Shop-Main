using Application.Class.Service.Interfaces;
using Kavenegar;
using System;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text;

namespace Application.Class.Service.Implamentations
{
    public class SmsService : ISmsService
    {
        private string apiKey ="";

        public async Task SendSmsServioce(string Mobile, string Code)
        {
            var SenderApi = new Kavenegar.KavenegarApi(apiKey);
            await SenderApi.VerifyLookup(Mobile, Code, "DigailyVerCode");
        }
    }
}
