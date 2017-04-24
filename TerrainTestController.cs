using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTestController : MonoBehaviour {

	private Terrain terrain;
	[SerializeField]
	private Texture2D[] textures;

	// Use this for initialization
	void Start () {
		terrain = gameObject.GetComponent<Terrain> ();

		TerrainController tc = new TerrainController (33, 33, textures);
		StartCoroutine (tc.SetRealtimeTerrainHeight (terrain));
		terrain.terrainData = tc.getTerrainData ();
	}
}
