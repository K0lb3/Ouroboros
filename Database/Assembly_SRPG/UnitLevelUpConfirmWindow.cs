namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(0, "Close", 1, 0)]
    public class UnitLevelUpConfirmWindow : MonoBehaviour, IFlowInterface
    {
        [SerializeField]
        private RectTransform ItemLayoutParent;
        [SerializeField]
        private GameObject ItemTemplate;
        [SerializeField]
        private Button DecideButton;
        private List<GameObject> mExpItems;
        public ConfirmDecideEvent OnDecideEvent;
        private Dictionary<string, int> mSelectItems;

        public UnitLevelUpConfirmWindow()
        {
            this.mExpItems = new List<GameObject>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
        }

        private void Awake()
        {
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_001D;
            }
            this.ItemTemplate.SetActive(0);
        Label_001D:
            if ((this.DecideButton != null) == null)
            {
                goto Label_004A;
            }
            this.DecideButton.get_onClick().AddListener(new UnityAction(this, this.OnDecide));
        Label_004A:
            return;
        }

        private void OnDecide()
        {
            if (this.OnDecideEvent == null)
            {
                goto Label_0016;
            }
            this.OnDecideEvent();
        Label_0016:
            FlowNode_GameObject.ActivateOutputLinks(this, 0);
            return;
        }

        public unsafe void Refresh(Dictionary<string, int> dict)
        {
            GameManager manager;
            string str;
            Dictionary<string, int>.KeyCollection.Enumerator enumerator;
            ItemParam param;
            ItemData data;
            GameObject obj2;
            if (dict == null)
            {
                goto Label_0012;
            }
            if (dict.Count >= 0)
            {
                goto Label_0013;
            }
        Label_0012:
            return;
        Label_0013:
            manager = MonoSingleton<GameManager>.Instance;
            enumerator = dict.Keys.GetEnumerator();
        Label_0025:
            try
            {
                goto Label_00A9;
            Label_002A:
                str = &enumerator.Current;
                param = manager.MasterParam.GetItemParam(str);
                if (param == null)
                {
                    goto Label_00A9;
                }
                if (dict[str] <= 0)
                {
                    goto Label_00A9;
                }
                data = new ItemData();
                data.Setup(0L, param, dict[str]);
                obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
                obj2.get_transform().SetParent(this.ItemLayoutParent, 0);
                DataSource.Bind<ItemData>(obj2, data);
                this.mExpItems.Add(obj2);
                obj2.SetActive(1);
            Label_00A9:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_002A;
                }
                goto Label_00C6;
            }
            finally
            {
            Label_00BA:
                ((Dictionary<string, int>.KeyCollection.Enumerator) enumerator).Dispose();
            }
        Label_00C6:
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private void Start()
        {
        }

        public delegate void ConfirmDecideEvent();
    }
}

