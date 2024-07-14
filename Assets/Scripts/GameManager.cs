using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; } = null;
	[SerializeField]
	private int score = 0;
	
	private Dictionary<int, int> levels;
	[SerializeField]
	private GameObject pauseMenu;
	[SerializeField]
	private GameObject pauseButton;
	
	public bool isGamePaused { get; set; } = false;
	public int audioVolume { get; set; } = 0;
	private void Awake () {
		if (Instance == null) {
			Instance = this;
			levels= new Dictionary<int, int>();
		} else {
			Destroy(gameObject);
		}
	}
	public void AddScore (int points) {
		score += points;
		Debug.Log("Score: " + score);
	} 
	public void flipPause () {
		isGamePaused = !isGamePaused;
		//set time scale to 0 if game is paused
		Time.timeScale = isGamePaused ? 0 : 1;
		flipVisibilityPauseMenu(isGamePaused);

	}
	private void flipVisibilityPauseMenu(bool isPaused){
		pauseButton.SetActive(!isPaused);
		pauseMenu.SetActive(isPaused);
		

	}
	public int isLevelAvailable(int level){
		if(level < levels.Count && level >= 0){
			return levels[level];
		}
		return -1;

	}
	public void addLevelAvailable(int a){
		if(!levels.ContainsKey(a)){
			levels.Add(a, 0);
		}
		

	}
	public void setLevel(int level, int value){
		if(level < levels.Count && level >= 0){
			levels[level] = value;
		}
		
	}

}
