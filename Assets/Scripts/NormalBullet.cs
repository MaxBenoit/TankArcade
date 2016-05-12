using UnityEngine;
using System.Collections;

public class NormalBullet : Bullet 
{
    new Rigidbody rigidbody;
    int delay = 0;
    public int speed;

	// Use this for initialization
	void Start () 
    {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate() 
    {
        Vector3 movement = transform.right * speed * Time.deltaTime;
        rigidbody.MovePosition(rigidbody.position - movement);
        delay++;
        if (delay > 1000)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
