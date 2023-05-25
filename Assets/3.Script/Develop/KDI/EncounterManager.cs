using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterManager : MonoBehaviour
{
    public static EncounterManager instance = null;

    public Text txtName;
    public Text txtContext;

    [SerializeField] private Transform parent;
    public enum Type
    {
        town,
        interactiveObject,
        enemy,
        dungeon
    }
    public Type type;

    private void Awake()
    {
        instance = this;
        parent = transform.parent;
    }
    public void ActiveEncounter(string name, string content)
    {
        txtName.text = name;
        txtContext.text = content;

        if (type == Type.town) 
        {
            parent.GetChild(1).gameObject.SetActive(true); //EncountUI on
            parent.GetChild(2).gameObject.SetActive(false); //SlotUI off
        }
        else if (type == Type.interactiveObject)
        {

        }
        else if (type == Type.enemy)
        {

        }
        else if (type == Type.dungeon)
        {
            parent.GetChild(1).gameObject.SetActive(true); //EncountUI on
            parent.GetChild(2).gameObject.SetActive(false);
        }
    }

    public void ExitButton()
    {
        parent.GetChild(1).gameObject.SetActive(false); //EncountUI off
        parent.GetChild(2).gameObject.SetActive(false); //SlotUI off
    }
}
