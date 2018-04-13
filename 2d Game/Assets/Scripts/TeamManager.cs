using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour {
	
	//Team color constants
	public const char TEAM_RED = 'r';
	public const char TEAM_GREEN = 'g';
	public const char TEAM_BLUE = 'b';
	public const char TEAM_YELLOW = 'y';
	public Dictionary<char, Color32> Starting_Colors = new Dictionary<char, Color32> ();
	// Use this for initialization
	void Start () {
		Starting_Colors.Add(TEAM_RED, new Color32(255,0,0,255));
		Starting_Colors.Add(TEAM_GREEN, new Color32(0,255,0,255));
		Starting_Colors.Add(TEAM_BLUE, new Color32(0,0,255,255));
		Starting_Colors.Add(TEAM_YELLOW, new Color32(255,255,0,255));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
