namespace SRPG
{
    using System;
    using UnityEngine;

    public class NetworkIndicator : MonoBehaviour
    {
        public GameObject Body;
        public float FadeTime;
        public float KeepUp;
        private CanvasGroup mCanvasGroup;
        private float mRemainingTime;

        public NetworkIndicator()
        {
            this.FadeTime = 1f;
            this.KeepUp = 0.5f;
            base..ctor();
            return;
        }

        private void Start()
        {
            if ((this.Body != null) == null)
            {
                goto Label_002E;
            }
            this.mCanvasGroup = this.Body.GetComponent<CanvasGroup>();
            this.Body.SetActive(0);
        Label_002E:
            return;
        }

        private void Update()
        {
            if (Network.IsIndicator != null)
            {
                goto Label_0017;
            }
            this.Body.SetActive(0);
            return;
        Label_0017:
            if (Network.IsBusy != null)
            {
                goto Label_003F;
            }
            if (AssetDownloader.isDone == null)
            {
                goto Label_003F;
            }
            if (FlowNode_NetworkIndicator.NeedDisplay() != null)
            {
                goto Label_003F;
            }
            if (EventAction.IsLoading == null)
            {
                goto Label_0052;
            }
        Label_003F:
            this.mRemainingTime = this.KeepUp + this.FadeTime;
        Label_0052:
            if (this.mRemainingTime <= 0f)
            {
                goto Label_00DB;
            }
            this.mRemainingTime -= Time.get_unscaledDeltaTime();
            if ((this.mCanvasGroup != null) == null)
            {
                goto Label_00B2;
            }
            if (this.FadeTime <= 0f)
            {
                goto Label_00B2;
            }
            this.mCanvasGroup.set_alpha(Mathf.Clamp01(this.mRemainingTime / this.FadeTime));
        Label_00B2:
            if ((this.Body != null) == null)
            {
                goto Label_00DB;
            }
            this.Body.SetActive(this.mRemainingTime > 0f);
        Label_00DB:
            return;
        }
    }
}

