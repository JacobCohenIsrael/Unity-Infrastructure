using UnityEngine;
using Infrastructure.Core.Login.Events;
using Implementation.Views.Screen;
using UnityEngine.UI;
using Infrastructure.Core.Login;
using Infrastructure.Base.Application.Events;

namespace CWO.Hud
{
    public class HudController : BaseUIObject {

        public Button logoutButton; 

        void Start ()
        {
            Hide();
        }

        protected override void SubscribeToEvents(SubscribeEvent e)
        {
            logoutButton.onClick.AddListener(() => { this.OnLogout(); });
        }

        public void OnLogout()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            LoginService loginService = application.serviceManager.get<LoginService>() as LoginService;
            loginService.Logout();
        }
    }
}

