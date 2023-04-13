using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RPG.Dialogue
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 0)]

    public class Dialogue_menu : ScriptableObject,  ISerializationCallbackReceiver
    {
        [SerializeField] List<Dialogue_nodes> nodes = new List<Dialogue_nodes>();
        [SerializeField] Vector2 offset_child = new(300, 50);


        Dictionary<string, Dialogue_nodes> node_LookUp = new Dictionary<string, Dialogue_nodes>();



        private void OnValidate()
        {
            node_LookUp.Clear();

            foreach(Dialogue_nodes node in GetAllNodes())
            {
                node_LookUp[node.name] = node;
            }
        }




        public IEnumerable<Dialogue_nodes> GetAllNodes()
        {
            return nodes;
        }


        public Dialogue_nodes GetRootNode()
        {
            return nodes[0];
        }

        internal IEnumerable<Dialogue_nodes> GetallChildren(Dialogue_nodes parent_Node)
        {
            foreach (string child_ID in parent_Node.get_Children())
            {
                if (node_LookUp.ContainsKey(child_ID))
                {
                    yield return node_LookUp[child_ID];
                }
            }
        }






        public IEnumerable<Dialogue_nodes> Get_Player_Children(Dialogue_nodes current_node_)
        {
            foreach(Dialogue_nodes n_ode in GetallChildren(current_node_))
            {
                if (n_ode.Is_Player_speaking())
                {
                    yield return n_ode;
                }
            }
        }



        public IEnumerable<Dialogue_nodes> Get_AI_Children(Dialogue_nodes current_node_)
        {
            foreach (Dialogue_nodes n_ode in GetallChildren(current_node_))
            {
                if (!n_ode.Is_Player_speaking())
                {
                    yield return n_ode;
                }
            }
        }




        //------------------Create, delete, make, add Node---------clean child--------------------------------

#if UNITY_EDITOR
        public void Create_Node(Dialogue_nodes parent_)
        {
            Dialogue_nodes _newNode = Make_Node(parent_);

            Undo.RegisterCreatedObjectUndo(_newNode, "Created new Node");
            Undo.RecordObject(this, "Added new Node");

            Add_Node(_newNode);
        }

                      

        public void Delete_Node(Dialogue_nodes node_toDelete)
        {

            Undo.RecordObject(this, "Deleted new Node");

            nodes.Remove(node_toDelete);

            OnValidate();
            Clean_child(node_toDelete);

            Undo.DestroyObjectImmediate(node_toDelete);
        }


        private Dialogue_nodes Make_Node(Dialogue_nodes parent_)
        {
            Dialogue_nodes _newNode = CreateInstance<Dialogue_nodes>();
            _newNode.name = Guid.NewGuid().ToString();

            if (parent_ != null)
            {
                parent_.add_Child(_newNode.name);
                _newNode.Setplayer_Speaking(!parent_.Is_Player_speaking());
                _newNode.set_Pos(parent_.get_Rect().position + offset_child);
            }

            return _newNode;
        }


        private void Add_Node(Dialogue_nodes _newNode)
        {
            nodes.Add(_newNode);
            OnValidate();
        }



        private void Clean_child(Dialogue_nodes node_toDelete)
        {
            foreach(Dialogue_nodes _node_ in GetAllNodes())
            {
                _node_.remove_Child(node_toDelete.name);
            }
        }


#endif




//-------------------------ReCreate node----------------------------------------

        public void OnBeforeSerialize()
        {

#if UNITY_EDITOR
            if (nodes.Count == 0)
            {
                Dialogue_nodes _newNode = Make_Node(null);
                Add_Node(_newNode);
            }


            if (AssetDatabase.GetAssetPath(this) != "")
            {
                foreach(Dialogue_nodes n_ode in GetAllNodes())
                {
                    if (AssetDatabase.GetAssetPath(n_ode) == "")
                    {
                        AssetDatabase.AddObjectToAsset(n_ode, this);
                    }
                }
            }
#endif
        }
        

        public void OnAfterDeserialize()
        {
        }
    }
}

