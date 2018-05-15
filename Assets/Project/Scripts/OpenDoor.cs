using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {

	private Animator doorAnim;

	// Use this for initialization
	void Start () {
		doorAnim = GetComponent<Animator> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			doorAnim.SetTrigger ("OpenDoor");
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			doorAnim.SetTrigger ("CloseDoor");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
