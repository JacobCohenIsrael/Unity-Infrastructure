using UnityEditor;
using UnityEngine;

public class Utility : MonoBehaviour {

	[MenuItem("Utility/Reset Player Prefs")]
	private static void _resetPlayerPrefs()
	{
		PlayerPrefs.DeleteAll();
	}
}
