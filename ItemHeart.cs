using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Hero hero = collider.GetComponent<Hero>();
        if (hero) {
            hero.Lifes++;
            Destroy(gameObject);
        }
    }
}
