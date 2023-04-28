namespace Sat.Recruitment.Api.Application.User
{
    public class UserPremiun : IUserType
    {
        public decimal GetMoney(string money)
        {
            var response = decimal.Parse(money);
            if (response > 100)
            {
                response += (response * 2);
            }
            return response;
        }
    }
}