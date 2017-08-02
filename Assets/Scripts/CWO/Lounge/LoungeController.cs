using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Implementation.Views.Screen;
using Infrastructure.Base.Application.Events;
using UnityEngine.UI;

namespace CWO.Market 
{
    public class LoungeController : BaseUIObject 
    {
        public Button exitButton;
        public GameManager gameManager;

    	void Start() 
        {
            Hide();
    	}

        protected override void SubscribeToEvents(SubscribeEvent e)
        {
            exitButton.onClick.AddListener(() => { this.OnExitLounge(); });
        }

        protected void OnExitLounge()
        {
            gameManager.ChangeState(GameManager.GameState.StarMenu);
        }
    }
}
