using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{

    AudioSource[] allSources;
    AudioSource jumpSource;
    AudioSource landSource;
    AudioSource crouchSource;
    AudioSource runSource;
    AudioSource cherrySource;

    // keep track of the jumping state ... 
    bool isJumping = false;
    // make sure to keep track of the movement as well !

    bool isPlaying = false;
    //normal pitch of jump and lans sound, needed for altering the pitch
    float jumpPitch = 1.0f;
    float landPitch = 1.0f;
    

    Rigidbody2D rb; // note the "2D" prefix 
    
    // Start is called before the first frame update
    void Start()
    {
        // get the references to your audio sources here !   
        allSources = GetComponents<AudioSource>();
        jumpSource = allSources[0];
        landSource = allSources[1];
        crouchSource = allSources[2];
        runSource = allSources[3];
        cherrySource = allSources[4];

        rb = GetComponent<Rigidbody2D>();
	     
    }

    // FixedUpdate is called whenever the physics engine updates
    void FixedUpdate()
    {
        // Use the ridgidbody instance to find out if the fox is
        // moving, and play the respective sound !
        // Make sure to trigger the movement sound only when
        // the movement begins ...

        // Use a magnitude threshold of 1 to detect whether the
        // fox is moving or not !
        // i.e.
        // if ( ??? > 1 && ???) {
        //    play sound here !
        // } else if ( ??? < 1 &&) {
        //   stop sound here !
        // }	

        float v = rb.velocity.magnitude;
        //Si la velocidad es más alta que 1, no hay musica sonando y no está saltando, suena la música de correr
        if (v > 1 && !isPlaying && !isJumping)
        {
            print("the fox is running");
            runSource.Play();
            isPlaying = true;
            isJumping = false;   
        }//Si la velocidad es inferior a 1 y hay música sonando, se para la música de correr
        else if (v < 1 && isPlaying )
        {
            runSource.Stop();
            isPlaying = false;
        }
        //Si está saltando, se para la música de correr
        else if (isJumping)
        {
            runSource.Stop();
            isJumping = true;          
        }
    }

    // trigger your jumping sound here !
    public void OnJump()
    {//Generamos un número random entre 0 y 100, si el número es menor que 50 se alterará el pitch del sonido de salto
        int randomNumber = Random.Range(0, 100);
        Debug.Log("Random Number of jump is " + randomNumber);
        float randomModifier = Random.Range(0.2f, 1.8f);//generamos un numero random para poder modificar el pitch y calcular el pitch final
        float finalPitch = jumpPitch * randomModifier;

        if (randomNumber < 50)
        {
            jumpSource.pitch = finalPitch;
        }
        isJumping = true;
        print("the fox has jumped");
        jumpSource.Play();    
    }

    // trigger your landing sound here !
    
   public void OnLanding() 
    {//Hacemos lo mísmo que en el void onjump
        int randomNumber = Random.Range(0, 100);
        Debug.Log("Random Number of land is " + randomNumber);
        float randomModifier = Random.Range(0.5f, 1.5f);
        float finalPitch = landPitch * randomModifier;

        if (randomNumber < 50)
        {
            landSource.pitch = finalPitch;
        }
        
        isJumping = false;
        print("the fox has landed");
        if (!isJumping && !isPlaying)// si ha saltado y ha parado de correr, suena el sonido de land
        {
            landSource.Play();
        }  
    // to keep things cleaner, you might want to
	// play this sound only when the fox actually jumped ...
    }

    // trigger your crouching sound here
    public void OnCrouching()
    {
        print("the fox is crouching");
        crouchSource.Play();
    }

    // trigger your cherry collection sound here !
    public void OnCherryCollect() {
        print("the fox has collected a cherry");
        cherrySource.Play();    
    }
}


