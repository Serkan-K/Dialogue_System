using GameDevTV.Inventories;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RPG.Quests
{
    [CreateAssetMenu(fileName = "New_Quest", menuName = "Quest", order =0)]

    public class Quest_ : ScriptableObject
    {
        [SerializeField] List<Objective> objectives = new List<Objective>();
        [SerializeField] List<Reward> rewards = new List<Reward>();

        [System.Serializable]
        public class Reward
        {
            [Min(1)]
            public int number;
            public InventoryItem item;
        }

        [System.Serializable]
        public class Objective
        {
            public string reference;
            public string description;
        }








 
        public string Get_title()
        {
            return name;
        }

        public int Get_Objective_count()
        {
            return objectives.Count;
        }


        public IEnumerable<Objective> Get_Objective()
        {
            return objectives;
        }


        public IEnumerable<Reward> Get_Rewards()
        {
            return rewards;
        }










        public bool Has_objective(string objective_ref)
        {
            foreach(var objective_0 in objectives)
            {
                if (objective_0.reference == objective_ref)
                {
                    return true;
                }               
            }
            return false;
        }



        public static Quest_ Get_by_Name(string quest_name)
        {
            foreach(Quest_ quest_u in Resources.LoadAll<Quest_>(""))
            {
                if (quest_u.name == quest_name)
                {
                    return quest_u;
                }
            }
            return null;
        }


    }
}

