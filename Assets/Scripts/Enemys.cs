using UnityEngine;
using System.Collections;

public class Enemys : MonoBehaviour 
{
    public GameObject Coin;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
            Instantiate(Coin, transform.position + new Vector3(0,2f,0), Quaternion.identity);
        }
    }
}
