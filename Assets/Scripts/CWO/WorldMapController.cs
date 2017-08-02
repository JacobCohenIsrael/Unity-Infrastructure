using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Infrastructure.Core.Star;
using Implementation.Views.Screen;
using Infrastructure.Core.Login.Events;
using System;
using Infrastructure.Base.Application.Events;
using CWO.Star;


namespace CWO
{
	public class WorldMapController : BaseUIObject 
	{
		public GameObject starPrefab;
        public Transform starSpawn;
		private Infrastructure.Core.Star.StarModel[] stars;

		void Start() 
		{
			StarService starService = Infrastructure.Base.Application.Application.getInstance ().serviceManager.get<StarService> () as StarService;
			Hide ();
		}

        protected override void SubscribeToEvents(SubscribeEvent e)
        {
            eventManager.AddListener<LoginSuccessfulEvent>(this.OnLoginSuccessful);
        }

		void OnLoginSuccessful(LoginSuccessfulEvent e)
		{          
			Debug.Log("Login Successful, Populating WorldMap");
			foreach (KeyValuePair<string, StarModel> starEntry in e.starsList) 
			{
                GameObject instantiatedStar = Instantiate(starPrefab, starSpawn);
				Vector3 starPosition = new Vector3(starEntry.Value.coordX, starEntry.Value.coordY, 0);
                StarController starController = instantiatedStar.GetComponent<StarController>();
                starController.star = starEntry.Value;
				instantiatedStar.transform.position = starPosition;
			}
		}
	}
}