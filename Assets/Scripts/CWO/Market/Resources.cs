using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CWO.Market 
{
    public class Resources : MonoBehaviour 
    {
        public Image[] itemImages = new Image[numResourceSlots];
        public Resource[] items = new Resource[numResourceSlots];


        public const int numResourceSlots = 4;


        public void AddResource(Resource itemToAdd)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                {
                    items[i] = itemToAdd;
                    itemImages[i].sprite = itemToAdd.sprite;
                    itemImages[i].enabled = true;
                    return;
                }
            }
        }


        public void RemoveResource (Resource itemToRemove)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == itemToRemove)
                {
                    items[i] = null;
                    itemImages[i].sprite = null;
                    itemImages[i].enabled = false;
                    return;
                }
            }
        }
    }
}