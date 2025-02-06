using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;


public class Sanitymeter : MonoBehaviour
{

    public float MaxSanity = 100f;
    public float CurentSanity = 100f;
    public float Damage = 5f;
    public Slider SanityMeter;
    public bool Walking;
    public GameObject rat;
    public float RestorAmunt = 20f;


    //Add audio qeue and light qeue
    public Camera mainCamera;
    public AudioClip ringing;
    public AudioSource Highpitchedringing;
    public Image OverLay;


    void Start()
    {
       
        Walking = true;

        if (SanityMeter == null)
        {
            Debug.LogError("\"SanityMeter (Slider) is not assigned in the inspector!\"");
        }

        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if(OverLay == null)
        {
            Debug.LogError("OverLay (Image) is not assigned in the inspector!");
            if (OverLay != null)
            {
                OverLay.color = new Color(0, 0, 0, 0);
            }
        }
    }


    void Update()
    {
        if (Walking == true && CurentSanity >= 0f)
        {
            //Sanity goes down all the time ;)
            CurentSanity = CurentSanity - Damage * Time.deltaTime;
            CurentSanity = Mathf.Clamp(CurentSanity, 0f, MaxSanity);
            SanityMeter.value = CurentSanity;

            Darkenscren();
        }
        //Sound starts playing and the scren go's darker
       
        
        
        if (CurentSanity == 60f && Highpitchedringing && !Highpitchedringing.isPlaying)
        {
            Highpitchedringing.PlayOneShot(ringing);
           
        }
        
    }
    //If you "eat" or pick up a rat sanity restors
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Restorsanity"))
        {
            Restorsanity();

        }
    }
    //gives back the sanity
    void Restorsanity()
    {
        Debug.Log("Before restore: " + CurentSanity);

       CurentSanity += RestorAmunt;
        CurentSanity = Mathf.Clamp(CurentSanity, 0f, MaxSanity);
        Debug.Log("Sanity Restored! Current sanity" + CurentSanity);

        if(SanityMeter != null)
        {
            SanityMeter.value = CurentSanity;
        }
        Darkenscren();
        Debug.Log("After restore: " + CurentSanity);
    }

    void Darkenscren()
    {
        //The Transision to darkscreen =)
        if(OverLay != null)
        {
            float targetAlpha = 1f - CurentSanity / MaxSanity;
            OverLay.color = new Color(0, 0, 0, Mathf.Lerp(OverLay.color.a, targetAlpha, Time.deltaTime * 100f));
            Debug.Log("OverLay alpha: " + OverLay.color.a);
        }
    }
}
