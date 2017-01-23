using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Infrastructure.Core.Star;
using Implementation.Views.Screen;
using Infrastructure.Core.Login.Events;
using System;
namespace CWO
{
	public class WorldMapController : BaseUIObject 
	{
		public GameObject starPrefab;
        public Transform starSpawn;
		private Infrastructure.Core.Star.StarModel[] stars;

		void Awake() 
		{
			StarService starService = Infrastructure.Base.Application.Application.getInstance ().serviceManager.get<StarService> () as StarService;
			application.eventManager.AddListener<LoginSuccessfulEvent>(this.OnLoginSuccessful);
			stars = starService.GetStarsList ();
			Hide ();
		}

		void OnLoginSuccessful(LoginSuccessfulEvent e)
		{
//			Debug.Log("Login Successful, Populating WorldMap");
			foreach (StarModel star in stars) 
			{
                GameObject instantiatedStar = Instantiate(starPrefab, starSpawn);
				Vector3 starPosition = new Vector3(star.coordX, star.coordY, 0);
				var starData = instantiatedStar.GetComponent ("StarController") as CWO.Star.StarController;
				starData.star = star;
				instantiatedStar.transform.position = starPosition;
			}
		}
	}
}