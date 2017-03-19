﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public RoomManager roomScript;
	public LoadXmlData roomData;
	private GUIStyle guiStyle = new GUIStyle();
	public int numberOfRooms, numberOfBossRooms, numberOfSpecialRooms;

	private int level = 1;

	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
		} else if(instance != this){
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);

		roomScript = GetComponent<RoomManager> ();
		roomData = GetComponent<LoadXmlData> ();

		//Loads in room data from XML
		roomData.loadRooms ();

		//Sets constant of number of rooms in the game
		numberOfRooms = roomData.getNumberOfRooms ();
		numberOfBossRooms = roomData.getNumberOfBossRooms ();
		numberOfSpecialRooms = roomData.getNumberOfSpecialRooms ();

		//Sets up a level
		InitGame ();
		guiStyle.fontSize = 20;
		guiStyle.normal.textColor = Color.white;
	}

	void InitGame(){
		roomScript.SetupLevel (level);
	}

	public void nextLevel(){
		level++;
		InitGame ();
	}
	
	// Update is called once per frame
	void Update () {
		roomScript.checkRoomComplete ();
		if (Input.GetKeyDown (KeyCode.R)) {
			roomScript.SetupLevel (level);
		}
	}

	void OnGUI(){
		GUI.Label (new Rect(10,0,100,25), "Health: "+GameObject.FindGameObjectWithTag("Player").GetComponent<movingObject>().currentHitpoints+"/"+GameObject.FindGameObjectWithTag("Player").GetComponent<movingObject>().maxHitpoints,guiStyle);
		GUI.Label (new Rect(10,25,100,25), "Speed: "+GameObject.FindGameObjectWithTag("Player").GetComponent<movingObject>().speed,guiStyle);
		GUI.Label (new Rect(10,50,100,25), "Range: "+GameObject.FindGameObjectWithTag("Player").GetComponent<movingObject>().range,guiStyle);
		GUI.Label (new Rect(10,75,100,25), "Fire Rate: "+GameObject.FindGameObjectWithTag("Player").GetComponent<movingObject>().fireDelay,guiStyle);
		GUI.Label (new Rect(10,100,100,25), "Shot Speed: "+GameObject.FindGameObjectWithTag("Player").GetComponent<movingObject>().shotSpeed,guiStyle);
		GUI.Label (new Rect(10,125,100,25), "Damage: "+GameObject.FindGameObjectWithTag("Player").GetComponent<movingObject>().dmg,guiStyle);
		GUI.Label (new Rect(10,150,100,25), "Floor #: "+level,guiStyle);
	}
}
