using System;
using CWO.Star;
using Implementation.Views.Screen;
using Infrastructure.Base.Application.Events;
using UnityEngine.UI;

namespace CWO.Ship
{
    public class ShipInSpaceController : BaseUIObject
    {
        public int PlayerId;

        public Image ShipImage;
        public Image BackgroundImage;
        public Button SelectShip;
        public NodeSpaceController NodeSpaceController;

        protected bool isSelected = false;

        void Start()
        {
            SelectShip.onClick.AddListener(() => { this.OnShipClicked(); });
        }
        protected override void SubscribeToEvents(SubscribeEvent e)
        {
            
        }

        private void OnShipClicked()
        {
            NodeSpaceController.SetSelectedShip(this);
        }
    }
}

