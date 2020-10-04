using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    public virtual void ReceiveDamage()
    {
        Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void Respawn()
    {
        gameObject.transform.position = new Vector3(-115, -0.2F, 0);
    }
}
