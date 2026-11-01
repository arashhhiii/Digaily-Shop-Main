using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Class.Service.Interfaces
{
    public interface ISmsService
    {
        Task SendSmsServioce(string Mobile, string Code);
    }
}
