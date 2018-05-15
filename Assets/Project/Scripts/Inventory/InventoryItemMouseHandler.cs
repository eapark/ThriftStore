using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemMouseHandler : MonoBehaviour {

	private Camera cameraInventory;
	private Canvas canvasInventory;
	private RectTransform panelInventoryRect;

	private Inventory inventory;
	public Item item;

	void Start() {
		cameraInventory = GameObject.Find ("Camera_Inventory").GetComponent<Camera> ();
		canvasInventory = GameObject.Find ("Canvas_Inventory").GetComponent<Canvas> ();
		panelInventoryRect = GameObject.Find ("Panel_Inventory").GetComponent<RectTransform> ();
		inventory = GameObject.Find ("Inventory").GetComponent<Inventory> ();
	}

	public void OnMouseDrag() {
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = canvasInventory.planeDistance;
		Vector3 newPos = cameraInventory.ScreenToWorldPoint (mousePos);
		transform.position = newPos;
	}

	public void OnMouseUp() {
		// If outside of Panel_Inventory, drop item
		if (!RectTransformUtility.RectangleContainsScreenPoint (
			    panelInventoryRect,
			    Input.mousePosition,
			    cameraInventory
		    )) {
			inventory.RemoveItem (item);
		} else {
			// Else, put it back into original position
			transform.localPosition = Vector3.zero;
		}
	}
}
