using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TankAbilitys : MonoBehaviour 
{
    const int NormalBulletCapacity = 100;
    const int ReboundBulletCapacity = 10;
    public int NormalBullets = 100;
    public int ReboundBullets = 100;

    public GameObject NormalBullet;
    public GameObject ReboundBullet;
    GameObject UsedBullet;
    new Rigidbody rigidbody;

	void Start () 
    {
        rigidbody = GetComponent<Rigidbody>();
        UsedBullet = NormalBullet;
	}
	
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UsedBullet = NormalBullet;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UsedBullet = ReboundBullet;
        }

        if (Input.GetMouseButtonDown(0))
        {
            NormalBullets--;
            Instantiate(UsedBullet, new Vector3(rigidbody.position.x, rigidbody.position.y, rigidbody.position.z), Quaternion.Euler(new Vector3(rigidbody.rotation.eulerAngles.x, rigidbody.rotation.eulerAngles.y + 90f, rigidbody.rotation.eulerAngles.z)));
        }
	}
}
