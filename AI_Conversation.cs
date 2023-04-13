using RPG.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RPG.Dialogue
{

    public class AI_Conversation : MonoBehaviour, IRaycastable
    {

        [SerializeField] private List<Dialogue_menu> dialogues_AI = new List<Dialogue_menu>();
        [SerializeField] string AI_name;

        private int current_Dialogue_AI;



        public CursorType GetCursorType()
        {
            return CursorType.Dialogue;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (dialogues_AI.Count==0)
            {
                return false;
            }


            if (Input.GetMouseButtonDown(0))
            {
                callingController.GetComponent<Player_Conversation>().Start_Dialogue(this, dialogues_AI[current_Dialogue_AI]);

                if (current_Dialogue_AI < dialogues_AI.Count - 1)
                {
                    current_Dialogue_AI++;
                }
                else
                {
                    current_Dialogue_AI = 0;
                }
            }
            return true;
        }





        public string Get_Name()
        {
            return AI_name;
        }






    }

}
