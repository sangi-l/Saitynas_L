using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas_L1.Data.Dtos.Auth
{
    public class SuccessfulLoginResponseDto
    {
        public string AccessToken { get; set; }
        public SuccessfulLoginResponseDto(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}
