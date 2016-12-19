using UnityEngine;
using Infrastructure.Core.Login.Events;
using Implementation.Views.Screen;
using UnityEngine.UI;
using Infrastructure.Core.Login;

public class LobbyController : BaseUIObject {

    public Button logoutButton; 

	void Awake ()
    {
        application.eventManager.AddListener<LoginSuccessfulEvent>(this.onLoginSuccessful);
        application.eventManager.AddListener<LogoutSuccessfulEvent>(this.OnLogoutSuccessful);
        logoutButton.onClick.AddListener(() => { this.OnLogout(); });
        Hide();
    }

    void onLoginSuccessful(LoginSuccessfulEvent e)
    {
        Debug.Log("Login Successful, Showing Lobby Screen");
        Show();
    }

    void OnLogoutSuccessful(LogoutSuccessfulEvent e)
    {
        Debug.Log("Logout Successful, Hiding Lobby Screen");
        Hide();
    }

    public void OnLogout()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        LoginService loginService = application.serviceManager.get<LoginService>() as LoginService;
        loginService.Logout();
    }
}
