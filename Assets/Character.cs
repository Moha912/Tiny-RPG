using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed = 5f;
    Rigidbody2D rb;
    Vector2 dir;
    Animator anim;

    public GameObject quete1;
    public BoxCollider2D colliderPnj1;

    public GameObject quete2;
    bool chestOk = false; // quete terminée ??
    public BoxCollider2D colliderPnj;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        rb.MovePosition(rb.position + dir * speed * UnityEngine.Time.deltaTime );

        SetParam();
    }

    void SetParam()
    {
        if(dir.x == 0 && dir.y == 0)
            anim.SetInteger("dir", 0);
    
        else if(dir.y < 0) // Bas
            anim.SetInteger("dir", 1);


        else if(dir.x > 0){ // Doite
            anim.SetInteger("dir", 2);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(dir.x < 0){ // Gauche
            anim.SetInteger("dir", 2);
            GetComponent<SpriteRenderer>().flipX = false;
        }

        else if(dir.y > 0) // Haut
            anim.SetInteger("dir", 3);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name == "quete0"){
            Destroy(collision.gameObject.GetComponent<CircleCollider2D>());
            quete1.SetActive(true);
            StartCoroutine("HideQuest");
        }

        if(collision.gameObject.name == "Legume"){
            Destroy(collision.gameObject);
            Destroy(colliderPnj1);
        }

        if(collision.gameObject.name == "quete1"){
            Destroy(collision.gameObject.GetComponent<CircleCollider2D>());
            quete2.SetActive(true);
            StartCoroutine("HideQuest");
        }

        if(collision.gameObject.name == "chest"){
            Destroy(collision.gameObject);
            chestOk = false; // quete fini
            Destroy(colliderPnj);      
        }
    }

    IEnumerator HideQuest(){
        yield return new WaitForSeconds(3);
        quete1.SetActive(false);
        quete2.SetActive(false);
    }
}
