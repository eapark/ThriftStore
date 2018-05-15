using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfArrangeItem : MonoBehaviour {
	// local positions for 3 items on a platform
	private Vector3[] itemPos0 = new Vector3[] { new Vector3(0.6f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(-0.6f, 0.0f, 0.0f) };

	// local positions for 2 items on a platform
	private Vector3[] itemPos1 = new Vector3[] { new Vector3(0.4f, 0.0f, 0.0f), new Vector3(-0.4f, 0.0f, 0.0f) };

	// all items that go on a shelf
	private Object[] shelfItems;

	// event to notify that all items in the scene are instantiated
	public delegate void ItemsPlaced();
	public static event ItemsPlaced onItemsPlaced;

	void Start() {
		shelfItems = Resources.LoadAll ("Prefabs/ShelfItems");
		PlaceItems ();
	}

	// Place 3, 2, 3 items on each platform
	void PlaceItems () {
		GameObject[] shelves = GameObject.FindGameObjectsWithTag ("Shelf");
		int itemLen = shelfItems.Length;

		GameObject temp_inst;

		foreach (GameObject shelf in shelves) {
			Transform p0 = shelf.transform.GetChild (0);
			Transform p1 = shelf.transform.GetChild (1);
			Transform p2 = shelf.transform.GetChild (2);

			// Place 3 items on p0 and p2
			foreach (Vector3 v in itemPos0) {
				temp_inst = (GameObject)Instantiate (shelfItems [Random.Range (0, itemLen)], p0);
				temp_inst.transform.localPosition = v;

				temp_inst = (GameObject)Instantiate (shelfItems [Random.Range (0, itemLen)], p2);
				temp_inst.transform.localPosition = v;
			}

			// Place 2 items on p1
			foreach (Vector3 v in itemPos1) {
				temp_inst = (GameObject)Instantiate (shelfItems [Random.Range (0, itemLen)], p1);
				temp_inst.transform.localPosition = v;
			}
		}

		if (onItemsPlaced != null) {
			onItemsPlaced ();
		}
	}
}
