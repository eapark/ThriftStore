using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Inventory : MonoBehaviour {
	public Inventory inventory;
	public GameObject panelInventory;

	// Use this for initialization
	void Start () {
		// Subscribe to ItemAdded, ItemRemoved event
		inventory.ItemAdded += Inventory_ItemAdded;
		inventory.ItemRemoved += Inventory_ItemRemoved;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void Inventory_ItemAdded(object sender, InventoryEventArgs e) {
		foreach (Transform itemSlot in panelInventory.transform) {
			// If no item is present, ie. only Background child is present
			if (itemSlot.childCount < 2) {
				GameObject newItem = Instantiate (e.Item.itemObj, itemSlot);
				newItem.name = "Item";
				InventoryItemMouseHandler handler = newItem.AddComponent<InventoryItemMouseHandler> ();
				handler.item = e.Item;
				newItem.transform.SetAsLastSibling ();
				newItem.transform.localScale = Vector3.one * 50f;
				newItem.transform.localPosition = Vector3.zero;
				newItem.transform.localEulerAngles = new Vector3 (0, 180, 0);
				newItem.SetActive (true);
				newItem.layer = 10; // itemInventory layer
				foreach (Transform child in newItem.transform) {
					child.gameObject.layer = 10;
				}
				newItem.tag = "Untagged";
				return;
			}
		}
	}

	private void Inventory_ItemRemoved (object sender, InventoryEventArgs e)
	{
		foreach (Transform itemSlot in panelInventory.transform) {
			if (itemSlot.childCount >= 2) {
				GameObject childItem = itemSlot.GetChild (1).gameObject;
				Destroy (childItem);
			}
		}
	}
}
