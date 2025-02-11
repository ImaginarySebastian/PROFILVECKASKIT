using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;


public class HUDen : MonoBehaviour
{
    VisualElement root;
    VisualElement _healthContainer;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
    }
    // Start is called before the first frame update
    void Start()
    {
        _healthContainer = root.Q<VisualElement>("HealthContainer");
        if (_healthContainer != null)
        {
            GameSession session = FindObjectOfType<GameSession>();
            if (session != null)
            {
                CreateHearts(_healthContainer, (uint)session.playerLives);
            }
            else
            {
                Debug.LogWarning("Gamesessiassfsahf");
            }
        }
        else
        {
            Debug.LogWarning("Healthcontainer notnsadasf");
        }
    }
    private void CreateHearts(VisualElement HealthContainer, uint hearts)
    {
        HealthContainer.Clear();
        for(int i = 0; i < hearts; i++)
        {
            VisualElement token = new VisualElement();
            token.AddToClassList("heart");
            HealthContainer.Add(token);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
