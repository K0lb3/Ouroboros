namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(100, "Close", 1, 100), Pin(2, "Refreshed", 1, 2), Pin(1, "Refresh", 0, 1)]
    public class GachaResultPieceDetail : MonoBehaviour, IFlowInterface
    {
        private readonly int OUT_CLOSE_DETAIL;
        public GameObject PieceInfo;
        public GameObject Bg;
        private ItemData mCurrentPiece;
        [SerializeField]
        private Button BackBtn;

        public GachaResultPieceDetail()
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
            this.Refresh();
            return;
        }

        private void Refresh()
        {
            if ((this.PieceInfo == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            DataSource.Bind<ItemData>(this.PieceInfo, this.mCurrentPiece);
            GameParameter.UpdateAll(this.PieceInfo);
            FlowNode_GameObject.ActivateOutputLinks(this, 2);
            return;
        }

        public void Setup(ItemData _data)
        {
            this.mCurrentPiece = _data;
            return;
        }

        public void Setup(int _index)
        {
            ItemParam param;
            int num;
            ItemData data;
            param = GachaResultData.drops[_index].item;
            num = GachaResultData.drops[_index].num;
            if (param == null)
            {
                goto Label_0038;
            }
            data = new ItemData();
            data.Setup(0L, param, num);
            this.Setup(data);
        Label_0038:
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

