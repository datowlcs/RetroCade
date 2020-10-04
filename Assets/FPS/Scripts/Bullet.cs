using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{

    public Rigidbody rbody;

    public float lifetime;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Activate(Vector3 position, Vector3 velocity)
    {
        transform.position = position;
        rbody.velocity = velocity;

        gameObject.SetActive(true);

        StartCoroutine("Decay");
    }

    private IEnumerator Decay()
    {
        yield return new WaitForSeconds(lifetime);

        Deactivate();
    }

    public void Deactivate()
    {
        BulletPool.main.AddToPool(this);

        StopAllCoroutines();

        gameObject.SetActive(false);
    }

    //This is where we handle what happens when a bullet hits an enemy
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("We hit something");

        if (other.gameObject.tag == "Germ")
        {
            Destroy(other.gameObject);

            Deactivate();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
