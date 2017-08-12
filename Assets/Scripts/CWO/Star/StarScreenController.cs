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
            eventManager.AddListener<PlayerOrbitStarEvent>(this.OnPlayerEnterStar);
            eventManager.AddListener<ShipEnteredNodeEvent>(this.OnShipEnteredNode);
            eventManager.AddListener<PlayerDepartFromStarEvent>(this.OnShipDeparted);
            eventManager.AddListener<PlayerLandOnStarEvent>(this.OnShipLanded);
            jumpButton.onClick.AddListener(() => { this.OnJump(); });
            landButton.onClick.AddListener(() => { this.OnLand(); });
        }

        private void cleanShipGrid()
        {
            foreach (RectTransform child in shipsGrid)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        private void OnShipLanded(PlayerLandOnStarEvent obj)
        {
            cleanShipGrid();
        }

        private void OnShipDeparted(PlayerDepartFromStarEvent e)
        {
            foreach (KeyValuePair<int, ShipModel> entry in e.NodeSpace.ships)
            {
                GameObject instantiatedShip;
                instantiatedShip = Instantiate(shipPrefab, shipsGrid);
                ShipInSpaceController shipInSpaceController = instantiatedShip.GetComponent<ShipInSpaceController>();
                shipInSpaceController.playerId = entry.Key;
            }
        }

        private void OnShipEnteredNode(ShipEnteredNodeEvent e)
        {
            GameObject instantiatedShip;
            instantiatedShip = Instantiate(shipPrefab, shipsGrid);
            ShipInSpaceController shipInSpaceController = instantiatedShip.GetComponent<ShipInSpaceController>();
            shipInSpaceController.playerId = e.PlayerId;
        }

        void OnPlayerEnterStar(PlayerOrbitStarEvent e)
        {

        }

        void OnJump()
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

        void OnLand()
        {
            PlayerModel player = playerController.player;
            playerService.LandPlayerOnStar(player);
        }


    } 
}