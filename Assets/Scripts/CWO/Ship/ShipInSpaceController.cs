using System;
using CWO.Star;
using Implementation.Views.Screen;
using Infrastructure.Base.Application.Events;
using UnityEngine.UI;
using Infrastructure.Core.Ship;

namespace CWO.Ship
{
    public class ShipInSpaceController : BaseUIObject
    {
        public int PlayerId;

        public Image ShipImage;
        public Image BackgroundImage;
        public Button SelectShip;
        public NodeSpaceController NodeSpaceController;
        public ShipModel Ship;

        protected bool isSelected = false;

        void Start()
        {
            SelectShip.onClick.AddListener(OnShipClicked);
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

