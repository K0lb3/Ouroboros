namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("シーン切り替え", "別シーンに切り替えます", 0x555555, 0x444488)]
    public class EventAction_Scene : EventAction_SceneBase
    {
        [StringIsSceneID]
        public string SceneID;
        private SceneRequest mAsyncOp;
        private GameObject mSceneRoot;
        [HideInInspector]
        public bool FadeIn;
        [HideInInspector]
        public bool WaitFadeIn;
        [HideInInspector]
        public float FadeInTime;

        public EventAction_Scene()
        {
            this.FadeIn = 1;
            this.WaitFadeIn = 1;
            this.FadeInTime = 3f;
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            base.Sequence.Scene = null;
            LightmapSettings.set_lightmapsMode(0);
            if (string.IsNullOrEmpty(this.SceneID) != null)
            {
                goto Label_0065;
            }
            if (this.FadeIn == null)
            {
                goto Label_0037;
            }
            GameUtility.FadeOut(0f);
        Label_0037:
            SceneAwakeObserver.AddListener(new SceneAwakeObserver.SceneEvent(this.OnSceneLoad));
            CriticalSection.Enter(4);
            this.mAsyncOp = AssetManager.LoadSceneAsync(this.SceneID, 1);
            goto Label_008D;
        Label_0065:
            if ((base.Sequence.Scene != null) == null)
            {
                goto Label_008D;
            }
            base.Sequence.Scene = null;
            base.ActivateNext();
        Label_008D:
            return;
        }

        private void OnSceneLoad(GameObject sceneRoot)
        {
            Camera[] cameraArray;
            int num;
            this.mSceneRoot = sceneRoot;
            base.Sequence.Scene = sceneRoot;
            cameraArray = this.mSceneRoot.GetComponentsInChildren<Camera>(1);
            num = ((int) cameraArray.Length) - 1;
            goto Label_003D;
        Label_002B:
            cameraArray[num].get_gameObject().SetActive(0);
            num -= 1;
        Label_003D:
            if (num >= 0)
            {
                goto Label_002B;
            }
            SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnSceneLoad));
            return;
        }

        public override void Update()
        {
            if (this.mAsyncOp == null)
            {
                goto Label_005B;
            }
            if (this.mAsyncOp.canBeActivated == null)
            {
                goto Label_0027;
            }
            this.mAsyncOp.ActivateScene();
        Label_0027:
            if (this.mAsyncOp.isDone == null)
            {
                goto Label_005A;
            }
            if (this.FadeIn == null)
            {
                goto Label_004D;
            }
            GameUtility.FadeIn(this.FadeInTime);
        Label_004D:
            CriticalSection.Leave(4);
            this.mAsyncOp = null;
        Label_005A:
            return;
        Label_005B:
            if (this.WaitFadeIn == null)
            {
                goto Label_007C;
            }
            if (GameUtility.IsScreenFading == null)
            {
                goto Label_007C;
            }
            if (this.FadeIn == null)
            {
                goto Label_007C;
            }
            return;
        Label_007C:
            base.ActivateNext();
            return;
        }
    }
}

