using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSound : MonoBehaviour
{//Declaramos los sonidos de la caja
    AudioSource[] allSources;
    AudioSource movementSource;
    AudioSource impactSource;


    Rigidbody2D rb;
    bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {//Inicializamos 
        allSources = GetComponents<AudioSource>();
        movementSource = allSources[0];
        impactSource = allSources[1];

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void awake()
    {
        gameObject.AddComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        float v = rb.velocity.magnitude;
        //Si la velocidad de la caja es mayor que 2.0 y no está sonando, empieza a sonar el soido de movement
        if(v > 0.2 && !isPlaying )
        {
            print("the box is moving");
            movementSource.Play();
            isPlaying = true;
            
        }
        else if (v < 0.2 && isPlaying)
        {
            movementSource.Stop();
            isPlaying = false;
        }
    }

    void Update()
    {
        
    }
    //Utilizamos el OnCollision para hacer que suene el sonido de impact cada vez que la caja colisiona
    void OnCollisionEnter2D(Collision2D collision)
    {
        print("the box has collided");
        impactSource.Play();
    }
}
