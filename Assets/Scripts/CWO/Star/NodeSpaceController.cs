using UnityEngine;
using Implementation.Views.Screen;
using UnityEngine.UI;
using Infrastructure.Core.Player.Events;
using Infrastructure.Core.Player;
using Infrastructure.Base.Application.Events;
using Infrastructure.Core.Node;
using Infrastructure.Core.Ship;
using System.Collections.Generic;
using CWO.Ship;
using Infrastructure.Core.Login.Events;

namespace CWO.Star
{
    public class NodeSpaceController : BaseUIObject {

        public Button jumpButton;
        public Button landButton;
        public PlayerController playerController;
        public GameObject shipPrefab;
        public RectTransform shipsGrid;

        protected NodeService nodeService;
        protected PlayerService playerService;

        [SerializeField]
        private Image _starImage;

        protected Dictionary<int, ShipModel> shipsInSpace;

        private void Start()
        {
            playerService = application.serviceManager.get<PlayerService>() as PlayerService;
            nodeService = application.serviceManager.get<NodeService>() as NodeService;
            Hide();
        }

        private void PrepareScreen()
        {
            if (playerController.player != null && nodeService != null)
            {
                if (nodeService.GetNodeByName(playerController.player.currentNodeName).HasStar())
                {
                    var node = nodeService.GetNodeByName(playerController.player.currentNodeName);
                    var texture = Resources.Load("Sprites/Nodes/" + node.Sprite) as Texture2D;
                    var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                    _starImage.sprite = sprite;
                    _starImage.gameObject.SetActive(true);
                }
                else
                {
                    _starImage.gameObject.SetActive(false);
                }
            }
        }

        protected override void SubscribeToEvents(SubscribeEvent e)
        {
            eventManager.AddListener<ShipEnteredNodeEvent>(onShipEnteredNode);
            eventManager.AddListener<ShipLeftNodeEvent>(onShipLeftNode);
            eventManager.AddListener<PlayerDepartFromStarEvent>(onShipDeparted);
            eventManager.AddListener<PlayerLandOnStarEvent>(onShipLanded);
            eventManager.AddListener<PlayerJumpedToNodeEvent>(onPlayerJumpToStar);
            eventManager.AddListener<LoginSuccessfulEvent>(OnLoginSuccessful);

            jumpButton.onClick.AddListener(onJump);
            landButton.onClick.AddListener(onLand);
        }

        private void OnLoginSuccessful(LoginSuccessfulEvent e)
        {
            PrepareScreen();
        }

        private void onPlayerJumpToStar(PlayerJumpedToNodeEvent e)
        {
            cleanShipGrid();
            PrepareScreen();
            foreach (var entry in e.NodeSpace.Ships)
            {
                var instantiatedShip = Instantiate(shipPrefab, shipsGrid);
                var shipInSpaceController = instantiatedShip.GetComponent<ShipInSpaceController>();
                var texture = Resources.Load("Sprites/Ships/" + entry.Value.GetShipType() + "/" + entry.Value.GetShipClass()) as Texture2D;
                var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                shipInSpaceController.PlayerId = entry.Key;
                shipInSpaceController.ShipImage.sprite = sprite;
            }
        }

        private void onShipLeftNode(ShipLeftNodeEvent e)
        {
            foreach (RectTransform child in shipsGrid)
            {
                var shipInSpaceController = child.gameObject.GetComponent<ShipInSpaceController>();
                if (shipInSpaceController.PlayerId == e.PlayerId)
                {
                    Destroy(child.gameObject);
                }


            }
        }

        private void cleanShipGrid()
        {
            foreach (RectTransform child in shipsGrid)
            {
                Destroy(child.gameObject);
            }
        }

        private void onShipLanded(PlayerLandOnStarEvent obj)
        {
            cleanShipGrid();
        }

        private void onShipDeparted(PlayerDepartFromStarEvent e)
        {
            PrepareScreen();
            cleanShipGrid();
            foreach (var entry in e.NodeSpace.Ships)
            {
                var instantiatedShip = Instantiate(shipPrefab, shipsGrid);
                var shipInSpaceController = instantiatedShip.GetComponent<ShipInSpaceController>();
                var texture = Resources.Load("Sprites/Ships/" + entry.Value.GetShipType() + "/" + entry.Value.GetShipClass()) as Texture2D;
                var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                shipInSpaceController.PlayerId = entry.Key;
                shipInSpaceController.ShipImage.sprite = sprite;
            }
        }

        private void onShipEnteredNode(ShipEnteredNodeEvent e)
        {
            var instantiatedShip = Instantiate(shipPrefab, shipsGrid);
            var shipInSpaceController = instantiatedShip.GetComponent<ShipInSpaceController>();
            var texture = Resources.Load("Sprites/Ships/" + e.ShipModel.GetShipType() + "/" + e.ShipModel.GetShipClass()) as Texture2D;
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            shipInSpaceController.PlayerId = e.PlayerId;
            shipInSpaceController.ShipImage.sprite = sprite;
            shipInSpaceController.PlayerId = e.PlayerId;
        }

        private void onJump()
        {
            var player = playerController.player;
            if (null == player.getActiveShip())
            {
                throw new UnityException("Player must have a ship to jump");
            }
            if (player.getActiveShip().GetCurrentHullAmount() > 0)
            {
                var playerJumpEvent = new PlayerOpenedWorldMap(player);
                application.eventManager.DispatchEvent(playerJumpEvent);
            }
            else
            {
                throw new UnityException("Player ship must be repaired");
            }
        }

        private void onLand()
        {
            var player = playerController.player;
            playerService.LandPlayerOnStar(player);
        }


    } 
}