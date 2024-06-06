using System;

namespace LegacySecurityManager;

public class SecurityManager
{
    private readonly Notifier _notifier;
    private UserDataRequester _userDataRequester;

    public SecurityManager(Notifier notifier, UserDataRequester userDataRequester)
    {
        _notifier = notifier;
        _userDataRequester = userDataRequester;
    }

    public void CreateValidUser()
    {
        var userData = _userDataRequester.Request();

        if (PasswordsDoNotMatch(userData.Password(), userData.ConfirmPassword()))
        {
            NotifyPasswordDoNotMatch();
            return;
        }

        if (IsPasswordToShort(userData.Password()))
        {
            NotifyPasswordIsToShort();
            return;
        }

        var encryptedPassword = EncryptPassword(userData.Password());
        NotifyUserCreation(userData.UserName(), userData.FullName(), encryptedPassword);
    }

    private void NotifyPasswordIsToShort()
    {
        Print("Password must be at least 8 characters in length");
    }

    private void NotifyPasswordDoNotMatch()
    {
        Print("The passwords don't match");
    }

    private void NotifyUserCreation(string username, string fullName, string encryptedPassword)
    {
        Print($"Saving Details for User ({username}, {fullName}, {encryptedPassword})\n");
    }

    private static string EncryptPassword(string password)
    {
        var array = password.ToCharArray();
        Array.Reverse(array);
        var encryptedPassword = new string(array);
        return encryptedPassword;
    }

    private static bool IsPasswordToShort(string password)
    {
        return password.Length < 8;
    }

    private static bool PasswordsDoNotMatch(string password, string confirmPassword)
    {
        return password != confirmPassword;
    }

    private void Print(string message)
    {
        _notifier.Notify(message);
    }

    public static void CreateUser() {
        Notifier notifier = new ConsoleNotifier();
        InputReader inputReader = new ConsoleInputReader();
        new SecurityManager(notifier, new ConsoleUserDataRequester(notifier, inputReader)).CreateValidUser();
    }
}

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

public interface UserDataRequester {
    UserData Request();
}

public class ConsoleUserDataRequester : UserDataRequester {
    private readonly Notifier notifier;
    private readonly InputReader inputReader;

    public ConsoleUserDataRequester(Notifier notifier, InputReader inputReader) {
        this.notifier = notifier;
        this.inputReader = inputReader;
    }
    public UserData Request() {

        return new UserData(RequestUserName(), RequestFullName(), RequestPassword(), RequestPasswordConfirmation());
    }

    private string RequestUserInput(string requestMessage)
    {
        notifier.Notify(requestMessage);
        return inputReader.Read();
    }

    private string RequestPasswordConfirmation()
    {
        return RequestUserInput("Re-enter your password");
    }

    private string RequestPassword()
    {
        return RequestUserInput("Enter your password");
    }

    private string RequestFullName()
    {
        return RequestUserInput("Enter your full name");
    }

    private string RequestUserName()
    {
        return RequestUserInput("Enter a username");
    }

}