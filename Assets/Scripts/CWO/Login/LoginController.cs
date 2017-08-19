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
        public const string loginSessionId = "editorSessionId";
        #else
        public const string loginSessionId = "sessionId";
        #endif
        public Button loginSubmitButton;

        protected LoginService loginService;

        protected override void SubscribeToEvents(SubscribeEvent e)
        {
            application.eventManager.AddListener<LoginSuccessfulEvent>(this.OnLoginSuccessful);
            loginSubmitButton.onClick.AddListener(() => { this.OnSubmit(); });
            loginService = application.serviceManager.get<LoginService>() as LoginService;
            Hide();
        }

        public void OnSubmit()
        {
            loginService.LoginAsGuest();
        }

        void OnLoginSuccessful(LoginSuccessfulEvent e)
        {
            PlayerPrefs.SetString(loginSessionId, e.Player.token);
            PlayerPrefs.SetInt("playerId", e.Player.id);
            PlayerPrefs.Save();
        }
    }
}

