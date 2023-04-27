using System;

namespace Sat.Recruitment.Api.Application.User
{
    public class UserSuper : IUserType
    {
        public decimal GetMoney(string money)
        {
            var response = decimal.Parse(money);
            if (response > 100)
            {
                response += (response * Convert.ToDecimal(0.20));
            }

            return response;
        }
    }
}