using UnityEngine;
using Infrastructure.Base.Application.Events;
using Infrastructure.Core.Login.Events;
using UnityEngine.UI;
using Implementation.Views.Screen;
using Infrastructure.Core.Login;

namespace CWO.Login
{
    public class LoginController : BaseUIObject
    {
        public Button loginSubmitButton;

        protected LoginService loginService;

        void Awake()
        {
            Debug.Log("Login Controller is Hiding");
            application.eventManager.AddListener<LoginSuccessfulEvent>(this.OnLoginSuccessful);
            loginSubmitButton.onClick.AddListener(() => { this.onSubmit(); });
            loginService = application.serviceManager.get<LoginService>() as LoginService;
            Hide();
        }

        void onSubmit()
        {
            loginService.LoginAsGuest();
        }

        void OnLoginSuccessful(LoginSuccessfulEvent e)
        {
            PlayerPrefs.SetString("sessionId", e.player.sessionId);
            PlayerPrefs.SetInt("playerId", e.player.id);
            PlayerPrefs.Save();
        }
    }
}

