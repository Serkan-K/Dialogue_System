using GameDevTV.Inventories;
using GameDevTV.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Quests
{
    public class Quest_List : MonoBehaviour, ISaveable, IPredicateEvaluator
    {
        List<Quest_Status> statuses = new List<Quest_Status>();

        public event Action onUpdate;

        public void Add_Mission(Quest_ ques_t)
        {

            if (Has_quest(ques_t)) return;

            Quest_Status new_Status = new Quest_Status(ques_t);
            statuses.Add(new_Status);

            if (onUpdate != null)
            {
                onUpdate();
            }
        }



        public void Complete_objcetive(Quest_ q_uest_complet, string objective_complet)
        {
            Quest_Status status_comp = get_Quest_status(q_uest_complet);
            status_comp.Complete_objective(objective_complet);



            if (status_comp.Is_Complete())
            {
                Give_Reward(q_uest_complet);
            }



            if (onUpdate != null)
            {
                onUpdate();
            }
        }

        public bool Has_quest(Quest_ ques_t)
        {
            return get_Quest_status(ques_t) != null;
        }

        public IEnumerable<Quest_Status> Get_Statuses()
        {
            return statuses;
        }




        private void Give_Reward(Quest_ q_uest_complet)
        {
            foreach(var reward_list in q_uest_complet.Get_Rewards())
            {
               bool succes_item = GetComponent<Inventory>().AddToFirstEmptySlot(reward_list.item, reward_list.number);

                if (!succes_item)
                {
                    GetComponent<ItemDropper>().DropItem(reward_list.item, reward_list.number);
                }
            }
        }





        private Quest_Status get_Quest_status(Quest_ ques_t)
        {
            foreach (Quest_Status statu_s in statuses)
            {
                if (statu_s.GetQuest_() == ques_t)
                {
                    return statu_s;
                }
            }
            return null;
        }






        public object CaptureState()
        {
            List<object> state_ = new List<object>();

            foreach(Quest_Status status_save in statuses)
            {
                state_.Add(status_save.CaptureState());
            }
            return state_;
        }

        public void RestoreState(object state)
        {
            List<object> state_List = state as List<object>;

            statuses.Clear();

            foreach(object objectState_ in state_List)
            {
                statuses.Add(new Quest_Status(objectState_));
            }
        }

        public bool? Evaluate(string predicate, string[] parameters)
        {
            switch (predicate)
            {
                case "HasQuest":
                    return Has_quest(Quest_.Get_by_Name(parameters[0]));
                case "ComletedQuest":
                    return get_Quest_status(Quest_.Get_by_Name(parameters[0])).Is_Complete();

            }
            return null;
        }
    }
}


