namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class RigSetup : ScriptableObject
    {
        public string RootBoneName;
        public SkeletonInfo[] Skeletons;
        public string LeftHand;
        public List<string> LeftHandChangeLists;
        public string RightHand;
        public List<string> RightHandChangeLists;
        public List<string> OptionAttachLists;
        public float EquipmentScale;
        [Description("この骨格の基準となる身長です")]
        public float Height;
        public string Head;

        public RigSetup()
        {
            this.RootBoneName = string.Empty;
            this.Skeletons = new SkeletonInfo[0];
            this.LeftHandChangeLists = new List<string>();
            this.RightHandChangeLists = new List<string>();
            this.OptionAttachLists = new List<string>();
            this.EquipmentScale = 1f;
            this.Height = 1f;
            this.Head = "Bip001 Head";
            base..ctor();
            return;
        }

        [Serializable]
        public class BoneInfo
        {
            public string name;
            public Vector3 offset;
            public Vector3 scale;

            public BoneInfo()
            {
                this.name = string.Empty;
                this.offset = new Vector3(0f, 0f, 0f);
                this.scale = new Vector3(1f, 1f, 1f);
                base..ctor();
                return;
            }
        }

        [Serializable]
        public class SkeletonInfo
        {
            public string name;
            public RigSetup.BoneInfo[] bones;

            public SkeletonInfo()
            {
                this.name = string.Empty;
                this.bones = new RigSetup.BoneInfo[0];
                base..ctor();
                return;
            }
        }
    }
}

