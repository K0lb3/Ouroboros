namespace SRPG
{
    using System;
    using UnityEngine;

    public class QuestMissionItem : MonoBehaviour
    {
        public GameParameter Star;
        public GameParameter FrameParam;
        public GameParameter IconParam;
        public GameParameter NameParam;
        public GameParameter AmountParam;
        public GameParameter ObjectigveParam;
        public GameParameter ProgressBadgeParam;
        public GameParameter ProgressCurrentParam;
        public GameParameter ProgressTargetParam;

        public QuestMissionItem()
        {
            base..ctor();
            return;
        }

        private void InternalSetGameParameterIndex(GameParameter target, int index)
        {
            if ((target == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            target.Index = index;
            return;
        }

        public void SetGameParameterIndex(int index)
        {
            this.InternalSetGameParameterIndex(this.Star, index);
            this.InternalSetGameParameterIndex(this.FrameParam, index);
            this.InternalSetGameParameterIndex(this.IconParam, index);
            this.InternalSetGameParameterIndex(this.NameParam, index);
            this.InternalSetGameParameterIndex(this.AmountParam, index);
            this.InternalSetGameParameterIndex(this.ObjectigveParam, index);
            this.InternalSetGameParameterIndex(this.ProgressBadgeParam, index);
            this.InternalSetGameParameterIndex(this.ProgressCurrentParam, index);
            this.InternalSetGameParameterIndex(this.ProgressTargetParam, index);
            return;
        }
    }
}

