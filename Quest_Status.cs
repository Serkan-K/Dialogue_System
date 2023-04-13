using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{

    public class Quest_Status
    {
         Quest_ quest_ui_;
         List<string> completed_Objective = new List<string>();

        [System.Serializable] class Quest_Status_Record
        {
           public string quest_Name_;
           public List<string> completed_Objective_s;
        }



        private Quest_ ques_t;

        public Quest_Status(Quest_ ques_t)
        {
            this.ques_t = ques_t;
        }



        public Quest_Status(object objectState_)
        {
            Quest_Status_Record state_o = objectState_ as Quest_Status_Record;
            quest_ui_ = Quest_.Get_by_Name(state_o.quest_Name_);
            completed_Objective = state_o.completed_Objective_s;
        }




        public Quest_ GetQuest_()
        {
            return ques_t;
        }

        public bool Is_Complete()
        {
            foreach(var objective_ in ques_t.Get_Objective())
            {
                if (!completed_Objective.Contains(objective_.reference))
                {
                    return false;
                }
            }
            return true;
        }



        public int Get_completed_Count()
        {
            return completed_Objective.Count/2;
        }


        public bool Is_objective_completed(string objective_)
        {
            return completed_Objective.Contains(objective_);
        }










        internal void Complete_objective(string objective_complet)
        {
            if (ques_t.Has_objective(objective_complet))
            {
                completed_Objective.Add(objective_complet);
            }
        }




        public object CaptureState()
        {
            Quest_Status_Record s_tate = new Quest_Status_Record();
            s_tate.quest_Name_ = ques_t.name;
            s_tate.completed_Objective_s = completed_Objective; ;
            return s_tate;

        }
    }
}


