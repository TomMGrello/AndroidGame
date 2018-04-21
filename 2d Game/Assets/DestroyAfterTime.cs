using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {
    private float time;
    public float TimeToDestroy = 1f;

	// Update is called once per frame
	void Update () {
        if (time >= TimeToDestroy)
            Destroy(gameObject);
        time += Time.deltaTime;
	}
}
