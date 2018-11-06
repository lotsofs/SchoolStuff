using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[NonSerialized] public CharacterController player;
	[SerializeField] float firingRate = 5;
	[SerializeField] CannonBall cannonBall;
	[SerializeField] Transform cannonBallSpawn;
	float lastShotTimer;

	// Use this for initialization
	void Start () {
		lastShotTimer = UnityEngine.Random.value * 5;	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = LookAtPlayer();
		lastShotTimer += Time.deltaTime;
		if (lastShotTimer >= firingRate) {
			lastShotTimer = 0;
			Shoot(direction);
		}
	}

	/// <summary>
	/// Point at the player
	/// </summary>
	/// <returns></returns>
	Vector3 LookAtPlayer() {
		Vector3 direction = player.transform.position - this.transform.position;
		transform.rotation = Quaternion.LookRotation(direction);
		return direction;
	}

	/// <summary>
	/// Creates a cannonball that flies towards the player
	/// </summary>
	/// <param name="direction"></param>
	void Shoot(Vector3 direction) {
		direction = direction.normalized;
		CannonBall cb = Instantiate(cannonBall, cannonBallSpawn.position, UnityEngine.Random.rotation);
		cb.direction = direction;
	}

}
