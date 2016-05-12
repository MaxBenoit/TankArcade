using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StandingEnemy : MonoBehaviour 
{
    GameObject UsedBullet;
    public GameObject NormalBullet;
    public GameObject ReboundBullet;
    GameObject Target;
    List<GameObject> TargetList;
    new Rigidbody rigidbody;
    int ShootIndex = 0;
	// Use this for initialization
	void Start () 
    {
        UsedBullet = NormalBullet;
        rigidbody = GetComponent<Rigidbody>();
        TargetList = new List<GameObject>();
        foreach (GameObject go in FindObjectsOfType<GameObject>())
        {
            if (go.tag == "Player")
            {
                TargetList.Add(go);
            }
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        CheckClosestPlayer();
        Rotate();
        if (ShootIndex > 500)
        {
            ShootIndex = 0;
            Instantiate(UsedBullet, new Vector3(rigidbody.position.x, rigidbody.position.y, rigidbody.position.z), Quaternion.Euler(new Vector3(rigidbody.rotation.eulerAngles.x, rigidbody.rotation.eulerAngles.y + 90f, rigidbody.rotation.eulerAngles.z)));
        }
        ShootIndex++;
	}

    void CheckClosestPlayer()
    {
        foreach (GameObject go in TargetList)
        {
            if (Target == null)
            {
                Target = go;
            }

            if (Vector3.Distance(transform.position, go.transform.position) < Vector3.Distance(transform.position, Target.transform.position))
            {
                Target = go;
            }
        }
    }

    void Rotate()
    {
        Vector3 Orientation = Target.transform.position - transform.position;
        transform.rotation = Quaternion.Euler(0,0,0);
    }
}
