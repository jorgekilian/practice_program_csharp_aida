namespace LegacySecurityManager;

public class UserData {
    private readonly string _username;
    private readonly string _fullName;
    private readonly string _password;
    private readonly string _confirmPassword;

    public UserData(string username, string fullName, string password, string confirmPassword) {
        _username = username;
        _fullName = fullName;
        _password = password;
        _confirmPassword = confirmPassword;
    }

    public string Password() {
        return _password;
    }

    public string ConfirmPassword() {
        return _confirmPassword;
    }

    public string UserName() {
        return _username;
    }

    public string FullName() {
        return _fullName;
    }
}