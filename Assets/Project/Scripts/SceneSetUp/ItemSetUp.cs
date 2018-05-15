using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemSetUp : MonoBehaviour {
	private Shader shader;
	public IDictionary<itemTag, float> discountRate = new Dictionary<itemTag, float> ();
	private itemTag[] tags = new itemTag[] {itemTag.blue, itemTag.green, itemTag.yellow, itemTag.white, itemTag.none};

	// Use this for initialization
	void Start () {
		shader = Shader.Find ("Standard");
		SetDiscountRate ();
	}

	void OnEnable() {
		ShelfArrangeItem.onItemsPlaced += AddTexture;
		ShelfArrangeItem.onItemsPlaced += SetPriceAndTag;
	}

	void OnDisable() {
		ShelfArrangeItem.onItemsPlaced -= AddTexture;
		ShelfArrangeItem.onItemsPlaced -= SetPriceAndTag;
	}

	// Update is called once per frame
	void Update () {
		
	}

	void AddTexture() {
		GameObject[] items = GameObject.FindGameObjectsWithTag ("Item");

		// 'Design' is for the different design on the item, 'Colors' is for the possible color of the shirts
		Object[] itemDesigns = Resources.LoadAll ("Images/ItemDesigns", typeof(Texture2D));
		Object[] itemColors = Resources.LoadAll ("Images/ItemColors", typeof(Texture2D));

		ShuffleArray (itemDesigns);
		ShuffleArray (itemColors);

		// Keep a counter for itemTextures to avoid duplicates
		int counter = 0;

		foreach (GameObject item in items) {
			MeshRenderer designPart = null;
			if (item.GetComponent<Item> ().iType == itemType.shirt) {
				// for shirts, pply material to the two submeshes
				designPart = item.transform.GetChild (0).GetComponent<MeshRenderer> ();

				// Apply color material to the 'Whole' submesh
				MeshRenderer wholePart = item.transform.GetChild (1).GetComponent<MeshRenderer> ();
				Material colorMaterial = new Material (shader);
				Texture2D colorTexture = (Texture2D)itemColors [Random.Range (0, itemColors.Length)];
				colorMaterial.mainTexture = colorTexture;

				wholePart.material = colorMaterial;
			} else {
				// Non-shirt items only need the texture
				designPart = item.gameObject.GetComponent<MeshRenderer> ();
			}

			// Apply new material
			Material designMaterial = new Material (shader);
			Texture2D designTexture = (Texture2D)itemDesigns [ Random.Range(0, itemDesigns.Length) ];

			if (item.GetComponent<Item> ().iType == itemType.shirt) {
				// Set textureMaterial to be "cutout" mode and apply it (code from Unity forum)
				designMaterial.SetFloat ("_Mode", 1.0f);
				designMaterial.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
				designMaterial.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
				designMaterial.SetInt ("_ZWrite", 0);
				designMaterial.DisableKeyword ("_ALPHATEST_ON");
				designMaterial.EnableKeyword ("_ALPHABLEND_ON");
				designMaterial.DisableKeyword ("_ALPHAPREMULTIPLY_ON");
				designMaterial.renderQueue = 3000;
			}

			designMaterial.mainTexture = designTexture;
			designPart.material = designMaterial;

			//counter++;
		}
	}

	void SetDiscountRate() {
		// Select one random tag to have a discount
		foreach (itemTag t in tags) {
			discountRate.Add (t, 1.0f);
		}
		// Set a random tag to have a discount rate
		// Don't include itemTag.none in the range
		discountRate [tags [Random.Range (0, tags.Length-1)]] = Random.Range (1, 10) * 0.1f;
	}

	void SetPriceAndTag() {
		foreach (GameObject item in GameObject.FindGameObjectsWithTag ("Item")) {
			int price = Random.Range (1, 20);
			item.GetComponent<Item> ().price = price;
			item.GetComponent<Item> ().iTag = tags[ Random.Range(0, tags.Length) ];
		}
	}

	void ShuffleArray(Object[] textures) {
		int length = textures.Length;
		for (int i = 0; i < 1000; i++) {
			int index1 = Random.Range (0, length);
			int index2 = Random.Range (0, length);
			Object temp = textures [index1];
			textures [index1] = textures [index2];
			textures [index2] = temp;
		}
	}
}
