namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(0, "Setup", 0, 0), Pin(10, "Refresh", 1, 10)]
    public class GachaResultWindow : MonoBehaviour, IFlowInterface
    {
        public GameObject ThumbnailListWindow;
        public Button BackButton;
        private bool Initalized;

        public GachaResultWindow()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <Start>m__326()
        {
            this.OnCloseWindow(this.BackButton);
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_000D;
            }
            this.SetUp();
            return;
        Label_000D:
            return;
        }

        private void OnCloseWindow(Button button)
        {
            if (this.Initalized != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "CLOSED_RESULT");
            return;
        }

        private void SetUp()
        {
            if (GachaResultData.drops == null)
            {
                goto Label_0030;
            }
            FlowNode_Variable.Set("GachaResultCurrentDetail", string.Empty);
            FlowNode_Variable.Set("GachaResultSingle", "0");
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
        Label_0030:
            return;
        }

        private void Start()
        {
            if ((HomeWindow.Current != null) == null)
            {
                goto Label_001B;
            }
            HomeWindow.Current.SetVisible(1);
        Label_001B:
            if ((this.BackButton != null) == null)
            {
                goto Label_0048;
            }
            this.BackButton.get_onClick().AddListener(new UnityAction(this, this.<Start>m__326));
        Label_0048:
            this.Initalized = 1;
            return;
        }
    }
}

