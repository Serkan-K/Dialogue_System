using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Dialogue;
using UnityEngine.UI;
using TMPro;

namespace RPG.UI
{

    public class Dialogue_UI : MonoBehaviour
    {
        Player_Conversation Player_Convers;
        [SerializeField] TextMeshProUGUI AI_text;
        [SerializeField] TextMeshProUGUI Speaker_Name;
        [SerializeField] Button next_Button;
        [SerializeField] Button quit_Button;
        [SerializeField] Transform choice_Root;
        [SerializeField] GameObject AI_responce;
        [SerializeField] GameObject choice_prefab;

        void Start()
        {
            Player_Convers = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Conversation>();
            Player_Convers.On_Conversation_Updated += Update_UI;
            next_Button.onClick.AddListener(() => Player_Convers.Next_dialogue());
            quit_Button.onClick.AddListener(() => Player_Convers.Quit());

            Update_UI();
        }

        
        void Update_UI()
        {

            gameObject.SetActive(Player_Convers.is_Dialgoue_active());

            if (!Player_Convers.is_Dialgoue_active())
            {
                return;
            }


            Speaker_Name.text = Player_Convers.get_current_Speaker_Name();


            AI_responce.SetActive(!Player_Convers.isChoosing());
            choice_Root.gameObject.SetActive(Player_Convers.isChoosing());


            if (Player_Convers.isChoosing())
            {
                Choose_List();
            }

            else
            {
                AI_text.text = Player_Convers.Get_Text();
                next_Button.gameObject.SetActive(Player_Convers.has_Next());
            }                      
        }








        private void Choose_List()
        {
            
            foreach(Transform item_ in choice_Root)
            {
                Destroy(item_.gameObject);
            }


            foreach (Dialogue_nodes choice_ in Player_Convers.get_Choices())
            {
                GameObject choice_instance = Instantiate(choice_prefab, choice_Root);
                var textComp = choice_instance.GetComponentInChildren<TextMeshProUGUI>();
                textComp.text = choice_.get_Text();

                Button button_ = choice_instance.GetComponentInChildren<Button>();
                button_.onClick.AddListener(()=>
                {
                    Player_Convers.Select_Choice(choice_);
                });
            }
        }
    }

}
