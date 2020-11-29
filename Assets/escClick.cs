using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escClick : MonoBehaviour
{
	public bool clicked = false;
	private void OnMouseDown()
	{
		clicked = true;
	}
}
