namespace server.Auth
{
    public interface IJwtAuthenicationManager
    {
        string AuthenticateUser (string username, string password);
    }
}
