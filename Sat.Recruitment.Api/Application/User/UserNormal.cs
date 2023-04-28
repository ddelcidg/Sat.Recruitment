using System;

namespace Sat.Recruitment.Api.Application.User
{
    public class UserNormal: IUserType
    {
        public decimal GetMoney(string money)
        {
            var response = decimal.Parse(money);
            if (response > 10 && response < 100)
                response += (response * Convert.ToDecimal(0.8));
            else
                response += (response * Convert.ToDecimal(0.12));

            return response;
        }
    }
}