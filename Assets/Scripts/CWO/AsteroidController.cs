using CWO.Star;
using UnityEngine;
using UnityEngine.UI;
using Implementation;

namespace CWO.Asteroid
{
    public class AsteroidController : Implementation.InstantiatedMonoBehaviour, ISelectable, IAttackable
    {

        [SerializeField]
        public int CurrentHull;

        [SerializeField]
        public int MaxHull;

        [SerializeField]
        private Image _asteroidImage;

        [SerializeField]
        private Image _backgroundImage;

        [SerializeField]
        private Button _selectAsteroidButton;

        [SerializeField]
        private NodeSpaceController _nodeSpaceController;

        [SerializeField]
        private Slider _hullBar;

        protected bool isSelected = false;

        private bool _isDestroyed = false;

        void Start()
        {
            _selectAsteroidButton.onClick.AddListener(OnSelect);
            UpdateHullBar();
        }

        public void OnSelect()
        {
            _nodeSpaceController.SetSelectedAsteroid(this);
        }

        public void TakeDamage(int damageTaken)
        {
            CurrentHull = CurrentHull - damageTaken;
            UpdateHullBar();
            if (CurrentHull < 1)
            {
                gameObject.SetActive(false);
                Instantiate(_nodeSpaceController.ExplosionPrefab, transform.position, Quaternion.identity, transform.parent);
                HideSelectedIndicator();
                _isDestroyed = true;
            }
        }

        public void UpdateHullBar()
        {
            _hullBar.maxValue = MaxHull;
            _hullBar.value = CurrentHull;
        }

        public void ShowSelectedIndicator()
        {
            _backgroundImage.enabled = true;
            _hullBar.gameObject.SetActive(true);
        }

        public void HideSelectedIndicator()
        {
            _backgroundImage.enabled = false;
            _hullBar.gameObject.SetActive(false);
        }

        public string GetOnSelectedDescription()
        {
            return "Asteroid" +"\nHull: " + CurrentHull + "/" + MaxHull;
        }

        public bool isDestroyed()
        {
            return _isDestroyed;
        }

        public Vector3 GetPosition()
        {
            return transform.parent.transform.localPosition + transform.localPosition;
        }
    }
}