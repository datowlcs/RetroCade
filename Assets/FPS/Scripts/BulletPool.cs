using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletPool : MonoBehaviour
{
    // Start is called before the first frame update

    //gets the main bulletpool of the scene
    public static BulletPool main;

    public GameObject bulletPrefab;
    public int poolSize = 100;

    private List<Bullet> availableBullets;


    private void Awake()
    {
        main = this;
    }

    void Start()
    {
        availableBullets = new List<Bullet>();

        for(int i=0; i<poolSize; i++)
        {
            Bullet b = Instantiate(bulletPrefab, transform).GetComponent<Bullet>();
            b.gameObject.SetActive(false);

            availableBullets.Add(b);
        }

    }

    public void PickFromPool(Vector3 position, Vector3 velocity)
    {
        if (availableBullets.Count < 1) return;

        availableBullets[0].Activate(position, velocity);

        availableBullets.RemoveAt(0);
    }

    public void AddToPool(Bullet bullet)
    {
        if (!availableBullets.Contains(bullet)) availableBullets.Add(bullet);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
