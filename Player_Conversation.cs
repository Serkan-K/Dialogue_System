using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using RPG.Core;


namespace RPG.Dialogue
{
    public class Player_Conversation : MonoBehaviour
    {
        // [SerializeField] Dialogue_menu test_Dialogue;
        [SerializeField] string Player_name;
        Dialogue_menu current_Dialogue;
        Dialogue_nodes current_node_ = null;
        AI_Conversation current_Conversant = null;
        bool is_choosing = false;

        public event Action On_Conversation_Updated;



        //private IEnumerator Start()
        //{
        //    yield return new WaitForSeconds(2);
        //    Start_Dialogue(test_Dialogue);
        //}




        public void Start_Dialogue(AI_Conversation new_Conversant, Dialogue_menu new_Dialogue)
        {
            current_Conversant = new_Conversant;
            current_Dialogue = new_Dialogue;
            current_node_ = current_Dialogue.GetRootNode();
            Trigger_enter_Action();
            On_Conversation_Updated();
        }




        public void Quit()
        {
            current_Dialogue = null;
            Trigger_exit_Action();
            current_node_ = null;
            is_choosing = false;
            current_Conversant = null;
            On_Conversation_Updated();

        }




        public string get_current_Speaker_Name()
        {
            if (is_choosing)
            {
                return Player_name;
            }
            else
            {
                return current_Conversant.Get_Name();
            }
        }




        public bool is_Dialgoue_active()
        {
            return current_Dialogue != null;
        }


        public bool isChoosing()
        {
            return is_choosing;
        }





        public string Get_Text()
        {
            if (current_node_ == null)
            {
                return "";
            }
            return current_node_.get_Text();
        }



        public IEnumerable<Dialogue_nodes> get_Choices()
        {
            return Filter_onCondition(current_Dialogue.Get_Player_Children(current_node_));
        }



        public void Select_Choice(Dialogue_nodes chosen_node)
        {
            current_node_ = chosen_node;
            Trigger_enter_Action();
            is_choosing = false;
            Next_dialogue();
        }







        public void Next_dialogue()
        {

            int num_player_choices = Filter_onCondition(current_Dialogue.Get_Player_Children(current_node_)).Count();

            if (num_player_choices > 0)
            {
                is_choosing = true;
                Trigger_exit_Action();
                On_Conversation_Updated();
                return;
            }


           Dialogue_nodes[] children_dialogue = Filter_onCondition(current_Dialogue.Get_AI_Children(current_node_)).ToArray();



            int random_Dialogue = UnityEngine.Random.Range(0, children_dialogue.Count());
            Trigger_exit_Action();



            if (children_dialogue.Length == 0)
            {
                Quit();
                return;
            }



            current_node_= children_dialogue[random_Dialogue];
            Trigger_enter_Action();
            On_Conversation_Updated();
        }


        public bool has_Next()
        {
            return Filter_onCondition(current_Dialogue.GetallChildren(current_node_)).Count() > 0;
        }



        private IEnumerable<Dialogue_nodes> Filter_onCondition(IEnumerable<Dialogue_nodes> input_Node)
        {
            foreach(var node_player_conversant in input_Node)
            {
                if (node_player_conversant.Check_Condition(GetEvaluators()))
                {
                    yield return node_player_conversant;
                }                
            }
        }

        

        private IEnumerable<IPredicateEvaluator> GetEvaluators()
        {
            return GetComponents<IPredicateEvaluator>();
        }





        private void Trigger_enter_Action()
        {
            if(current_node_!=null)
            {
                Trigger_Action(current_node_.get_on_Enter_Action());
            }
        }

        private void Trigger_exit_Action()
        {
            if (current_node_ != null)
            {
                Trigger_Action(current_node_.get_on_Exit_Action());
            }
        }

        private void Trigger_Action(string action_0)
        {
            if (action_0 == null) return;

            foreach (Trigger_Dialogue trigger_ in current_Conversant.GetComponents<Trigger_Dialogue>())
            {
                trigger_.Trigger_(action_0);
            }

        }

    }
}

