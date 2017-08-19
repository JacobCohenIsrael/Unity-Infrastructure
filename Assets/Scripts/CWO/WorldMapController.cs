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
			Hide ();
		}

        protected override void SubscribeToEvents(SubscribeEvent e)
        {
;
        }
	}
}