namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class ArtifactWindowEventHandler : MonoBehaviour, IGameParameter
    {
        public ArtifactList mArtifactList;
        public Button mBackButton;
        public Button mForwardButton;

        public ArtifactWindowEventHandler()
        {
            base..ctor();
            return;
        }

        public ArtifactData GetArtifactData()
        {
            return DataSource.FindDataOfClass<ArtifactData>(base.get_gameObject(), null);
        }

        public void OnBackButton(Button button)
        {
            ArtifactData data;
            data = this.GetArtifactData();
            if (data != null)
            {
                goto Label_000E;
            }
            return;
        Label_000E:
            if ((this.mArtifactList == null) == null)
            {
                goto Label_0020;
            }
            return;
        Label_0020:
            this.mArtifactList.SelectBack(data);
            return;
        }

        public void OnForwardButton(Button button)
        {
            ArtifactData data;
            data = this.GetArtifactData();
            if (data != null)
            {
                goto Label_000E;
            }
            return;
        Label_000E:
            if ((this.mArtifactList == null) == null)
            {
                goto Label_0020;
            }
            return;
        Label_0020:
            this.mArtifactList.SelectFoward(data);
            return;
        }

        private void UpdateBackButtonIntaractable()
        {
            ArtifactData data;
            data = this.GetArtifactData();
            if (data != null)
            {
                goto Label_000E;
            }
            return;
        Label_000E:
            if ((this.mArtifactList != null) == null)
            {
                goto Label_004A;
            }
            if ((this.mBackButton != null) == null)
            {
                goto Label_004A;
            }
            this.mBackButton.set_interactable(this.mArtifactList.CheckStartOfIndex(data) == 0);
        Label_004A:
            return;
        }

        private void UpdateForwardButtonIntaractable()
        {
            ArtifactData data;
            data = this.GetArtifactData();
            if (data != null)
            {
                goto Label_000E;
            }
            return;
        Label_000E:
            if ((this.mArtifactList != null) == null)
            {
                goto Label_004A;
            }
            if ((this.mForwardButton != null) == null)
            {
                goto Label_004A;
            }
            this.mForwardButton.set_interactable(this.mArtifactList.CheckEndOfIndex(data) == 0);
        Label_004A:
            return;
        }

        public void UpdateValue()
        {
            this.UpdateBackButtonIntaractable();
            this.UpdateForwardButtonIntaractable();
            return;
        }
    }
}

