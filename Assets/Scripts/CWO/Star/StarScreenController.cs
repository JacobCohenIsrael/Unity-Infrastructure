using UnityEngine;
using Infrastructure.Core.Login.Events;
using Implementation.Views.Screen;
using UnityEngine.UI;
using Infrastructure.Core.Player.Events;
using Infrastructure.Core.Player;
using Infrastructure.Base.Application.Events;
using Infrastructure.Core.Node;
using Infrastructure.Core.Ship;
using System.Collections.Generic;
using CWO.Ship;
using System;

namespace CWO.Star
{
    public class StarScreenController : BaseUIObject {

        public Button jumpButton;
        public Button landButton;
        public PlayerController playerController;
        public GameObject shipPrefab;
        public RectTransform shipsGrid;

        protected NodeService nodeService;
        protected PlayerService playerService;


        protected Dictionary<int, ShipModel> shipsInSpace;

        void Start()
        {
            playerService = application.serviceManager.get<PlayerService>() as PlayerService;
            nodeService = application.serviceManager.get<NodeService>() as NodeService;
            Hide();
        }

        protected override void SubscribeToEvents(SubscribeEvent e)
        {
            eventManager.AddListener<PlayerOrbitStarEvent>(this.onPlayerEnterStar);
            eventManager.AddListener<ShipEnteredNodeEvent>(this.onShipEnteredNode);
            eventManager.AddListener<ShipLeftNodeEvent>(this.onShipLeftNode);
            eventManager.AddListener<PlayerDepartFromStarEvent>(this.onShipDeparted);
            eventManager.AddListener<PlayerLandOnStarEvent>(this.onShipLanded);
            eventManager.AddListener<PlayerJumpedToStarEvent>(this.onPlayerJumpToStar);

            jumpButton.onClick.AddListener(() => { this.onJump(); });
            landButton.onClick.AddListener(() => { this.onLand(); });
        }

        private void onPlayerJumpToStar(PlayerJumpedToStarEvent e)
        {
            cleanShipGrid();
        }

        private void onShipLeftNode(ShipLeftNodeEvent e)
        {
            foreach (RectTransform child in shipsGrid)
            {
                ShipInSpaceController shipInSpaceController = child.gameObject.GetComponent<ShipInSpaceController>();
                if (shipInSpaceController.PlayerId == e.PlayerId)
                {
                    GameObject.Destroy(child.gameObject);
                }


            }
        }

        private void cleanShipGrid()
        {
            foreach (RectTransform child in shipsGrid)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        private void onShipLanded(PlayerLandOnStarEvent obj)
        {
            cleanShipGrid();
        }

        private void onShipDeparted(PlayerDepartFromStarEvent e)
        {
            foreach (KeyValuePair<int, ShipModel> entry in e.NodeSpace.ships)
            {
                GameObject instantiatedShip;
                instantiatedShip = Instantiate(shipPrefab, shipsGrid);
                ShipInSpaceController shipInSpaceController = instantiatedShip.GetComponent<ShipInSpaceController>();
                shipInSpaceController.PlayerId = entry.Key;
            }
        }

        private void onShipEnteredNode(ShipEnteredNodeEvent e)
        {
            GameObject instantiatedShip;
            instantiatedShip = Instantiate(shipPrefab, shipsGrid);
            ShipInSpaceController shipInSpaceController = instantiatedShip.GetComponent<ShipInSpaceController>();
            shipInSpaceController.PlayerId = e.PlayerId;
        }

        private void onPlayerEnterStar(PlayerOrbitStarEvent e)
        {

        }

        private void onJump()
        {
            PlayerModel player = playerController.player;
            if (null == player.getActiveShip())
            {
                throw new UnityException("Player must have a ship to jump");
            }
            if (player.getActiveShip().currentHullAmount > 0)
            {
                PlayerJumpEvent playerJumpEvent = new PlayerJumpEvent(player);
                application.eventManager.DispatchEvent<PlayerJumpEvent>(playerJumpEvent);
            }
            else
            {
                throw new UnityException("Player ship must be repaired");
            }
        }

        private void onLand()
        {
            PlayerModel player = playerController.player;
            playerService.LandPlayerOnStar(player);
        }


    } 
}