using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_manager : MonoBehaviour {
    /*******************************************************************************************************/
    /********************************************* Properties **********************************************/
    /*******************************************************************************************************/
    public const int MAX_HEALTH = 100;
    private SpriteRenderer spr;
    private TeamManager tm;

	//Health logic
	private int m_Health = MAX_HEALTH;

	public delegate void OnHealthChangeDelegate();
	public event OnHealthChangeDelegate OnHealthChange;

	public int Health {
		get { return m_Health;}
		set {
			if (m_Health == value)
				return;
			m_Health = value;
			if (OnHealthChange != null)
				OnHealthChange ();
		}
	}

	//Team logic
	private char m_Team = 'n';

	public delegate void OnTeamChangeDelegate();
	public event OnTeamChangeDelegate OnTeamChange;

	public char Team {
		get { return m_Team;}
		set {
			if (m_Team == value)
				return;
			m_Team = value;
			if (OnTeamChange != null)
				OnTeamChange ();
		}
	}

	/*******************************************************************************************************/
	/********************************************** Functions **********************************************/
	/*******************************************************************************************************/

	void Dynamic_Color() {
        Color32 Starting_Color = tm.Starting_Colors[m_Team];
        int health_delta = MAX_HEALTH - m_Health;
        int r = Starting_Color.r - health_delta < 0 ? 0 : Starting_Color.r - health_delta;
        int g = Starting_Color.g - health_delta < 0 ? 0 : Starting_Color.g - health_delta;
        int b = Starting_Color.b - health_delta < 0 ? 0 : Starting_Color.b - health_delta;
        Color32 New_Color = new Color32((byte)r, (byte)g, (byte)b, 255);
        spr.color = New_Color;
    }

	// Use this for initialization
	void Start () {
        tm = GameObject.FindGameObjectWithTag("Team Manager").GetComponent<TeamManager>();
        spr = transform.Find("background").GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
