using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class InteractCashRegister : MonoBehaviour {
	public GameObject panel_CashRegister;
	public Inventory inventory;
	public GameObject canvas_main;
	public GameObject canvas_inventory;

	private GameObject player;
	private float totalPrice;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider col) {
		if (col.gameObject.tag == "Player") {
			if (Input.GetKeyDown (KeyCode.Return)) {
				StartCoroutine (BuyItems ());
			}
		}
	}

	private IEnumerator BuyItems() {
		// Disable player and Canvas (to prevent the menu from popping up once game is over)
		player.GetComponent<FirstPersonController> ().enabled = false;
		canvas_main.SetActive (false);
		canvas_inventory.SetActive (false);

		panel_CashRegister.SetActive (true);
		int counter = 1; // Start from 1st child of panel_CashRegister, ie. skip 0th child which is Title

		foreach (Item item in inventory.items) {
			if (item == null) {
				continue;
			}
			yield return new WaitForSeconds (1.0f);

			Transform row = panel_CashRegister.transform.GetChild (counter);
			counter++;
			foreach (Transform child in row.transform) {
				child.gameObject.SetActive (true);
				child.gameObject.gameObject.SetActive (true);
			}

			// Put item as child of itemHolder
			Transform itemHolder = row.GetChild (0);
			GameObject boughtItem = Instantiate (item.itemObj);
			boughtItem.transform.SetParent (itemHolder);
			boughtItem.transform.localScale = Vector3.one * 50f;
			boughtItem.transform.localPosition = Vector3.zero;
			boughtItem.transform.localEulerAngles = new Vector3 (0, 180, 0);
			boughtItem.layer = 10; // itemInventory layer
			foreach (Transform child in boughtItem.transform) {
				child.gameObject.layer = 10;
			}

			// Update text to be price
			Text itemPrice = row.GetComponentInChildren<Text> ();
			itemPrice.text = ""+item.price;

			totalPrice += item.price;
		}
		// Activate TotalRow
		yield return new WaitForSeconds (1.0f);
		panel_CashRegister.transform.GetChild (9).gameObject.SetActive (true);
		panel_CashRegister.transform.GetChild (9).gameObject.GetComponentInChildren<Text> ().text = "" + totalPrice;

		// Activate Buttons
		yield return new WaitForSeconds (1.0f);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		panel_CashRegister.transform.GetChild(10).gameObject.SetActive (true);
	}
}
