using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_manager : MonoBehaviour {

    public bool debug_stationary = false;
    public int debug_health = 100;
    public char debug_team = 'r';
    public float movement_speed = 5;
    public GameObject projectile;
    public float projectileSpeed = 50;
    public List<Weapon> Weapons = new List<Weapon>();
    public int selectedWeapon = 0;

    /*******************************************************************************************************/
    /********************************************* Properties **********************************************/
    /*******************************************************************************************************/
    public const int MAX_HEALTH = 100;
    private SpriteRenderer spr;
    private TeamManager tm;
    private Rigidbody2D rb;
    private Game_Manager gm;
    private bool initializing;

	//Health logic
	private int m_Health = 100;

	public delegate void OnHealthChangeDelegate();
	public event OnHealthChangeDelegate OnHealthChange;

    public int Health {
		get { return m_Health;}
		set {
			if (m_Health == value && !initializing)
				return;

			m_Health = value;

            if( m_Health < 0)
                m_Health = 0;

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
			if (m_Team == value && !initializing)
				return;
			m_Team = value;
			if (OnTeamChange != null)
				OnTeamChange ();
		}
	}

	/*******************************************************************************************************/
	/********************************************** Functions **********************************************/
	/*******************************************************************************************************/

	private void Dynamic_Color() {
        Color32 Starting_Color = tm.Starting_Colors[m_Team];
        int health_delta = MAX_HEALTH - m_Health;
        int r = Starting_Color.r - health_delta < 0 ? 0 : Starting_Color.r - health_delta;
        int g = Starting_Color.g - health_delta < 0 ? 0 : Starting_Color.g - health_delta;
        int b = Starting_Color.b - health_delta < 0 ? 0 : Starting_Color.b - health_delta;
        Color32 New_Color = new Color32((byte)r, (byte)g, (byte)b, 255);
        spr.color = New_Color;
    }

    private void Team_Change()
    {
        Dynamic_Color();
    }

    private bool Take_Damage(int amount)
    {
        int result = Health - amount;
        if (result <= 0) return Death();
        else
        {
            Health = result;
            return false;
        }
    }

    private bool Death()
    {
        return true;
    }

    private void Player_Movement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector2 mov = new Vector2(hor, ver);
        rb.velocity = movement_speed*mov;
    }

    private void Player_Look()
    {
        // convert mouse position into world coordinates
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // get direction you want to point at
        Vector2 direction = (mouseWorldPosition - (Vector2)transform.position).normalized;

        // set vector of transform directly
        transform.up = direction;
    }

    private void Fire_Projectile()
    {
        Weapon wep = Weapons[selectedWeapon];
        List<GameObject> projBuffer = new List<GameObject>();
        for(int i = 0; i < wep.RoundsPerShot; i++)
        {
            GameObject proj = Instantiate(wep.projectile, transform.position, Quaternion.identity);
            ProjectileManager prm = proj.GetComponent<ProjectileManager>();
            prm.color = wep.GunColor;
            prm.Team = m_Team;
            prm.playerSpawned = gameObject;
            // convert mouse position into world coordinates
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 diff = mouseWorldPosition - new Vector2(transform.position.x,transform.position.y);

            Vector2 halfleft = new Vector2(-diff.y, diff.x) / Mathf.Sqrt(Mathf.Pow(diff.x,2) + Mathf.Pow(diff.y, 2)) * (wep.SpreadAmount/2);

            // get direction you want to point at
            Vector2 direction = (mouseWorldPosition - (Vector2)transform.position).normalized;

            proj.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
        }      
    }

    private void ProjectileCollisionHandler(GameObject incoming_projectile)
    {
        ProjectileManager pm = incoming_projectile.GetComponent<ProjectileManager>();

        if (pm.playerSpawned.Equals(gameObject)) return; //Don't kill yourself: Call 1-800-273-8255

        incoming_projectile.GetComponent<Rigidbody2D>().velocity *= 0; //stop moving
        incoming_projectile.GetComponent<BoxCollider2D>().enabled = false;

        bool killed = false;
        if (pm.Team != m_Team) killed = Take_Damage(pm.Damage);
        else if (gm.FRIENDLY_FIRE_ENABLED) killed = Take_Damage(pm.Damage);

        if (killed)
        {
            pm.CausedKill();
            Destroy(gameObject);
        }
    }

    private void ChangeWeapon()
    {
        float dir = Input.GetAxis("Mouse ScrollWheel") * 10;
        if (dir < 0f) selectedWeapon = ((selectedWeapon - 1) + Weapons.Count) % Weapons.Count;
        else if (dir > 0f) selectedWeapon = ((selectedWeapon + 1) + Weapons.Count) % Weapons.Count;
    }

    // Use this for initialization
    void Start () {
        initializing = true;
        tm = GameObject.FindGameObjectWithTag("Team Manager").GetComponent<TeamManager>();
        spr = transform.Find("player_source").Find("background").GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<Game_Manager>();

        OnHealthChange += Dynamic_Color;
        OnTeamChange += Team_Change;
        Team_Change();

    }

    // Update is called once per frame
    void Update() {
        if (initializing)
        {
            Health = debug_health;
            Team = debug_team;
            initializing = false;
        }
    

        if (!debug_stationary)
        {
            Player_Look();
            if (Input.GetMouseButtonDown(0)) Fire_Projectile();
            ChangeWeapon();
        }
    }

    void FixedUpdate()
    {
        if(!debug_stationary) Player_Movement();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "projectile") ProjectileCollisionHandler(col.gameObject);
    }


}
