namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(1, "Refresh", 0, 1), Pin(10, "退店", 1, 10)]
    public class InnWindow : MonoBehaviour, IFlowInterface
    {
        [Description("フレンド申請通知バッジ")]
        public GameObject NotificationBadge;
        [Description("フレンドプレゼントボタン")]
        public GameObject FriendPresentButton;

        public InnWindow()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            this.Refresh();
            return;
        }

        private void Awake()
        {
        }

        private void OnDestroy()
        {
            GameManager local1;
            if ((MonoSingleton<GameManager>.GetInstanceDirect() != null) == null)
            {
                goto Label_0036;
            }
            local1 = MonoSingleton<GameManager>.Instance;
            local1.OnSceneChange = (GameManager.SceneChangeEvent) Delegate.Remove(local1.OnSceneChange, new GameManager.SceneChangeEvent(this.OnGoOutInn));
        Label_0036:
            return;
        }

        private bool OnGoOutInn()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
            return 1;
        }

        public void Refresh()
        {
            int num;
            if ((this.NotificationBadge != null) == null)
            {
                goto Label_003B;
            }
            num = MonoSingleton<GameManager>.Instance.Player.FollowerNum;
            this.NotificationBadge.SetActive(num > 0);
            GameParameter.UpdateAll(this.NotificationBadge);
        Label_003B:
            if ((this.FriendPresentButton != null) == null)
            {
                goto Label_007D;
            }
            if (MonoSingleton<GameManager>.Instance.MasterParam.IsFriendPresentItemParamValid() == null)
            {
                goto Label_0071;
            }
            this.FriendPresentButton.SetActive(1);
            goto Label_007D;
        Label_0071:
            this.FriendPresentButton.SetActive(0);
        Label_007D:
            return;
        }

        private void Start()
        {
            GameManager local1;
            local1 = MonoSingleton<GameManager>.Instance;
            local1.OnSceneChange = (GameManager.SceneChangeEvent) Delegate.Combine(local1.OnSceneChange, new GameManager.SceneChangeEvent(this.OnGoOutInn));
            return;
        }
    }
}

