using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour {
	
	//Team color constants
	public const char TEAM_RED = 'r';
	public const char TEAM_GREEN = 'g';
	public const char TEAM_BLUE = 'b';
	public const char TEAM_YELLOW = 'y';
    public const char UNASSIGNED = 'n';

	public Dictionary<char, Color32> Starting_Colors;
	// Use this for initialization
	void Awake () {
        Starting_Colors = new Dictionary<char, Color32>();
		Starting_Colors.Add(TEAM_RED, new Color32(255,0,0,255));
		Starting_Colors.Add(TEAM_GREEN, new Color32(0,255,0,255));
		Starting_Colors.Add(TEAM_BLUE, new Color32(0,0,255,255));
		Starting_Colors.Add(TEAM_YELLOW, new Color32(255,255,0,255));
        Starting_Colors.Add(UNASSIGNED, new Color32(120, 120, 120, 255));
    }
}
