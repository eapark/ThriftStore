using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class CanvasActions : MonoBehaviour {
	public GameObject panelMenu;
	public GameObject panelSetting;
	public GameObject panelInventory;
	public GameObject imageBackground;

	private GameObject player;
	private bool menuOn = false;
	private bool inventoryOn = false;

	// Use this for initialization
	void Start () {
		panelSetting.SetActive (false);
		panelMenu.SetActive (false);
		imageBackground.SetActive (false);
		panelInventory.SetActive (false);

		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		Cursor.lockState = (menuOn || inventoryOn)?CursorLockMode.None:CursorLockMode.Locked;
		Cursor.visible = (menuOn || inventoryOn);
		if (Input.GetKeyDown (KeyCode.Escape)) {
			toggleMenuOn ();
		}
		// Don't open inventory if menu is active
		else if (Input.GetKeyDown (KeyCode.E) && !menuOn) {
			toggleInventoryOn ();
		}

		Time.timeScale = (menuOn || inventoryOn)?0.0f:1.0f; // Pause/resume time flow in game
	}

	private void toggleMenuOn(){
		menuOn = !menuOn;
		panelMenu.SetActive (menuOn);
		imageBackground.SetActive (menuOn);
		player.GetComponent<FirstPersonController> ().enabled = !menuOn;
	}

	private void toggleInventoryOn(){
		inventoryOn = !inventoryOn;
		panelInventory.SetActive (inventoryOn);
		//imageBackground.SetActive (inventoryOn);
		player.GetComponent<FirstPersonController> ().enabled = !inventoryOn;
	}

	public void ResumeGame() {
		toggleMenuOn ();
	}

	public void RestartGame() {
		// Fade out and reload current scene
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}

	public void Settings() {
		panelMenu.SetActive (false);
		panelSetting.SetActive (true);
		imageBackground.SetActive (true);
	}

	public void MainMenu() {
		//SceneManager.LoadScene ();
	}
}
