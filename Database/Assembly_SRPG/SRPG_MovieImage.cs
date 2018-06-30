namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(9, "Started", 1, 0), Pin(10, "Finished", 1, 1), RequireComponent(typeof(CriManaMovieControllerForUI))]
    public class SRPG_MovieImage : RawImage, IFlowInterface
    {
        public const int PINID_STARTED = 9;
        public const int PINID_FINISHED = 10;
        private CriManaMovieControllerForUI mMovieController;
        private bool mPlaying;

        public SRPG_MovieImage()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
        }

        protected override void Awake()
        {
            base.Awake();
            GameUtility.SetNeverSleep();
            if (Application.get_isPlaying() == null)
            {
                goto Label_0054;
            }
            MyCriManager.Setup(0);
            this.mMovieController = base.GetComponent<CriManaMovieControllerForUI>();
            if ((this.mMovieController != null) == null)
            {
                goto Label_0054;
            }
            this.mMovieController.set_moviePath(MyCriManager.GetLoadFileName(this.mMovieController.get_moviePath(), 0));
        Label_0054:
            return;
        }

        protected override void OnDestroy()
        {
            GameUtility.SetDefaultSleepSetting();
            base.OnDestroy();
            return;
        }

        private void Update()
        {
            if ((this.mMovieController != null) == null)
            {
                goto Label_00A5;
            }
            if (this.mMovieController.get_player().get_status() < 5)
            {
                goto Label_00A5;
            }
            this.set_material(this.mMovieController.get_material());
            this.UpdateMaterial();
            if (this.mMovieController.get_player().get_status() != 5)
            {
                goto Label_0074;
            }
            if (this.mPlaying != null)
            {
                goto Label_00A5;
            }
            this.mPlaying = 1;
            FlowNode_GameObject.ActivateOutputLinks(this, 9);
            return;
            goto Label_00A5;
        Label_0074:
            if (this.mMovieController.get_player().get_status() != 6)
            {
                goto Label_00A5;
            }
            if (this.mPlaying == null)
            {
                goto Label_00A5;
            }
            this.mPlaying = 0;
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
            return;
        Label_00A5:
            return;
        }
    }
}

