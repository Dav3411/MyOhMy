using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletMoveSpeed = 15f;
    Rigidbody bulletRig;
	// Use this for initialization
	void Start () {
        bulletRig = gameObject.GetComponent<Rigidbody>();
        Vector3 vecAddPos = transform.rotation * Vector3.forward * bulletMoveSpeed;
        bulletRig.AddForce(vecAddPos, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
        //transform.position += ((transform.rotation * vecAddPos) * Time.deltaTime);
        // addforce
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("1");
        Destroy(this.gameObject);
        
    }
}
