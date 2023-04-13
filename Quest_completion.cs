using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Quests
{
    public class Quest_completion : MonoBehaviour
    {
        [SerializeField] Quest_ q_uest_complet;
        [SerializeField] string objective_complet_1;
        [SerializeField] string objective_complet_2;
        [SerializeField] string objective_complet_3;


        public void Complete_objcetive()
        {
            Quest_List quest_complet_List_ = GameObject.FindGameObjectWithTag("Player").GetComponent<Quest_List>();
            quest_complet_List_.Complete_objcetive(q_uest_complet, objective_complet_1);
            quest_complet_List_.Complete_objcetive(q_uest_complet, objective_complet_2);
            quest_complet_List_.Complete_objcetive(q_uest_complet, objective_complet_3);
        }           
    }
}

