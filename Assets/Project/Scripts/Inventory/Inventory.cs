using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
	public GameObject canvasInventory;
	public const int numItemSlots = 8;
	public Item[] items = new Item[numItemSlots];

	public event EventHandler<InventoryEventArgs> ItemAdded;
	public event EventHandler<InventoryEventArgs> ItemRemoved;

	private bool inventoryOn = false;

	public void Update(){
		if (Input.GetKeyDown (KeyCode.E)) {
			inventoryOn = !inventoryOn;
			canvasInventory.SetActive (inventoryOn);
		}
	}

	public void AddItem(Item toAdd){
		for (int i = 0; i < items.Length; i++) {
			if (items [i] == null) {
				items [i] = toAdd;
				toAdd.OnPickup ();

				if (ItemAdded != null) {
					ItemAdded (this, new InventoryEventArgs (toAdd)); // notify subscribers
				}
				return;
			}
		}
		// TODO: sound effect
	}

	public void RemoveItem(Item toRid){
		for (int i = 0; i < items.Length; i++) {
			if (items [i] == toRid) {
				items [i] = null;

				toRid.OnDrop ();

				if (ItemRemoved != null) {
					ItemRemoved (this, new InventoryEventArgs (toRid)); // notify subscribers
				}

				return;
			}
		}
		// TODO: sound effect
	}
}
