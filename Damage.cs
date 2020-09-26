using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
   [SerializeField]
    private GameObject parent;
    public GameObject Parent { set { parent = value; } get { return parent; } }
    private Vector3 direction;
    public Vector3 Direction { set { direction = value; } }
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if (unit && unit.gameObject != parent)
        {
            Destroy(gameObject);
        }
    }
}
