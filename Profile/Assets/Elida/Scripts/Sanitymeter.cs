using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sanitymeter : MonoBehaviour
{

    public float MaxSanity = 100f;
    public float CurentSanity = 100f;
    public float Damage = 5f;
    public Slider SanitySlider;
    public bool Walking;

    void Start()
    {
        Walking = true;
    }


    void Update()
    {
        if (Walking == true && CurentSanity >= 0f)
        {

            CurentSanity = CurentSanity - Damage * Time.deltaTime;
            SanitySlider.value = CurentSanity / MaxSanity;
        }
        //Sound starts playing and the scren go's darker
        if (CurentSanity == 60f)
        {

        }

        else if (Walking == false && CurentSanity <= 100f )
        {
            CurentSanity = CurentSanity + Damage * Time.deltaTime;
            SanitySlider.value = CurentSanity / MaxSanity;
        }
    }

}
