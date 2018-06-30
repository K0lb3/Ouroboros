namespace SRPG
{
    using System;
    using UnityEngine;

    [Pin(100, "Finish", 1, 3), Pin(1, "Unknown", 0, 1), Pin(2, "Reset", 0, 2)]
    public class VersusUnknownEnemy : MonoBehaviour, IFlowInterface
    {
        public RawImage_Transparent EnemyImage;
        public GameObject UnknownObj;

        public VersusUnknownEnemy()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0012;
            }
            this.RefreshUnknow();
            goto Label_001F;
        Label_0012:
            if (pinID != 2)
            {
                goto Label_001F;
            }
            this.RefreshReset();
        Label_001F:
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        }

        private void RefreshReset()
        {
            if ((this.EnemyImage != null) == null)
            {
                goto Label_0035;
            }
            this.EnemyImage.set_color(new Color(1f, 1f, 1f, 1f));
        Label_0035:
            if ((this.UnknownObj != null) == null)
            {
                goto Label_0052;
            }
            this.UnknownObj.SetActive(0);
        Label_0052:
            return;
        }

        private void RefreshUnknow()
        {
            if ((this.EnemyImage != null) == null)
            {
                goto Label_0035;
            }
            this.EnemyImage.set_color(new Color(0f, 0f, 0f, 1f));
        Label_0035:
            if ((this.UnknownObj != null) == null)
            {
                goto Label_0052;
            }
            this.UnknownObj.SetActive(1);
        Label_0052:
            return;
        }
    }
}

