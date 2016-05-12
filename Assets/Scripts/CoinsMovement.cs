using UnityEngine;
using System.Collections;

public class CoinsMovement : MonoBehaviour {
    new Rigidbody rigidbody;

	// Use this for initialization
	void Start () 
    {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(0,1f,0));
	}

    void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponentInParent<TankCharacteristics>().Coins++;
        Destroy(gameObject);
    }
}
