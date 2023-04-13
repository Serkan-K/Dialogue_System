using RPG.Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_List_UI : MonoBehaviour
{    
    [SerializeField] Quest_Item_UI quest_prefab;
    Quest_List qu_est_List;

    void Start()
    {
        qu_est_List = GameObject.FindGameObjectWithTag("Player").GetComponent<Quest_List>();
        qu_est_List.onUpdate += reDraw;

        reDraw();
    }




    private void reDraw()
    {
        foreach (Transform item_ in transform)
        {
            Destroy(item_.gameObject);
        }

        foreach (Quest_Status status_ in qu_est_List.Get_Statuses())
        {
            Quest_Item_UI ui_Instance = Instantiate<Quest_Item_UI>(quest_prefab, transform);
            ui_Instance.Setup_(status_);
        }
    }
}
