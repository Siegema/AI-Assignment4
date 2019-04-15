using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAHazard : MonoBehaviour
{
    public float damage;
    public float continuosDmg; 

    private void OnTriggerEnter(Collider collider)
    {
        IsDamageable dmgable = collider.gameObject.GetComponent<IsDamageable>();
        if(dmgable)
        {
            dmgable.Damage(damage);
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        IsDamageable dmgable = collider.gameObject.GetComponent<IsDamageable>();
        if(dmgable)
        {
            dmgable.Damage(continuosDmg);
        }
        
    }
}
