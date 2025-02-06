using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class HUDen : MonoBehaviour
{
    [SerializeField] PlayerHealth pla;
    VisualElement root;
    ProgressBar healthbar;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
    }
    // Start is called before the first frame update
    void Start()
    {
        healthbar = root.Q<ProgressBar>("HealthBar");
    }

    // Update is called once per frame
    void Update()
    {
        if(pla != null && healthbar != null)
        {
            //healthbar.value = pla.playerHealth;
        }
    }
}
