using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacles : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if (unit && unit is Hero)
        {
            unit.ReciveDamage();
        }
        Arrow arrow = collider.GetComponent<Arrow>();
        if (arrow)
        {
            Destroy(arrow);
            Destroy(gameObject);
        }
    }
    
}
