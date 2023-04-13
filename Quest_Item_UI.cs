using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Quests;


    public class Quest_Item_UI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] TextMeshProUGUI progress;

        Quest_Status sta_tus;

        public void Setup_(Quest_Status sta_tus)
        {
            this.sta_tus = sta_tus;
            title.text = sta_tus.GetQuest_().Get_title();
            progress.text = sta_tus.Get_completed_Count() + "/" + sta_tus.GetQuest_().Get_Objective_count();
        }


        public Quest_Status Get_Quest_Status()
        {
            return sta_tus;
        }
    }
