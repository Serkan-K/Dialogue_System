using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Quests
{
    //[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]

    public class Quest_old : ScriptableObject
    {

        [SerializeField] string[] tasks;


        public IEnumerable<string> GetTask()
        {
            yield return "Task 1";
            Debug.Log("Do some work");
            yield return "Task 2";

        }

    }
}

