using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TileLevel
{
	public List<Tile> tiles { get; set; }
	public int laneCount { get; set; }
	public string songTrack { get; set; }
	public TileLevel()
	{
		tiles = new List<Tile>();
	}
}
public class Tile
{
	public int tileLane { get; set; }
	public float tileSpawn { get; set; }
	public float tileHit { get; set; }
	public float tileMiss { get; set; }
	public Tile()
	{
		tileLane = 0;
		tileSpawn = 0;
		tileHit = 0;
		tileMiss = 0;
	}

}
public class TileManager : MonoBehaviour
{
	public static TileManager Instance { get; private set; } = null;

	// Start is called before the first frame updat
	#region GoodCodingPractices
	string level2 = "{\"tiles\":[{\"tileLane\": 0,\"tileSpawn\":0.50,\"tileHit\":0.20,\"tileMiss\":1.30}, {\"tileLane\": 0,\"tileSpawn\":1.50,\"tileHit\":0.20,\"tileMiss\":1.30},{\"tileLane\": 3,\"tileSpawn\":1.60,\"tileHit\":0.20,\"tileMiss\":1.30},{\"tileLane\": 1,\"tileSpawn\":1.70,\"tileHit\":0.20,\"tileMiss\":5}],\"laneCount\": 4, \"songTrack\": \"level1.wav\"}";
	#endregion
	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}

	}
	void Start()
	{
		StartCoroutine(GameStart());
	}
	IEnumerator GameStart()
	{
		yield return new WaitForSeconds(3);
	}


}
