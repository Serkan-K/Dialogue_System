using GameDevTV.Core.UI.Tooltips;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    public class Quest_tooltip : TooltipSpawner
    {
        public override bool CanCreateTooltip()
        {
            return true;
        }

        public override void UpdateTooltip(GameObject tooltip)
        {
           Quest_Status stat_us = GetComponent<Quest_Item_UI>().Get_Quest_Status();
            tooltip.GetComponent<Quest_tooltip_UI>().Setup(stat_us);
        }
    }

}

