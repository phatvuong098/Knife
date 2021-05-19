using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Event_SelectKnife(string name);

public class UI_KnifeSelect : MonoBehaviour
{
    public static Event_SelectKnife OnKnifeSelect;

    [SerializeField]
    GameObject panel;

    private static UI_KnifeSelect _instance;

    public static GameObject Panel { get => _instance.panel; }

    private void Awake()
    {
        _instance = this;
    }

    public static void ShowPanel()
    {
        Panel.SetActive(true);
    }

    public static void HidePanel()
    {
        Panel.SetActive(false);
    }

    public void BTN_OnKnifeSelect(string name)
    {
        OnKnifeSelect?.Invoke(name);
        Panel.SetActive(false);
    }
}
