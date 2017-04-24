using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController {
	private int Xpoint = 0;
	private int Ypoint = 0;
	private float _relief = 15f;

	private Texture2D[] textures;
	private TerrainData terrainData;

	public TerrainController (int x, int y, Texture2D[] texs) {
		Xpoint = x;
		Ypoint = y;
		terrainData = new TerrainData ();
		textures = texs;
	}

	public IEnumerator SetRealtimeTerrainHeight (Terrain tr) {
		terrainData.alphamapResolution = 33;
		terrainData.heightmapResolution = 33;

		float[,] heights = new float[Xpoint, Ypoint];
		float[,,] map = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, 3];
		terrainData.splatPrototypes = getSplatProttypes (textures);

		for (int x = 0; x < Xpoint; x++) {
			for (int y = 0; y < Ypoint; y++) {
				float _seedX = Random.value * 100f;
				float _seedZ = Random.value * 100f;

				float xHeight = (_seedX) / _relief;
				float yHeight = (_seedZ) / _relief;

				heights[x, y] =  Mathf.PerlinNoise(xHeight, yHeight);
				map [x, y, getTextureIndex(heights [x, y])] = 1f;
				yield return new WaitForSeconds (0.001f);
				terrainData.SetHeights (0, 0, heights);
				terrainData.SetAlphamaps (0, 0, map);
				tr.terrainData = terrainData;
			}
		}
	}

	public TerrainData getTerrainData () {
		return terrainData;
	}

	public SplatPrototype[] getSplatProttypes (Texture2D[] texs) {
		SplatPrototype[] splayPrototypes = new SplatPrototype[texs.Length];

		for (int i = 0; i < texs.Length; i++) {
			splayPrototypes [i] = new SplatPrototype ();
			splayPrototypes [i].tileSize = Vector2.one;
			splayPrototypes [i].texture = texs [i];
		}

		return splayPrototypes;
	}

	private int getTextureIndex (float height) {
		if (height < 0.3f)
			return 0;
		if (height >= 0.3f && height < 0.6f)
			return 1;
		if (height >= 0.6)
			return 2;
		return 1;
	}
}
