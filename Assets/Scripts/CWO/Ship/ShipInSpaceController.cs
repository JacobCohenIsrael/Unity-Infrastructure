using CWO.Star;
using Implementation.Views.Screen;
using Infrastructure.Base.Application.Events;
using UnityEngine.UI;
using Infrastructure.Core.Ship;
using Implementation;
using UnityEngine;

namespace CWO.Ship
{
    public class ShipInSpaceController : BaseUIObject, ISelectable
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
            SelectShip.onClick.AddListener(OnSelect);
        }
        protected override void SubscribeToEvents(SubscribeEvent e)
        {
            
        }

        public void OnSelect()
        {
            NodeSpaceController.SetSelectedShip(this);
        }

        public void ShowSelectedIndicator()
        {
            BackgroundImage.enabled = true;
        }

        public void HideSelectedIndicator()
        {
            BackgroundImage.enabled = false;
        }

        public string GetOnSelectedDescription()
        {
            return "Player: " + PlayerId +
            "\nShip Race: " + Ship.GetShipType() +
            "\nShip Class: " + Ship.GetShipClass();
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}

