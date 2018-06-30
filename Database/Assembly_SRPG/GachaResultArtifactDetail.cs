namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(2, "Refreshed", 1, 2), Pin(1, "Refresh", 0, 1), Pin(100, "Close", 1, 100)]
    public class GachaResultArtifactDetail : MonoBehaviour, IFlowInterface
    {
        private readonly int OUT_CLOSE_DETAIL;
        public GameObject ArtifactInfo;
        public GameObject Bg;
        private ArtifactData mCurrentArtifact;
        [SerializeField]
        private Button BackBtn;

        public GachaResultArtifactDetail()
        {
            this.OUT_CLOSE_DETAIL = 100;
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            this.Refresh();
            return;
        }

        private ArtifactData CreateArtifactData(ArtifactParam param, int rarity)
        {
            ArtifactData data;
            Json_Artifact artifact;
            data = new ArtifactData();
            artifact = new Json_Artifact();
            artifact.iid = 1L;
            artifact.exp = 0;
            artifact.iname = param.iname;
            artifact.fav = 0;
            artifact.rare = Math.Min(Math.Max(param.rareini, rarity), param.raremax);
            data.Deserialize(artifact);
            return data;
        }

        private void OnCloseDetail()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, this.OUT_CLOSE_DETAIL);
            return;
        }

        private void OnEnable()
        {
            Animator animator;
            CanvasGroup group;
            animator = base.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_001F;
            }
            animator.SetBool("close", 0);
        Label_001F:
            if ((this.Bg != null) == null)
            {
                goto Label_0056;
            }
            group = this.Bg.GetComponent<CanvasGroup>();
            if ((group != null) == null)
            {
                goto Label_0056;
            }
            group.set_interactable(1);
            group.set_blocksRaycasts(1);
        Label_0056:
            return;
        }

        private void Refresh()
        {
            ArtifactParam param;
            if ((this.ArtifactInfo == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            param = null;
            if (string.IsNullOrEmpty(GlobalVars.SelectedArtifactID) != null)
            {
                goto Label_006C;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(GlobalVars.SelectedArtifactID);
            if (param != null)
            {
                goto Label_003F;
            }
            return;
        Label_003F:
            this.mCurrentArtifact = new ArtifactData();
            this.mCurrentArtifact = this.CreateArtifactData(param, param.rareini);
            GlobalVars.SelectedArtifactID = string.Empty;
        Label_006C:
            DataSource.Bind<ArtifactData>(this.ArtifactInfo, this.mCurrentArtifact);
            GameParameter.UpdateAll(this.ArtifactInfo);
            FlowNode_GameObject.ActivateOutputLinks(this, 2);
            return;
        }

        public void Setup(ArtifactData _artifact)
        {
            this.mCurrentArtifact = _artifact;
            return;
        }

        public void Setup(int _index)
        {
            ArtifactParam param;
            int num;
            ArtifactData data;
            param = GachaResultData.drops[_index].artifact;
            num = GachaResultData.drops[_index].Rare;
            if (param == null)
            {
                goto Label_0030;
            }
            data = this.CreateArtifactData(param, num);
            this.Setup(data);
        Label_0030:
            return;
        }

        private void Start()
        {
            if ((this.BackBtn != null) == null)
            {
                goto Label_002D;
            }
            this.BackBtn.get_onClick().AddListener(new UnityAction(this, this.OnCloseDetail));
        Label_002D:
            return;
        }
    }
}

