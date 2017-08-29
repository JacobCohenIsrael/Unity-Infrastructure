using System;
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
        public const string guestLoginToken = "editorToken";
        #else
        public const string guestLoginToken = "token";
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
            if (PlayerPrefs.HasKey(guestLoginToken))
            {
                string token = PlayerPrefs.GetString(guestLoginToken);
                loginService.LoginAsGuest(token);
            }
            else
            {
                var guid = Guid.NewGuid().ToString();
                loginService.LoginAsGuest(guid);
                PlayerPrefs.SetString(guestLoginToken, guid);
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

