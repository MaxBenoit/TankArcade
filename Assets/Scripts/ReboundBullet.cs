using UnityEngine;
using System.Collections;

public class ReboundBullet : Bullet 
{
    public GameObject bullet;
    new Rigidbody rigidbody;
    int delay = 0;
    public int speed;
    Vector3 Orientation;

	// Use this for initialization
	void Start () 
    {
        rigidbody = GetComponent<Rigidbody>();
        Orientation = transform.right;
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = Orientation * speed * Time.deltaTime;
        rigidbody.MovePosition(rigidbody.position - movement);
        delay++;
        if (delay > 1000)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "WallPerpendicular")
        {
            Instantiate(bullet, new Vector3(rigidbody.position.x, rigidbody.position.y, rigidbody.position.z), Quaternion.Euler(new Vector3(rigidbody.rotation.eulerAngles.x, 180 - rigidbody.rotation.eulerAngles.y, rigidbody.rotation.eulerAngles.z)));
        }
        if (collision.gameObject.tag == "WallParallel")
        {
            Instantiate(bullet, new Vector3(rigidbody.position.x, rigidbody.position.y, rigidbody.position.z), Quaternion.Euler(new Vector3(rigidbody.rotation.eulerAngles.x, 360 - rigidbody.rotation.eulerAngles.y, rigidbody.rotation.eulerAngles.z)));
        }
        Destroy(gameObject);
    }
}
