using System;
using System.Collections.Generic;
using System.Text;

namespace IqOptionApi.Models
{
    public class LoginResult
    {
        public string SSID { get; set; }
        public string ErrorMessage { get; set; }
        public string Phone2FA { get; set; }
        public bool IsError()
        {
            return SSID == null;
        }
    }
}
