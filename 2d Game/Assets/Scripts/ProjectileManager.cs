﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour {

    public Color32 color;
    public char Team;
    public int Damage = 10;
    public const byte FADE_AMOUNT = 50;
    public const byte LIFETIME = 5;
    public GameObject playerSpawned;

    private float time_count = 0f;

    void Start()
    {
        TrailRenderer tr = GetComponent<TrailRenderer>();
        tr.startColor = color;
        tr.endColor = new Color32((byte)(color.r - FADE_AMOUNT), (byte)(color.g - FADE_AMOUNT), (byte)(color.b - FADE_AMOUNT), (byte)(color.a - FADE_AMOUNT));
    }

    void Update()
    {
        time_count += Time.deltaTime;
        if (time_count >= LIFETIME) Destroy(gameObject);
    }

    public void CausedKill()
    {
        Debug.Log("KILL!");
    }
}