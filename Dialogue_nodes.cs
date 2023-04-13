using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static RPG.Core.Predicate;

namespace RPG.Dialogue
{

    public class Dialogue_nodes : ScriptableObject
    {
        [SerializeField] bool isPlayer_speaking = false;
        [SerializeField] string text;
        [SerializeField] string on_Enter_action;
        [SerializeField] string on_Exit_action;
        [SerializeField] List<string> children = new List<string>();
        [SerializeField] Rect rect = new(100, 100, 200, 100);
        [SerializeField] Condition condition;
        

        public Rect get_Rect()
        {
            return rect;
        }



        public string get_Text()
        {
            return text;
        }


        public List<string> get_Children()
        {
            return children;
        }




        public bool Is_Player_speaking()
        {
            return isPlayer_speaking;
        }






        public string get_on_Enter_Action()
        {
            return on_Enter_action;
        }

        public string get_on_Exit_Action()
        {
            return on_Exit_action;
        }



        public bool Check_Condition(IEnumerable<IPredicateEvaluator> evaluators_node)
        {
            return condition.Check(evaluators_node);
        }




#if UNITY_EDITOR
        public void set_Pos(Vector2 new_Pos)
        {
            Undo.RecordObject(this, "Move Dialogue Node");

            rect.position = new_Pos;

            EditorUtility.SetDirty(this);
        }

        public void set_Text(string new_Text)
        {
            if (new_Text != text)
            {
                Undo.RecordObject(this, "Update Dialogue Text");

                text = new_Text;

                EditorUtility.SetDirty(this);
            }
        }

        public void add_Child(string child_ID)
        {
            Undo.RecordObject(this, "Add link");

            children.Add(child_ID);

            EditorUtility.SetDirty(this);
        }


        public void remove_Child(string child_ID)
        {
            Undo.RecordObject(this, "Remove link");

            children.Remove(child_ID);

            EditorUtility.SetDirty(this);
        }

        public void Setplayer_Speaking(bool new_Is_playerSpeaking)
        {
            Undo.RecordObject(this, "Change speaker");

            isPlayer_speaking = new_Is_playerSpeaking;

            EditorUtility.SetDirty(this);
        }

       


#endif

    }
}


