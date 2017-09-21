using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Implementation
{
    public interface ISelectable
    {
        void ShowSelectedIndicator();
        void HideSelectedIndicator();
        string GetOnSelectedDescription();
        Vector3 GetPosition();
        void OnSelect();
    }
}

