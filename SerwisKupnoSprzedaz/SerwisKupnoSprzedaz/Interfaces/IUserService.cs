namespace SerwisKupnoSprzedaz.Interfaces
{
    public interface IUserService
    {
        public bool isValidRegister(string username, string password, string password2, string adress);

        public bool isValidLogin(string login, string pass);
                     

    }
}
