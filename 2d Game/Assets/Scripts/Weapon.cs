using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public GameObject projectile;
    public enum FireModes{SINGLE,BURST2,BURST3,SPREAD3,SPREAD5,FULLAUTO}
    public FireModes FireMode;
    public float RateOfFire = 0.25f;
    public float RateOfBurstShots = 0.125f;
    public int RoundsPerShot = 1;
    public int MagazineCapacity = 20;
    public float SpreadAmount = 5f;
    public Color32 GunColor = new Color32(255, 128, 64, 255);
}
