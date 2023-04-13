using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Group_Attack : MonoBehaviour
    {

        [SerializeField] Fighter[] fighters;
        [SerializeField] bool active_on_Start;



        private void Start()
        {
            Active_group_Attack(active_on_Start);
        }



        public void Active_group_Attack(bool should_Active)
        {
            foreach(Fighter fighter_ in fighters)
            {
                CombatTarget target_ = fighter_.GetComponent<CombatTarget>();

                if (target_ != null)
                {
                    target_.enabled = should_Active;
                }


                fighter_.enabled = should_Active;
            }

        }


    }
}


