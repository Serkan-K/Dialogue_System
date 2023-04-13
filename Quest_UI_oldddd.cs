using RPG.Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Quest_UI_oldddd : MonoBehaviour
    {
        [SerializeField] Quest_old quest;

        void Start()
        {

            foreach (string task in quest.GetTask())
            {
                Debug.Log(task);
            }
        }

    }



