using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Quests;
using TMPro;
using System;

namespace RPG.Quests
{
    public class Quest_tooltip_UI : MonoBehaviour
    {

        [SerializeField] TextMeshProUGUI title;
        [SerializeField] TextMeshProUGUI reward_text;
        [SerializeField] Transform objcetive_Container;
        [SerializeField] GameObject objective_Prefab;
        [SerializeField] GameObject objective_incomplete_prefab;
       



        public void Setup(Quest_Status _status_)
        {
            Quest_ quest_s = _status_.GetQuest_();

            title.text = quest_s.Get_title();

            foreach (Transform item_ in objcetive_Container)
            {
                Destroy(item_.gameObject);
            }


            foreach (var objective_ in quest_s.Get_Objective())
            {

                GameObject prefab_ = objective_incomplete_prefab;

                if (_status_.Is_objective_completed(objective_.reference))
                {
                    prefab_ = objective_Prefab;
                }


                GameObject objective_Instance = Instantiate(prefab_, objcetive_Container);
                TextMeshProUGUI objective_Text = objective_Instance.GetComponentInChildren<TextMeshProUGUI>();
                objective_Text.text = objective_.description;
            }


            reward_text.text = Get_Reward_text(quest_s);

        }


        private string Get_Reward_text(Quest_ quest_s)
        {
            string reward_text = "";

            foreach(var reward_tooltip_UI in quest_s.Get_Rewards())
            {
                if (reward_text != "")
                {
                    reward_text += ", ";
                }

                if (reward_tooltip_UI.number > 1)
                {
                    reward_text += reward_tooltip_UI.number + " ";
                }


                reward_text += reward_tooltip_UI.item.GetDisplayName();
            }

            if (reward_text == "")
            {
                reward_text = "No reward";
            }

            reward_text += ".";

            return reward_text;
        }
    }
}


