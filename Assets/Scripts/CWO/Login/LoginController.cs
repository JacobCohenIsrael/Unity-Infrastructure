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
        #if UNITY_EDITOR 
        public const string loginToken = "editorToken";
        #else
        public const string loginToken = "token";
        #endif
        public Button loginSubmitButton;

        protected LoginService loginService;

        protected override void SubscribeToEvents(SubscribeEvent e)
        {
            application.eventManager.AddListener<LoginSuccessfulEvent>(OnLoginSuccessful);
            loginSubmitButton.onClick.AddListener(OnSubmit);
            loginService = application.serviceManager.get<LoginService>() as LoginService;
            Hide();
        }

        public void OnSubmit()
        {
            if (PlayerPrefs.HasKey(LoginController.loginToken))
            {
                string token = PlayerPrefs.GetString(LoginController.loginToken);
                loginService.LoginAsGuest(token);
            }
            else
            {
                string guid = loginService.LoginAsGuest();
                PlayerPrefs.SetString(loginToken, guid);
                PlayerPrefs.Save();
            }

        }

        void OnLoginSuccessful(LoginSuccessfulEvent e)
        {
            PlayerPrefs.SetInt("playerId", e.Player.id);
            PlayerPrefs.Save();
        }
    }
}

