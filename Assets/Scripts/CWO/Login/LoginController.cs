using UnityEngine;
using Infrastructure.Base.Application.Events;
using Infrastructure.Core.Login.Events;
using UnityEngine.UI;
using Implementation.Views.Screen;
using Infrastructure.Core.Login;

public class LoginController : BaseUIObject
{
    public Button loginSubmitButton;

    protected LoginService loginService;

    void Awake()
    {
        Debug.Log("Login Controller is Hiding");
        application.eventManager.AddListener<ApplicationStartedEvent>(this.OnApplicationStarted);
        application.eventManager.AddListener<LoginSuccessfulEvent>(this.OnLoginSuccessful);
        application.eventManager.AddListener<LogoutSuccessfulEvent>(this.OnLogoutSuccessful);
        loginSubmitButton.onClick.AddListener(() => { this.onSubmit(); });
        loginService = application.serviceManager.get<LoginService>() as LoginService;
        Hide();
    }

    private void OnApplicationStarted(ApplicationStartedEvent e)
    {
        if (!PlayerPrefs.HasKey("sessionId"))
        {
            Debug.Log("Login Controller is Showing");
            Show();
        }
    }

    void onSubmit()
    {
        loginService.LoginAsGuest();
    }

    void OnLoginSuccessful(LoginSuccessfulEvent e)
    {
        Debug.Log("Login Successful, Hiding Login Screen");
        PlayerPrefs.SetString("sessionId", e.player.sessionId);
        PlayerPrefs.Save();
        Hide();
    }

    void OnLogoutSuccessful(LogoutSuccessfulEvent e)
    {
        Debug.Log("Logout Successful, Showing Login Screen");
        Show();
    }
}
