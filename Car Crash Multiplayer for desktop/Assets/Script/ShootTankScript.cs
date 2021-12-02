﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTankScript : MonoBehaviour {

	PhotonView view;
	public GameObject Bullet;
	public int force = 30;
	Rigidbody rb;
    private bool ShootBool = true;

	// Use this for initialization
	void Start () {
		view = GetComponent<PhotonView> ();
	}

	// Update is called once per frame
	void Update () {


		if (view.isMine && Input.GetKeyDown (KeyCode.Space) && ShootBool) {

            ShootBool = false;
            StartCoroutine(SetShootBoolBack());
				view.RPC ("shoot", PhotonTargets.All, transform.Find ("ShootPosition").transform.position, transform.Find ("ShootPosition").transform.rotation);
 
		}
	}

    IEnumerator SetShootBoolBack()
    {
        yield return new WaitForSeconds(0.5f);
        ShootBool = true;

    }



    [PunRPC]
	void shoot(Vector3 Pos, Quaternion quaat)
	{
		GameObject GO = Instantiate (Bullet, Pos, quaat) as GameObject;
		GO.GetComponent<Rigidbody> ().velocity = transform.TransformDirection (Vector3.forward * force);
	}


				}
