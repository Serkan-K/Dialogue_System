using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Dialogue
{
    public class Trigger_Dialogue : MonoBehaviour
    {
        [SerializeField] string action_;
        [SerializeField] UnityEvent on_Trigger;

        public void Trigger_(string action_Trigger)
        {
            if (action_Trigger == action_)
            {
                on_Trigger.Invoke();
            }            
        }
    }

}