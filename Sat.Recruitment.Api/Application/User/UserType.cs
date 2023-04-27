namespace Sat.Recruitment.Api.Application.User
{
    public class UserType
    {
        private IUserType UserTypeRepository { get; set; }

        public UserType(string userType)
        {
            UserTypeRepository = ResolveInterface(userType);
        }
        private IUserType ResolveInterface(string userType)
        {
            return userType switch
            {
                "Normal" => new UserNormal(),
                "SuperUser" => new UserSuper(),
                "Premium" => new UserPremiun(),
                _ => UserTypeRepository
            };
        }
        
        public decimal GetMoney(string money)
        {
            return UserTypeRepository.GetMoney(money);
        }
    }
}