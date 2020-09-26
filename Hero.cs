using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero : Unit
{
    [SerializeField]
    public int Life = 5;  
    public int Lifes {
        get { return Life; }
        set
            {
            if (value <= 5) Life = value;
            lifesBar.Refresh();
        }
    }

   private LifesBar lifesBar;
    [SerializeField]
    public int arrowsItem = 5;
    public int ArrowsItem {
        get { return arrowsItem; }
        set {
            if (value <= 5) arrowsItem = value;
            arrowsBar.RefreshArrows();
        }
    }
    private ArrowsBar arrowsBar;
    [SerializeField]
    public float speed = 20f;
    [SerializeField]
    public float jumpeForce = 15f;
    private bool isGrounded = false;
    private Arrow arrow;
    [SerializeField]
    public bool attack = true;
    private Damage damage;
    

    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer sprite;
    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }
    private void Awake()
    {
        arrowsBar = FindObjectOfType<ArrowsBar>();
        lifesBar = FindObjectOfType<LifesBar>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        arrow = Resources.Load<Arrow>("Arrow");// Загрузка префаба 
        damage = Resources.Load<Damage>("Circle");
    }
    private void FixedUpdate()
    {
        CheckGround();
    }
    private void Update()
    {
        if (isGrounded) State = CharState.Ilde;
        if (Life <= 0)
        {
            State = CharState.Dead;
            Invoke("ReloadLevel", 3);
        }
        if (Input.GetButton("Horizontal")) Run();
        if (isGrounded && Input.GetButtonDown("Jump")) Jump();
        if (Input.GetButton("Fire1")) Attack();
        if (Input.GetButtonDown("Fire2") && arrowsItem != 0)
        {
            arrowsItem--;
            Shoot();
            Debug.Log(arrowsItem);
        }
        if (Input.GetButton("Fire3")) Protected();



    }

    
    private void Run()
    {
        if (isGrounded) State = CharState.Run;
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        sprite.flipX = direction.x < 0.0f;
    }

    private void Jump()
    {
        State = CharState.Jump;
        rigidBody.AddForce(transform.up * jumpeForce, ForceMode2D.Impulse);
    }

    private void Shoot()
    {
        Vector3 position = transform.position; position.y += 9.0f; position.x += 9.0f;// Откуда происходит выстрел 
        Arrow newArrow = Instantiate(arrow, position, arrow.transform.rotation) as Arrow; //Создание стрелы

        newArrow.Parent = gameObject;
        newArrow.Direction = newArrow.transform.right * (sprite.flipX ? -1.0f : 1.0f);
    }
    private void Attack()
    {
        if (attack == true)
        {
            attack = false;
            Invoke("AttackReset", 1);
            State = CharState.Hit;
            if (sprite.flipX == false)
            {
                Vector3 position = transform.position; position.y += 9.0f; position.x += 7.0f;
                Damage newDamage = Instantiate(damage, position, damage.transform.rotation) as Damage;

                newDamage.Parent = gameObject;
                newDamage.Direction = newDamage.transform.right * (sprite.flipX ? -1.0f : 1.0f);
            }
            else if (sprite.flipX == true) {
                Vector3 position = transform.position; position.y += 9.0f; position.x -= 7.0f;
                Damage newDamage = Instantiate(damage, position, damage.transform.rotation) as Damage;

                newDamage.Parent = gameObject;
                newDamage.Direction = newDamage.transform.right * (sprite.flipX ? -1.0f : 1.0f);
            }
        }
    }
    private void AttackReset() {
        attack = true;
    }


    private void Protected() {
        State = CharState.Protection;
    }
   
    public override void ReciveDamage()
    {
        Lifes--;
        rigidBody.velocity = Vector3.zero;
        rigidBody.AddForce(transform.up * 100.0f, ForceMode2D.Impulse);
        State = CharState.Damage;

        
    }

    private void CheckGround()
    {
        if (!isGrounded) State = CharState.Jump;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = colliders.Length > 1;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Arrow arrow = collider.gameObject.GetComponent<Arrow>();
        if (arrow && Input.GetButton("Fire3"))
        {
            Destroy(arrow);
        }
        else if (arrow && arrow.Parent != gameObject) {
            ReciveDamage();
        }
        Damage damage = collider.gameObject.GetComponent<Damage>();
        if (damage && damage.Parent != gameObject) {
            ReciveDamage();
        }
         if (collider.gameObject.tag == "FinishLevel") { // переход на другую сцену 
            SceneManager.LoadScene(2);
        }
    }
    private void ReloadLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

public enum CharState { 
Ilde,
Run,
Jump,
Dead,
Damage,
Protection,
Hit,
}
