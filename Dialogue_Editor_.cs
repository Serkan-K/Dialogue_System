using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace RPG.Dialogue.Editor
{
    public class Dialogue_Editor_ : EditorWindow
    {

        Dialogue_menu selected_Dialogue = null;

        [NonSerialized] GUIStyle nodeStyle;
        [NonSerialized] GUIStyle player_nodeStyle;
        [NonSerialized] Dialogue_nodes dragging_Node = null;
        [NonSerialized] Dialogue_nodes creating_Node = null;
        [NonSerialized] Dialogue_nodes deleting_Node = null;
        [NonSerialized] Dialogue_nodes linking_Node = null;
        [NonSerialized] bool dragging_Canvas;



        Vector2 dragging_offset;
        Vector2 scroll_Pos;
        Vector2 window_size = new(500, 500);
        Vector2 dragging_Canvas_offset;




        //--------------------Menu-------------------------------------------------

        [MenuItem("Window/Dialogue Editor")]

        public static void Show_EditorWindow()
        {
            GetWindow(typeof(Dialogue_Editor_), false, "Dialogue Editor");
        }


        [OnOpenAssetAttribute(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            Dialogue_menu _dialogue_ = EditorUtility.InstanceIDToObject(instanceID) as Dialogue_menu;

            if (_dialogue_ != null)
            {
                Show_EditorWindow();
                return true;
            }
            return false;
        }


        //----------------Node style----------------------------------------------------------

        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChange;

            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("node6") as Texture2D;
            nodeStyle.normal.textColor = Color.white;
            nodeStyle.padding = new RectOffset(20, 20, 20, 20);
            nodeStyle.border = new RectOffset(12, 12, 12, 12);

            player_nodeStyle = new GUIStyle();
            player_nodeStyle.normal.background = EditorGUIUtility.Load("node2") as Texture2D;
            player_nodeStyle.normal.textColor = Color.white;
            player_nodeStyle.padding = new RectOffset(20, 20, 20, 20);
            player_nodeStyle.border = new RectOffset(12, 12, 12, 12);

        }




        //---------------Selected Node info----------------------------------------------------------


        private void OnSelectionChange()
        {
            Dialogue_menu newdialogue = Selection.activeObject as Dialogue_menu;
            if (newdialogue != null)
            {
                selected_Dialogue = newdialogue;
                Repaint();
                Reset_Nodes();
            }
        }



        void Reset_Nodes()
        {
            dragging_Node = null;
            creating_Node = null;
            deleting_Node = null;
            linking_Node = null;
        }








        private void OnGUI()
        {
            if (selected_Dialogue == null)
            {
                EditorGUILayout.LabelField("no dialogue selected");
            }
            else
            {
                ProcessDragEvent();

                scroll_Pos = EditorGUILayout.BeginScrollView(scroll_Pos);

                Rect canvas = GUILayoutUtility.GetRect(window_size.x, window_size.y);
                Texture2D background_Tex = Resources.Load("background") as Texture2D;
                GUI.DrawTexture(canvas, background_Tex);


                foreach (Dialogue_nodes node in selected_Dialogue.GetAllNodes())
                {
                    Draw_Connections(node);
                }
                foreach (Dialogue_nodes node in selected_Dialogue.GetAllNodes())
                {
                    Draw_Node(node);
                }

                EditorGUILayout.EndScrollView();

                if (creating_Node != null)
                {                                      
                    selected_Dialogue.Create_Node(creating_Node);
                    creating_Node = null;
                }

                if (deleting_Node != null)
                {                    
                    selected_Dialogue.Delete_Node(deleting_Node);
                    deleting_Node = null;
                }
            }
        }




//-------------------Drag--------------------------------------------------

        void ProcessDragEvent()
        {


            Vector2 maxSize = new(500, 500);

            foreach (Dialogue_nodes node_s in selected_Dialogue.GetAllNodes())
            {
                maxSize.x = node_s.get_Rect().xMax > maxSize.x ? node_s.get_Rect().xMax : maxSize.x;
                maxSize.y = node_s.get_Rect().yMax > maxSize.y ? node_s.get_Rect().yMax : maxSize.y;
            }

            window_size = maxSize + new Vector2(1000, 1000);




            if (Event.current.type == EventType.MouseDown && dragging_Node == null)
            {
                dragging_Node = GetNode_atPoint(Event.current.mousePosition + scroll_Pos);

                if (dragging_Node != null)
                {
                    dragging_offset = dragging_Node.get_Rect().position - Event.current.mousePosition;
                    Selection.activeObject = dragging_Node;
                }

                else
                {
                    dragging_Canvas = true;
                    dragging_Canvas_offset = Event.current.mousePosition + scroll_Pos;
                    Selection.activeObject = selected_Dialogue;
                }


            }

            else if (Event.current.type == EventType.MouseUp && dragging_Node != null)
            {
                dragging_Node = null;
            }


            else if (Event.current.type == EventType.MouseDrag && dragging_Node != null)
            {


                dragging_Node.set_Pos(Event.current.mousePosition + dragging_offset);

                GUI.changed = true;
            }


            else if (dragging_Node == null && Event.current.type == EventType.MouseDrag)
            {
                scroll_Pos -= Event.current.delta;
                GUI.changed = true;
            }

        }



        private Dialogue_nodes GetNode_atPoint(Vector2 point)

        {

            Dialogue_nodes found_Node = null;

            foreach (Dialogue_nodes node in selected_Dialogue.GetAllNodes())
            {
                if (node.get_Rect().Contains(point))
                {
                    found_Node = node;
                }
            }

            return found_Node;
        }







//-------------------Node text--------------------------------------------------

        private void Draw_Node(Dialogue_nodes node)
        {

            GUIStyle style_player = nodeStyle;

            if (node.Is_Player_speaking())
            {
                style_player = player_nodeStyle;
            }


            GUILayout.BeginArea(node.get_Rect(), style_player);
               
            node.set_Text(EditorGUILayout.TextField(node.get_Text()));





//-------------------Node buttons--------------------------------------------------


            GUILayout.BeginHorizontal();

            if (GUILayout.Button("+ Add"))
            {
                creating_Node = node;
            }


            if (GUILayout.Button("x Delete"))
            {
                deleting_Node = node;
            }

            GUILayout.EndHorizontal();

            Link_Node(node);


            GUILayout.EndArea();
        }





        private void Link_Node(Dialogue_nodes node)
        {
            if (linking_Node == null)
            {
                if (GUILayout.Button("link"))
                {
                    linking_Node = node;
                }
            }

            else if (linking_Node == node)
            {
                if (GUILayout.Button("cancel"))
                {
                    linking_Node = null;
                }
            }

            else if (linking_Node.get_Children().Contains(node.name))
            {
                if (GUILayout.Button("unlink"))
                {                    
                    linking_Node.remove_Child(node.name);
                    linking_Node = null;
                }
            }


            else
            {
                if (GUILayout.Button("link here"))
                {
                    Undo.RecordObject(selected_Dialogue, "Add link");
                    linking_Node.add_Child(node.name);
                    linking_Node = null;
                }
            }
        }


        //-------------------Node connections--------------------------------------------------

        private void Draw_Connections(Dialogue_nodes node)
        {
            foreach (Dialogue_nodes child_node in selected_Dialogue.GetallChildren(node))
            {
                Vector3 start_pos = new Vector2(node.get_Rect().xMax, node.get_Rect().center.y);
                Vector3 end_pos = new Vector2(child_node.get_Rect().xMin, child_node.get_Rect().center.y);
                Vector3 curve = end_pos - start_pos;
                curve.x -= Mathf.Min(50, curve.x * 1f);
                curve.y = 0;

                Handles.DrawBezier(start_pos, end_pos, start_pos + curve, end_pos - curve, Color.white, null, 5f);
            }
        }

    }
}