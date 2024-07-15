using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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
public class TileManager : MonoBehaviour{
	InputMap input;	
	public Vector2 press;
	public static TileManager Instance { get; private set; } = null;
	[SerializeField]
	private GameObject tilePrefab; 
	static bool finished=false;
	
	
	#region LevelsHardcode
	string level2 = "{\"tiles\": [{\"tileLane\": 0, \"tileSpawn\": 0.1, \"tileHit\": 3.0, \"tileMiss\": 0.2}, {\"tileLane\": 1, \"tileSpawn\": 0.2, \"tileHit\": 3.0, \"tileMiss\": 0.2}, {\"tileLane\": 0, \"tileSpawn\": 0.7, \"tileHit\": 1.0, \"tileMiss\": 0.2}, {\"tileLane\": 2, \"tileSpawn\": 1.0, \"tileHit\": 3.0, \"tileMiss\": 0.2}, {\"tileLane\": 3, \"tileSpawn\": 1.0, \"tileHit\": 3.0, \"tileMiss\": 0.2}, {\"tileLane\": 0, \"tileSpawn\": 1.0, \"tileHit\": 3.0, \"tileMiss\": 0.2}, {\"tileLane\": 1, \"tileSpawn\": 1.0, \"tileHit\": 3.0, \"tileMiss\": 0.2}, {\"tileLane\": 2, \"tileSpawn\": 1.0, \"tileHit\": 3.0, \"tileMiss\": 0.2}, {\"tileLane\": 3, \"tileSpawn\": 1.0, \"tileHit\": 3.0, \"tileMiss\": 0.2}, {\"tileLane\": 0, \"tileSpawn\": 1.0, \"tileHit\": 3.0, \"tileMiss\": 0.2}, {\"tileLane\": 1, \"tileSpawn\": 1.0, \"tileHit\": 3.0, \"tileMiss\": 0.2}, {\"tileLane\": 2, \"tileSpawn\": 1.0, \"tileHit\": 3.0, \"tileMiss\": 0.2}, {\"tileLane\": 3, \"tileSpawn\": 1.0, \"tileHit\": 3.0, \"tileMiss\": 0.2}, {\"tileLane\": 0, \"tileSpawn\": 1.0, \"tileHit\": 3.0, \"tileMiss\": 0.2}, {\"tileLane\": 1, \"tileSpawn\": 1.0, \"tileHit\": 3.0, \"tileMiss\": 0.2}, {\"tileLane\": 2, \"tileSpawn\": 1.0, \"tileHit\": 3.0, \"tileMiss\": 0.2}, {\"tileLane\": 3, \"tileSpawn\": 1.0, \"tileHit\": 3.0, \"tileMiss\": 0.2}], \"laneCount\": 4, \"songTrack\": \"clairedelune.wav\"}";
	#endregion
	private Dictionary<int, GameObject> levels;
	void Awake(){
		if (Instance == null)
		{
			Instance = this;
			levels = new Dictionary<int, GameObject>();
			input = new InputMap();
		}
		else
		{
			Destroy(gameObject);
		}

	}
	void OnEnable(){
		input.Enable();
		input.Player.Click.performed += x => CallbackTouch();

	}
	void OnDisable(){
		input.Disable();
	}
	void Start()
	{
		StartCoroutine(GameStart());

	}
	void FixedUpdate(){
		
	}
	
	TileLevel LoadLevel(string level)
	{
		TileLevel tileLevel = new TileLevel();
		tileLevel = JsonConvert.DeserializeObject<TileLevel>(level);
		return tileLevel;
	}
	IEnumerator GameStart(){
		//Find camera
		//force phone to portrait
		#if !UNITY_STANDALONE

			#if !UNITY_EDITOR
				Screen.orientation = ScreenOrientation.Portrait;
				Debug.Log("Portrait");
			#else
					Screen.orientation = ScreenOrientation.LandscapeLeft;
				Debug.Log("Landscape");
			#endif
		#endif	
		
		var k = LoadLevel(level2);
		SpawnLanes(k);
		yield return new WaitForSeconds(3);
		Camera.main.GetComponent<AudioSource>().Play();
		StartCoroutine(SpawnTiles(k));

		

		
	}
	IEnumerator SpawnTiles(TileLevel k){
		foreach (Tile tile in k.tiles){
			Debug.Log("Spawning tile");
			//instantiate the tile
			yield return new WaitForSeconds(tile.tileSpawn);
			GameObject _tile = Instantiate(tilePrefab);
			_tile.tag="Tile";
			float width = Camera.main.orthographicSize * 2 * Camera.main.aspect;
			float widthRatio = width/k.laneCount;
			_tile.transform.localScale = new Vector3(widthRatio, 1+tile.tileHit, 1);
			var f = _tile.AddComponent<TileBehaviour>();
			f.fallTime = tile.tileMiss;
			f.lane = tile.tileLane;
			
			
			_tile.transform.position = levels[tile.tileLane].transform.position;

			
		}
		StartCoroutine(GameOver());
		finished = true;
		
	}
	IEnumerator GameOver(){
		bool k = false;
		
		while(!k){
			if (GameObject.FindGameObjectsWithTag("Tile").Length == 0){
				k = true;
				Debug.Log(GameObject.FindGameObjectsWithTag("Tile").Length);
				SceneManager.LoadScene(0);
			}
			yield return new WaitForSeconds(0.1f);
		}
	
		


		
		//check if theres any tile left

		//SceneManager.LoadScene(0);
	}
	void CallbackTouch(){
		//get where the player clicked
		Debug.Log("Touch");
		Vector2 pos = Camera.main.ScreenToWorldPoint(input.Player.MousePosition.ReadValue<Vector2>());
		press= pos;
		//check if the player clicked on a tile by its collider
		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
		if (hit.collider != null){
			Debug.Log("Hit");
			Destroy(hit.collider.gameObject);
		}

	}
	void SpawnLanes(TileLevel k){
		
		var laneCount = k.laneCount;
		
		for (int i = 0; i < laneCount; i++){
			//create a square
			//GameObject lane = GameObject.CreatePrimitive(PrimitiveType.Quad);
			//aparently there is no square primitive...
			//clone prefab
			GameObject lane = new GameObject(); //Instantiate(prefab);

			

				#if !UNITY_EDITOR && !UNITY_STANDALONE
					#if UNITY_ANDROID
						float height = Camera.main.orthographicSize * 2;
						
						//put them on top of the screen
						float posX = Camera.main.orthographicSize;
						float heightRatio = posX/laneCount;
						lane.transform.position = new Vector3(-posX/2 + heightRatio*i + heightRatio/2, height, 0);
						//fill in the screen
						lane.transform.localScale = new Vector3(1, heightRatio, 1);
					#endif
				#else
					Debug.Log("Landscape2");
					float width = Camera.main.orthographicSize * 2 * Camera.main.aspect;
					//put them on top of the screen
					float posY = Camera.main.orthographicSize;
					float widthRatio = width/laneCount;

					lane.transform.position = new Vector3(-width/2 + widthRatio*i + widthRatio/2, posY, 0);
					
					//fill in the screen
					lane.transform.localScale = new Vector3(widthRatio, 1, 1);
				#endif
			
			levels.Add(i, lane);

			
			
		}
	
	}


}
