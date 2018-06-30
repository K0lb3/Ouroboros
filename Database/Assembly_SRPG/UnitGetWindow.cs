namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnitGetWindow : MonoBehaviour
    {
        public GameObject PopupUnit;
        [Space(5f)]
        public GameObject PopupAnimation;
        public string PopupRarityVar;
        public string PopupShardVar;
        [Space(5f)]
        public GameObject ShardNum;
        [Space(10f)]
        public GameObject ShardGauge;
        [Space(5f)]
        public GameObject ShardAnimation;
        [Space(5f)]
        public string EndShardState;
        [Space(10f)]
        public GameObject NormalGetEffect;
        public GameObject RareGetEffect;
        public GameObject SRareGetEffect;
        private UnitData mUnitData;
        private Animator mPopupAnimator;
        private GetUnitShard mShardWindow;
        private GameObject mCurrentGetEffect;
        private bool mIsEnd;
        public bool isMaxShard;
        private bool mIsShardEnd;
        private bool mIsEffectEnd;
        private bool mIsClickClose;

        public UnitGetWindow()
        {
            this.PopupRarityVar = string.Empty;
            this.PopupShardVar = string.Empty;
            this.EndShardState = string.Empty;
            base..ctor();
            return;
        }

        public unsafe void Init(string unitId, bool isConver)
        {
            GameManager manager;
            Json_Unit unit;
            ItemParam param;
            bool flag;
            Text text;
            int num;
            ItemParam param2;
            <Init>c__AnonStorey3CA storeyca;
            int num2;
            storeyca = new <Init>c__AnonStorey3CA();
            storeyca.uid = unitId;
            if (string.IsNullOrEmpty(storeyca.uid) == null)
            {
                goto Label_0021;
            }
            return;
        Label_0021:
            if ((this.PopupUnit == null) == null)
            {
                goto Label_0033;
            }
            return;
        Label_0033:
            manager = MonoSingleton<GameManager>.Instance;
            this.mUnitData = manager.Player.Units.Find(new Predicate<UnitData>(storeyca.<>m__461));
            if (this.mUnitData != null)
            {
                goto Label_0099;
            }
            unit = new Json_Unit();
            unit.iid = -1L;
            unit.iname = storeyca.uid;
            this.mUnitData = new UnitData();
            this.mUnitData.Deserialize(unit);
        Label_0099:
            DataSource.Bind<UnitData>(base.get_gameObject(), this.mUnitData);
            param = manager.MasterParam.GetItemParam(this.mUnitData.UnitParam.piece);
            DataSource.Bind<ItemParam>(base.get_gameObject(), param);
            GameParameter.UpdateAll(base.get_gameObject());
            flag = isConver;
            this.mIsShardEnd = flag == 0;
            if (flag == null)
            {
                goto Label_0135;
            }
            if ((this.ShardNum != null) == null)
            {
                goto Label_0135;
            }
            text = this.ShardNum.GetComponent<Text>();
            if ((text != null) == null)
            {
                goto Label_0135;
            }
            text.set_text(&this.mUnitData.GetChangePieces().ToString());
        Label_0135:
            if ((this.ShardGauge != null) == null)
            {
                goto Label_01F1;
            }
            this.ShardGauge.SetActive(flag);
            if (flag == null)
            {
                goto Label_01F1;
            }
            num = this.mUnitData.AwakeLv;
            if (num >= this.mUnitData.GetAwakeLevelCap())
            {
                goto Label_01D9;
            }
            param2 = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(this.mUnitData.UnitParam.piece);
            this.mShardWindow = this.ShardGauge.GetComponent<GetUnitShard>();
            this.mShardWindow.Refresh(this.mUnitData.UnitParam, param2.name, num, this.mUnitData.GetChangePieces(), 0);
            goto Label_01F1;
        Label_01D9:
            this.ShardGauge.get_gameObject().SetActive(0);
            this.mIsShardEnd = 1;
        Label_01F1:
            return;
        }

        public void OnCloseClick()
        {
            this.mIsClickClose = 1;
            if ((this.mShardWindow != null) == null)
            {
                goto Label_0023;
            }
            this.mShardWindow.OnClicked();
        Label_0023:
            return;
        }

        public void PlayAnim(bool isConver)
        {
            int num;
            bool flag;
            num = this.mUnitData.UnitParam.rare;
            flag = isConver;
            this.PopupUnit.SetActive(1);
            this.SpawnGetEffect(num);
            this.mPopupAnimator = this.PopupAnimation.GetComponent<Animator>();
            this.mPopupAnimator.SetInteger(this.PopupRarityVar, num + 1);
            this.mPopupAnimator.SetBool(this.PopupShardVar, flag);
            return;
        }

        private void SpawnGetEffect(int rarity)
        {
            int num;
            num = rarity;
            switch (num)
            {
                case 0:
                    goto Label_0021;

                case 1:
                    goto Label_0021;

                case 2:
                    goto Label_0021;

                case 3:
                    goto Label_0032;

                case 4:
                    goto Label_0043;
            }
            goto Label_0043;
        Label_0021:
            this.mCurrentGetEffect = this.NormalGetEffect;
            goto Label_0054;
        Label_0032:
            this.mCurrentGetEffect = this.RareGetEffect;
            goto Label_0054;
        Label_0043:
            this.mCurrentGetEffect = this.SRareGetEffect;
        Label_0054:
            if ((this.mCurrentGetEffect == null) == null)
            {
                goto Label_0066;
            }
            return;
        Label_0066:
            this.mCurrentGetEffect.SetActive(1);
            this.mCurrentGetEffect.get_transform().SetParent(this.PopupUnit.get_transform(), 0);
            return;
        }

        private void Update()
        {
            if (this.mIsEnd == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.mIsShardEnd != null)
            {
                goto Label_0040;
            }
            if ((this.mShardWindow != null) == null)
            {
                goto Label_0040;
            }
            if (this.mShardWindow.IsRunningAnimator != null)
            {
                goto Label_0040;
            }
            this.mIsShardEnd = 1;
            return;
        Label_0040:
            if (this.mIsShardEnd == null)
            {
                goto Label_008A;
            }
            if (this.mIsClickClose == null)
            {
                goto Label_008A;
            }
            if (this.mPopupAnimator.GetInteger(this.PopupRarityVar) <= 0)
            {
                goto Label_008A;
            }
            if (this.mIsShardEnd != null)
            {
                goto Label_008A;
            }
            this.mPopupAnimator.SetInteger(this.PopupRarityVar, 0);
        Label_008A:
            if (this.mIsShardEnd == null)
            {
                goto Label_00FC;
            }
            if (this.mIsClickClose == null)
            {
                goto Label_00FC;
            }
            if (this.mPopupAnimator.GetInteger(this.PopupRarityVar) <= 0)
            {
                goto Label_00FC;
            }
            if (this.isMaxShard == null)
            {
                goto Label_00FC;
            }
            if ((this.mShardWindow != null) == null)
            {
                goto Label_00FC;
            }
            if (this.mShardWindow.IsRunningAnimator != null)
            {
                goto Label_00FC;
            }
            this.mPopupAnimator.SetInteger(this.PopupRarityVar, 0);
            this.isMaxShard = 0;
        Label_00FC:
            if (this.mIsEnd != null)
            {
                goto Label_0135;
            }
            if (this.mIsShardEnd == null)
            {
                goto Label_0135;
            }
            if (this.mIsClickClose == null)
            {
                goto Label_0135;
            }
            if (GameUtility.IsAnimatorRunning(this.mPopupAnimator) != null)
            {
                goto Label_0135;
            }
            this.mIsEnd = 1;
            return;
        Label_0135:
            return;
        }

        public bool IsEnd
        {
            get
            {
                return this.mIsEnd;
            }
        }

        [CompilerGenerated]
        private sealed class <Init>c__AnonStorey3CA
        {
            internal string uid;

            public <Init>c__AnonStorey3CA()
            {
                base..ctor();
                return;
            }

            internal bool <>m__461(UnitData u)
            {
                return (u.UnitID == this.uid);
            }
        }
    }
}

