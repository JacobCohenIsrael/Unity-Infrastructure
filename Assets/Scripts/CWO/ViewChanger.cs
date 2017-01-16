using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CWO
{
    public class ViewChanger : MonoBehaviour 
    {
        public GameManager gameManager;

        public void WorldMap()
        {
            gameManager.ChangeState(GameManager.GameState.WorldMap);
        }

        public void StarMenu()
        {
            gameManager.ChangeState(GameManager.GameState.StarMenu);
        }

        public void StarOrbit()
        {
            gameManager.ChangeState(GameManager.GameState.StarOrbit);
        }
    }
}

