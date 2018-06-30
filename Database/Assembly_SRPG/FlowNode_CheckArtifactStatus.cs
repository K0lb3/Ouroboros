namespace SRPG
{
    using GR;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(1, "判定", 0, 1), NodeType("Tips/CheckArtifactStatus", 0x7fe5), Pin(3, "False", 1, 3), Pin(2, "True", 1, 2)]
    public class FlowNode_CheckArtifactStatus : FlowNode
    {
        private const int PIN_ID_IN = 1;
        private const int PIN_ID_TRUE = 2;
        private const int PIN_ID_FALSE = 3;
        [SerializeField]
        private Flag flag;
        [CompilerGenerated]
        private static Func<ArtifactData, bool> <>f__am$cache1;

        public FlowNode_CheckArtifactStatus()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <OnActivate>m__191(ArtifactData arti)
        {
            return (arti.ArtifactParam.iname == "AF_ARMO_ARMOR_BEGINNER_01");
        }

        private ArtifactData GetArtifactDataFromUniqueID(long uniqueId)
        {
            <GetArtifactDataFromUniqueID>c__AnonStorey266 storey;
            storey = new <GetArtifactDataFromUniqueID>c__AnonStorey266();
            storey.uniqueId = uniqueId;
            return Enumerable.FirstOrDefault<ArtifactData>(MonoSingleton<GameManager>.Instance.Player.Artifacts, new Func<ArtifactData, bool>(storey.<>m__190));
        }

        public override void OnActivate(int pinID)
        {
            int num;
            ArtifactData data;
            ArtifactData data2;
            Flag flag;
            if (pinID != 1)
            {
                goto Label_0105;
            }
            switch (this.flag)
            {
                case 0:
                    goto Label_0025;

                case 1:
                    goto Label_0078;

                case 2:
                    goto Label_00C3;
            }
            goto Label_0105;
        Label_0025:
            if (<>f__am$cache1 != null)
            {
                goto Label_004C;
            }
            <>f__am$cache1 = new Func<ArtifactData, bool>(FlowNode_CheckArtifactStatus.<OnActivate>m__191);
        Label_004C:
            if (Enumerable.Count<ArtifactData>(MonoSingleton<GameManager>.Instance.Player.Artifacts, <>f__am$cache1) >= 3)
            {
                goto Label_006B;
            }
            base.ActivateOutputLinks(2);
            goto Label_0073;
        Label_006B:
            base.ActivateOutputLinks(3);
        Label_0073:
            goto Label_0105;
        Label_0078:
            data = this.GetArtifactDataFromUniqueID(GlobalVars.SelectedArtifactUniqueID);
            if (data == null)
            {
                goto Label_00B6;
            }
            if ((data.ArtifactParam.iname == "AF_ARMO_ARMOR_BEGINNER_01") == null)
            {
                goto Label_00B6;
            }
            base.ActivateOutputLinks(2);
            goto Label_00BE;
        Label_00B6:
            base.ActivateOutputLinks(3);
        Label_00BE:
            goto Label_0105;
        Label_00C3:
            data2 = this.GetArtifactDataFromUniqueID(GlobalVars.SelectedArtifactUniqueID);
            if (data2 == null)
            {
                goto Label_00F8;
            }
            if (data2.Rarity != 3)
            {
                goto Label_00F8;
            }
            base.ActivateOutputLinks(2);
            goto Label_0100;
        Label_00F8:
            base.ActivateOutputLinks(3);
        Label_0100:;
        Label_0105:
            return;
        }

        [CompilerGenerated]
        private sealed class <GetArtifactDataFromUniqueID>c__AnonStorey266
        {
            internal long uniqueId;

            public <GetArtifactDataFromUniqueID>c__AnonStorey266()
            {
                base..ctor();
                return;
            }

            internal bool <>m__190(ArtifactData arti)
            {
                return (arti.UniqueID == this.uniqueId);
            }
        }

        public enum Flag
        {
            ArmorCountLessThan3,
            SelectArmor,
            ArmorRarityReachedBy4
        }
    }
}

