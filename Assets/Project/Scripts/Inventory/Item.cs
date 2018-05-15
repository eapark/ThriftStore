using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum itemType {
	none, shirt, cup, plate
}
public enum itemTag {
	none, blue, yellow, green, white
}

public class Item : MonoBehaviour {
	public itemType iType = itemType.none;
	public GameObject itemObj = null;
	public float price = 0f;
	public itemTag iTag = itemTag.none;

	void Start() {
		itemObj = Instantiate (gameObject);
		// Get rid of unnecessary components
		if (iType == itemType.shirt) {
			Destroy (itemObj.GetComponent<FixedJoint> ());
		}
		Destroy (itemObj.GetComponent<Rigidbody> ());
		Destroy (itemObj.GetComponent<Item> ());

		itemObj.SetActive (false);
		itemObj.hideFlags = HideFlags.HideInHierarchy;
	}

	public void OnPickup() {
		if (iType == itemType.shirt) {
			Destroy (GetComponent<FixedJoint> ());
		}
		gameObject.SetActive (false);
	}

	public void OnDrop() {
		gameObject.SetActive (true);
		gameObject.transform.SetParent (null);
		Vector3 position = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 3f);
		gameObject.transform.position = Camera.main.ScreenToWorldPoint(position);
	}
}

// Wrapper of item to use as parameter when event (ie. item added) is raised
// Needed for Event Handler 'ItemAdded'
public class InventoryEventArgs : EventArgs {
	public Item Item;
	public InventoryEventArgs(Item item) {
		Item = item;
	}
}