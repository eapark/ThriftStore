using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson; // For disabling/abling FPS controller script
using UnityEngine.EventSystems;

public class ObserveItem : MonoBehaviour {
	private bool observing = false;
	private GameObject observingItem;
	private GameObject duplicated;
	public GameObject Panel_Background;
	private GameObject player;
	private Inventory inventory;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		inventory = FindObjectOfType<Inventory> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!observing) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit) && !EventSystem.current.IsPointerOverGameObject()) {
				if (hit.transform.tag == "Item") {
					// Left click
					if (Input.GetMouseButtonDown (0)) {
						// Disable FPS Controller script
						player.GetComponent<FirstPersonController> ().enabled = false;

						// Activate Panel_Background
						Panel_Background.SetActive (true);

						// Put duplicated object in layer ItemObserving
						duplicated = Instantiate (hit.transform.gameObject.GetComponent<Item> ().itemObj, new Vector3(0f, 0f, -1.5f), Quaternion.identity);
						duplicated.SetActive (true);
						duplicated.layer = 9;

						foreach (Transform child in duplicated.transform) {
							child.gameObject.layer = 9;
						}

						observing = true;
						observingItem = hit.transform.gameObject;
					} else if (Input.GetKeyDown (KeyCode.F)) {
						Item item = hit.collider.gameObject.GetComponent<Item> ();
						inventory.AddItem (item);
					}
				}
			}
		}
		else {
			if (Input.GetMouseButtonDown (0)) {
				// Get out of observing mode
				observing = false;
				player.GetComponent<FirstPersonController>().enabled = true;
				Panel_Background.SetActive (false);
				Destroy (duplicated);
			}
			else if (Input.GetKeyDown (KeyCode.F)) {
				Item item = observingItem.GetComponent<Item> ();
				inventory.AddItem (item);
				// Get out of observing mode
				observing = false;
				player.GetComponent<FirstPersonController>().enabled = true;
				Panel_Background.SetActive (false);
				Destroy (duplicated);
			}
			// If observing is true, rotate item with mouse
			duplicated.transform.Rotate(Input.GetAxis("Mouse Y") * Time.deltaTime * 30, Input.GetAxis("Mouse X") * -Time.deltaTime * 30, 0) ;
		}
	}
}
