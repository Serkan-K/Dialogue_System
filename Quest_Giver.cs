using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Quests
{

    public class Quest_Giver : MonoBehaviour
    {
        [SerializeField] Quest_ quest_give;


        public void Give_Quest()
        {
            Quest_List quest_giver_List_ = GameObject.FindGameObjectWithTag("Player").GetComponent<Quest_List>();
            quest_giver_List_.Add_Mission(quest_give);
        }
    }
}

