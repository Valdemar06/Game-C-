using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsBar : MonoBehaviour
{
    private Transform[] Arrows = new Transform[5];
    private Hero hero;

    private void Awake()
    {
        hero = FindObjectOfType<Hero>();
        for (int i = 0; i < Arrows.Length; i++) {
            Arrows[i] = transform.GetChild(i);
            Debug.Log(Arrows[i]);
        }
    }

    public void RefreshArrows()
    {
        for (int i = 0; i < Arrows.Length; i++)
        {
            if (i < hero.ArrowsItem) Arrows[i].gameObject.SetActive(true);
            else Arrows[i].gameObject.SetActive(false);
        }

    }
}
