#if !UNITY_WINRT || UNITY_EDITOR || UNITY_WP8
using System;

namespace Newtonsoft.Json.ObservableSupport
{
	public class AddingNewEventArgs
	{
		public Object NewObject { get; set; }
		public AddingNewEventArgs()
		{

		}

		public AddingNewEventArgs(Object newObject)
		{
			NewObject = newObject;
		}


	}
}

#endif
