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

        private ShipInSpaceController _selectedShipRef;

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
            eventManager.AddListener<ShipEnteredNodeEvent>(OnShipEnteredNode);
            eventManager.AddListener<ShipLeftNodeEvent>(OnShipLeftNode);
            eventManager.AddListener<PlayerDepartFromStarEvent>(OnShipDeparted);
            eventManager.AddListener<PlayerLandOnStarEvent>(OnShipLanded);
            eventManager.AddListener<PlayerJumpedToNodeEvent>(OnPlayerJumpToStar);
            eventManager.AddListener<LoginSuccessfulEvent>(OnLoginSuccessful);

            jumpButton.onClick.AddListener(OnJump);
            landButton.onClick.AddListener(OnLand);
        }

        private void OnLoginSuccessful(LoginSuccessfulEvent e)
        {
            PrepareScreen();
            foreach (var entry in e.Node.Ships)
            {
                if (entry.Key == e.Player.id) continue;
                InstantiateShip(entry.Value, entry.Key);
            }
        }

        private void OnPlayerJumpToStar(PlayerJumpedToNodeEvent e)
        {
            CleanShipGrid();
            PrepareScreen();
            foreach (var entry in e.NodeSpace.Ships)
            {
                InstantiateShip(entry.Value, entry.Key);
            }
        }

        private void OnShipLeftNode(ShipLeftNodeEvent e)
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

        private void CleanShipGrid()
        {
            foreach (RectTransform child in shipsGrid)
            {
                Destroy(child.gameObject);
            }
        }

        private void OnShipLanded(PlayerLandOnStarEvent e)
        {
            CleanShipGrid();
        }

        private void OnShipDeparted(PlayerDepartFromStarEvent e)
        {
            PrepareScreen();
            CleanShipGrid();
            foreach (var entry in e.NodeSpace.Ships)
            {
                if (entry.Key == e.Player.id) continue;
                InstantiateShip(entry.Value, entry.Key);
            }
        }

        private void OnShipEnteredNode(ShipEnteredNodeEvent e)
        {
            InstantiateShip(e.ShipModel, e.PlayerId);
        }

        private void InstantiateShip(ShipModel ship, int playerId)
        {
            var instantiatedShip = Instantiate(shipPrefab, shipsGrid);
            var shipInSpaceController = instantiatedShip.GetComponent<ShipInSpaceController>();
            var texture = Resources.Load("Sprites/Ships/" + ship.GetShipType() + "/" + ship.GetShipClass()) as Texture2D;
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            shipInSpaceController.PlayerId = playerId;
            shipInSpaceController.ShipImage.sprite = sprite;
            shipInSpaceController.NodeSpaceController = this;
        }

        private void OnJump()
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

        private void OnLand()
        {
            var player = playerController.player;
            playerService.LandPlayerOnStar(player);
        }

        public void SetSelectedShip(ShipInSpaceController shipRef)
        {
            shipRef.BackgroundImage.enabled = true;
            if (_selectedShipRef != null)
            {
                _selectedShipRef.BackgroundImage.enabled = false;
            }
            if (_selectedShipRef == shipRef)
            {
                _selectedShipRef = null;
                return;
            }
            _selectedShipRef = shipRef;
        }


    } 
}