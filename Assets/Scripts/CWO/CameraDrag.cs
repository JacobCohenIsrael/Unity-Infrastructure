using UnityEngine;
using System.Collections;

namespace CWO
{
	[System.Serializable]
	public class Boundries
	{
		public float top;
		public float bottom;
		public float left;
		public float right;
		public float minSize;
		public float maxSize;
		public float sensitivity;
	}

	public class CameraDrag : MonoBehaviour 
	{
		public Boundries boundries;
		Vector3 hitPosition = Vector3.zero;
		Vector3 currentPosition = Vector3.zero;
		Vector3 cameraPosition = Vector3.zero;

		void Update()
		{
			// Zoom
			//float size = Camera.main.orthographicSize;
			//size -= Input.GetAxis("Mouse ScrollWheel") * boundries.sensitivity;
			//size = Mathf.Clamp(size, boundries.minSize, boundries.maxSize);
			//Camera.main.orthographicSize = size;

			// Camera Move
			if(Input.GetMouseButtonDown(0)){
				hitPosition = Input.mousePosition;
				cameraPosition = transform.position;

			}
			if(Input.GetMouseButton(0)){
				currentPosition = Input.mousePosition;
				LeftMouseDrag();        
			}
		}

		void LeftMouseDrag(){
			// From the Unity3D docs: "The z position is in world units from the camera."  In my case I'm using the y-axis as height
			// with my camera facing back down the y-axis.  You can ignore this when the camera is orthograhic.
			currentPosition.z = hitPosition.z = cameraPosition.y;

			// Get direction of movement.  (Note: Don't normalize, the magnitude of change is going to be Vector3.Distance(current_position-hit_position)
			// anyways.  
			Vector3 direction = Camera.main.ScreenToWorldPoint(currentPosition) - Camera.main.ScreenToWorldPoint(hitPosition);

			// Invert direction to that terrain appears to move with the mouse.
			direction = direction * -1;

			Vector3 position = cameraPosition + direction;

			if (position.x < boundries.right && position.x > boundries.left && position.y < boundries.top && position.y > boundries.bottom) 
			{
				transform.position = position;
			}
		}
	}
}
