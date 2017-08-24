using UnityEngine;
using UnityEngine.UI;

namespace Implementation.Views.Screen
{
    public abstract class BaseUIObject : BaseMonoBehaviour
    {
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual void SetBackground(string resourcePath)
        {
            var texture = Resources.Load(resourcePath) as Texture2D;
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            var image = GetComponent<Image>();
            image.sprite = sprite;
        }
    }
}
