using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovebleMonster : Monster
{
    [SerializeField]
    private float speed = 30.0f;
    private Vector3 direction;
    [SerializeField]
    private int Life = 3;

    
    private SpriteRenderer sprite;

    protected override void Awake() {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    protected override void Start()
    {
        direction = transform.right;
    }

    protected override void Update()
    {
        if (Life <= 0) {
            Destroy(gameObject);
        }
        Move();
    }

    
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if (unit && unit is Hero)
        {
            
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 5.0f) ReciveDamage();
            else unit.ReciveDamage();
        }
        Arrow arrow = collider.GetComponent<Arrow>();
        if (arrow) ReciveDamage();
        Damage damage = collider.GetComponent<Damage>();
        if (damage) ReciveDamage();
    }
    public override void ReciveDamage()
    {
        Life--;
    }

    private void Move() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 17.0f + transform.right * direction.x  * 5.0f, 1.0f);
        if (colliders.Length > 0 && colliders.All(x => !x.GetComponent<Hero>())) direction *= -1.0f;
       transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        sprite.flipX = direction.x < 0.0f;
    }

}

