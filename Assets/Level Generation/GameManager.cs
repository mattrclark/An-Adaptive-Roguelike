﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	BoardCreator b; //Boardcreator object
	mTree t; //Level Tree
	int levelNum; //Level number

	public GameObject floor_Prefab;
	public GameObject rooms_Parent;
	public GameObject player_Prefab;

	//Used for GUIDrawRect function
	private static Texture2D _staticRectTexture;
	private static GUIStyle _staticRectStyle;

	void Start () {
		levelNum = 1; //Level start at 1
		createLevel (); //Create the level and all the sections

	}

	void Update () {
		//Create new level
		if (Input.GetKeyDown ("r")) {
			createLevel();
		}
			
		//Change level number and create new level
		if(Input.GetKeyDown(key:KeyCode.LeftArrow)){
			if (levelNum-- <= 0) {
				levelNum++;
			} else {
				createLevel ();
			}
		}
		if(Input.GetKeyDown(key:KeyCode.RightArrow)){
			levelNum++;
			createLevel ();
		}
	}

	//Create new tree and new boardcreator
	void createLevel(){

		foreach(Transform child in rooms_Parent.transform){
			GameObject.Destroy (child.gameObject);
		}

		t = new mTree(levelNum);
		t.printNodes ();
		List<Node> allNodes = t.getNodes ();

		foreach (Node _n in allNodes) {
			GameObject floorGO = Instantiate (floor_Prefab, _n.getGridPosition(), Quaternion.identity,rooms_Parent.transform) as GameObject;
			floorGO.transform.localScale = new Vector2 (0.7f, 0.7f);

			int[] nodeChildren = _n.getChildren ();

			if (nodeChildren != null) {
				foreach (int _child in nodeChildren) {
					GameObject doorGO = Instantiate (player_Prefab, new Vector2 ((_n.getGridPosition ().x + allNodes [_child].getGridPosition ().x) / 2f, (_n.getGridPosition ().y + allNodes [_child].getGridPosition ().y) / 2f), Quaternion.identity, rooms_Parent.transform) as GameObject;
					doorGO.transform.localScale = new Vector2 (0.2f, 0.2f);
				}
			}
		}

		/*
		//b = new BoardCreator (t);
		//List<Section> sections = b.getSections();

		//Create Rooms
		foreach (Section _s in sections) {
			if (t.getNodes () [_s.nodeIndex].getChildren () == null) {
				GameObject floorGO = Instantiate (floor_Prefab, new Vector2 (_s.centerPos.x - 5, _s.centerPos.y - 5), Quaternion.identity,rooms_Parent.transform) as GameObject;
				floorGO.transform.localScale = new Vector2 (_s.size.x * 0.7f, _s.size.y * 0.7f);
			}
		}
		*/
		/*
		//Create Corridors
		foreach (Section _s in sections) {
			GameObject corridorGO;

			//Vertical Corridor
			if (_s.centerPos.x == sections [_s.parent].centerPos.x) {
				corridorGO = Instantiate (v_corridor_Prefab, new Vector3 (_s.centerPos.x-5, (_s.centerPos.y + sections [_s.parent].centerPos.y) / 2f-5,-1f), Quaternion.identity,corridors_Parent.transform) as GameObject;
				corridorGO.transform.localScale = new Vector2 (0.05f, Mathf.Abs(_s.centerPos.y-sections [_s.parent].centerPos.y));
			}
			//Horizontal Corridor
			else {
				corridorGO = Instantiate (h_corridor_Prefab, new Vector3((_s.centerPos.x+sections[_s.parent].centerPos.x)/2f-5,_s.centerPos.y-5,-1f), Quaternion.identity,corridors_Parent.transform) as GameObject;
				corridorGO.transform.localScale = new Vector2 (Mathf.Abs(_s.centerPos.x-sections [_s.parent].centerPos.x), 0.05f);
			}
		}
		*/
	}


	// Original Code by User: IQpierce from https://forum.unity3d.com/threads/draw-a-simple-rectangle-filled-with-a-color.116348/
	// Note that this function is only meant to be called from OnGUI() functions.
	public static void GUIDrawRect( Rect position, Color color, int _index )
	{
		if( _staticRectTexture == null )
		{
			_staticRectTexture = new Texture2D( 1, 1 );
		}

		if( _staticRectStyle == null )
		{
			_staticRectStyle = new GUIStyle();
		}

		_staticRectTexture.SetPixel( 0, 0, color );
		_staticRectTexture.Apply();

		_staticRectStyle.normal.background = _staticRectTexture;

		if (_index == -1) {
			GUI.Box (position, " ", _staticRectStyle);
		} else {
			GUI.Box (position, _index + " ", _staticRectStyle);
		}


	}
}
