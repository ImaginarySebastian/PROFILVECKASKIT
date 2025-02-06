using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using System;


[System.Serializable]

public class ButtonEvent
{
    [SerializeField] string _buttonName = "";
    [SerializeField] UnityEvent _unityEvent;
    Button _button;

    public void Activate(UIDocument document)
    {
        if (_button== null)
        {
            _button = document.rootVisualElement.Q<Button>(_buttonName);

        }

        _button.clicked += _unityEvent.Invoke;
    }
    public void Inactivate(UIDocument document)
    {
        _button.clicked -= _unityEvent.Invoke;
    }
}
[System.Serializable]
public class ClickChange: UnityEvent<ClickEvent>
{

}
[System.Serializable]
public class BoolChange:UnityEvent<bool>
{

}
[System.Serializable]
public class ToggleEvent
{
    [SerializeField] string _toggleName = "";
    [SerializeField] BoolChange _boolEvent;

    Toggle _toggle;

    public void Activate(UIDocument document)
    {
        if (_toggle == null)
        {
            _toggle = document.rootVisualElement.Q<Toggle>(_toggleName);
        }
        _toggle.RegisterCallback<ChangeEvent<bool>>(evt => _boolEvent.Invoke(evt.newValue));
    }
    public void Inactivate(UIDocument document)
    {
        _toggle.UnregisterCallback<ChangeEvent<bool>>(evt => _boolEvent.Invoke(evt.newValue));
    }
}


public class Menu : MonoBehaviour
{
    [SerializeField] UIDocument _document;
    [SerializeField] List<ButtonEvent> _buttonEvents;
    [SerializeField] List<ToggleEvent> _toggleEvents;


    VisualElement _curMenu = null;

    public void SwitchMenu(string menuName)
    {
        if (_curMenu != null)
        {
            _curMenu.style.display = DisplayStyle.None;
        }
        _curMenu = _document.rootVisualElement.Q<VisualElement>(menuName);
        _curMenu.style.display = DisplayStyle.Flex;
    }

    private void OnEnable()
    {
        _buttonEvents.ForEach(Button => Button.Activate(_document));
        _curMenu = _document.rootVisualElement.Q<VisualElement>("MenuVisualTree");
        _toggleEvents.ForEach(Button => Button.Activate(_document));
    }

    private void OnDisable()
    {
        _buttonEvents.ForEach(button => button.Inactivate(_document));
        _toggleEvents.ForEach(button => button.Inactivate(_document));
    }
}







