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
			stars = starService.GetStarsList ();
			Hide ();
		}

        protected override void SubscribeToEvents(SubscribeEvent e)
        {
            eventManager.AddListener<LoginSuccessfulEvent>(this.OnLoginSuccessful);
        }

		void OnLoginSuccessful(LoginSuccessfulEvent e)
		{
//			Debug.Log("Login Successful, Populating WorldMap");
			foreach (StarModel star in stars) 
			{
                GameObject instantiatedStar = Instantiate(starPrefab, starSpawn);
				Vector3 starPosition = new Vector3(star.coordX, star.coordY, 0);
                StarController starData = instantiatedStar.GetComponent<StarController>();
				starData.star = star;
				instantiatedStar.transform.position = starPosition;
			}
		}
	}
}