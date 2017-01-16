using UnityEngine;
using System.Collections;

namespace Implementation.Views.Screen
{
    public abstract class BaseUIObject : MonoBehaviour
    {
        public Infrastructure.Base.Application.Application application = Infrastructure.Base.Application.Application.getInstance();

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
