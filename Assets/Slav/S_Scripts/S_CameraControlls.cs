/**
 * Author: Viaceslav Povstianoj
 * Purpose: To control player camera component.
 * Language: C#
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CameraControlls : MonoBehaviour 
{
	/* private variables */
	private float _zoomRate = 0.4f;
	private float _screenEdgeRange = 35.0f;
	private float _cameraSpeed = 0.25f;

	/* Update is called once per frame */
	void Update ()
	{
		CameraZoom ();
		CameraMouseMovement ();
	}

	/**
	 * Allows player to zoom camera in and out by changing the "field of view" distance.
	 * @param _zoomRate
	 * @return fieldOfView
	 */
	void CameraZoom ()
	{
		if (Input.GetAxis ("Mouse ScrollWheel") != 0) 
		{
			if (Input.GetAxis ("Mouse ScrollWheel") > 0) 
			{
				if (Camera.main.fieldOfView >= 20)
					Camera.main.fieldOfView -= _zoomRate;
			}

			else 
			{
				if (Camera.main.fieldOfView <= 100) 
					Camera.main.fieldOfView += _zoomRate*2;
			}
		}
	}

	/**
	 * Allows player to move camera by pressing W,A,S,D or arrow keys.
	 * Player can also move camera by dragging mouse cursor to the edge of a screen.
	 * @param _screenEdgeRange
	 * @param _cameraSpeed
	 * @return transform.position
	 */
	void CameraMouseMovement ()
	{
		if (Input.mousePosition.y < Screen.height / _screenEdgeRange || Input.GetButton ("Vertical") && Input.GetAxis ("Vertical") < 0) 
		{
			transform.position += new Vector3 (0, 0, -_cameraSpeed);
		}

		if (Input.mousePosition.y > Screen.height - Screen.height / _screenEdgeRange || Input.GetButton ("Vertical") && Input.GetAxis ("Vertical") > 0) 
		{
			transform.position += new Vector3 (0, 0, _cameraSpeed);
		}

		if (Input.mousePosition.x < Screen.width / _screenEdgeRange || Input.GetButton ("Horizontal") && Input.GetAxis ("Horizontal") < 0) 
		{
			transform.position += new Vector3 (-_cameraSpeed, 0, 0);
		}

		if (Input.mousePosition.x > Screen.width - Screen.width / _screenEdgeRange || Input.GetButton ("Horizontal") && Input.GetAxis ("Horizontal") > 0) 
		{
			transform.position += new Vector3 (_cameraSpeed, 0, 0);
		}
	}
}