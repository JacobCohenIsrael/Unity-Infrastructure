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
using CWO.Asteroid;
using Implementation;
using System.Collections;

namespace CWO.Star
{
    public class NodeSpaceController : BaseUIObject {

        public Button jumpButton;
        public Button landButton;

        [SerializeField]
        private Button _attackButton;

        public PlayerController playerController;
        public GameObject shipPrefab;
        public GameObject ExplosionPrefab;
        public RectTransform shipsGrid;
        public RectTransform asteroidsGrid;
        public RectTransform weaponOrigin;
        private bool _isAttacking;

        protected NodeService nodeService;
        protected PlayerService playerService;

        [SerializeField]
        private Text _selectedEntityDescription;

        private ISelectable _selectedEntity;

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
                _selectedEntityDescription.text = "";
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
            _attackButton.onClick.AddListener(OnAttack);
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
            shipInSpaceController.Ship = ship;
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
            _selectedEntityDescription.enabled = true;
            _selectedEntityDescription.text = shipRef.GetOnSelectedDescription();
            SelectEntity(shipRef);
        }

        public void SetSelectedAsteroid(AsteroidController asteroidRef)
        {
            _selectedEntityDescription.enabled = true;
            _selectedEntityDescription.text = asteroidRef.GetOnSelectedDescription();
            SelectEntity(asteroidRef);
        }

        private void SelectEntity(ISelectable entity)
        {
            if (entity is IAttackable)
            {
                _attackButton.gameObject.SetActive(true);
            }
            entity.ShowSelectedIndicator();
            if (_selectedEntity != null)
            {
                _selectedEntity.HideSelectedIndicator();
                if (!(entity is IAttackable))
                {
                    _attackButton.gameObject.SetActive(false);
                }
            }
            if (_selectedEntity == entity)
            {
                _selectedEntity = null;
                _selectedEntityDescription.enabled = false;
                _attackButton.gameObject.SetActive(false);
                return;
            }
            _selectedEntity = entity;
            if (_isAttacking)
            {
                ColorBlock cb = _attackButton.colors;
                cb.normalColor = Color.white;
                _isAttacking = false;
                CancelInvoke("AttackEntity");
                _attackButton.colors = cb;
            }
        }
        
        private void OnAttack()
        {
            if (_selectedEntity is IAttackable)
            {
                ColorBlock cb = _attackButton.colors;
                if (!_isAttacking)
                {
                    _isAttacking = true;
                    cb.normalColor = Color.red;
                    
                    InvokeRepeating("AttackEntity", 0, 0.3f);
                }
                else
                {
                    cb.normalColor = Color.white;
                    _isAttacking = false;
                    CancelInvoke("AttackEntity");
                }
                _attackButton.colors = cb;
            }
        }

        private IEnumerator WeaponsOff(float time)
        {
            yield return new WaitForSeconds(time);
            weaponOrigin.gameObject.GetComponent<LineRenderer>().enabled = false;
        }

        private void AttackEntity()
        {
            IAttackable attackedEntity = _selectedEntity as IAttackable;
            attackedEntity.TakeDamage(5);
            if (attackedEntity.isDestroyed())
            {
                DeselectEntity();
            }
            else
            {
                _selectedEntityDescription.text = _selectedEntity.GetOnSelectedDescription();
                var line = weaponOrigin.gameObject.GetComponent<LineRenderer>();
                Vector3 originLocalPosition = weaponOrigin.transform.localPosition;
                Vector3 targetLocalPosition = _selectedEntity.GetPosition();
                line.SetPosition(1, targetLocalPosition - originLocalPosition);
                line.enabled = true;
                StartCoroutine(WeaponsOff(0.25f));
            }
        }

        public void DeselectEntity()
        {
            _selectedEntity = null;
            _selectedEntityDescription.enabled = false;
            _isAttacking = false;
            ColorBlock cb = _attackButton.colors;
            cb.normalColor = Color.white;
            _attackButton.colors = cb;
            CancelInvoke("AttackEntity");
        }
    } 
}