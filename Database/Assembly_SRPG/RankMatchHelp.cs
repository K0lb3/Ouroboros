namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class RankMatchHelp : MonoBehaviour, IWebHelp
    {
        public RankMatchHelp()
        {
            base..ctor();
            return;
        }

        public bool GetHelpURL(out string url, out string title)
        {
            GameManager manager;
            VersusRankParam param;
            *(title) = null;
            *(url) = null;
            manager = MonoSingleton<GameManager>.Instance;
            if ((manager == null) == null)
            {
                goto Label_001A;
            }
            return 0;
        Label_001A:
            param = manager.GetVersusRankParam(manager.RankMatchScheduleId);
            if (param == null)
            {
                goto Label_004F;
            }
            if (string.IsNullOrEmpty(param.HelpURL) != null)
            {
                goto Label_004F;
            }
            *(title) = param.Name;
            *(url) = param.HelpURL;
            return 1;
        Label_004F:
            return 0;
        }
    }
}

