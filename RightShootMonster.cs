using UnityEngine;
using System.Collections;

public class RightShootMonster : Monster
{
    [SerializeField]
    private float rate = 2.0F;
    [SerializeField]
    private Color bulletColor = Color.white;
    private Animator animator;

    private Arrow arrow;

    protected override void Awake()
    {
        arrow = Resources.Load<Arrow>("Arrow");
    }

    protected override void Start()
    {
        InvokeRepeating("Shoot", rate, rate);
    }

    private void Shoot()
    {
        Vector3 position = transform.position; position.y += 9.0F;
        Arrow newBullet = Instantiate(arrow, position, arrow.transform.rotation) as Arrow;

        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right;
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit is Hero)
        {

            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 2.2F) ReciveDamage();
            else unit.ReciveDamage();
        }
        Arrow arrow = collider.GetComponent<Arrow>();
        if (arrow && arrow.Parent != gameObject)
        {
            ReciveDamage();
        }
    }
}
