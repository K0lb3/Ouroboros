namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("カメラ/フェード", "画面をフェードイン・フェードアウトさせます", 0x555555, 0x444488)]
    public class EventAction_FadeScreen : EventAction
    {
        public FadeModes Mode;
        [HideInInspector]
        public bool FadeOut;
        public bool Async;
        public float FadeTime;
        [HideInInspector]
        public Color FadeColor;
        [StringIsActorID, SerializeField]
        private string[] ExcludeUnits;
        [StringIsActorID, SerializeField]
        private string[] IncludeUnits;

        public EventAction_FadeScreen()
        {
            this.FadeTime = 3f;
            this.FadeColor = Color.get_black();
            this.ExcludeUnits = new string[0];
            this.IncludeUnits = new string[0];
            base..ctor();
            return;
        }

        public override void GoToEndState()
        {
            FadeController controller;
            FadeController controller2;
            TacticsUnitController[] controllerArray;
            TacticsUnitController[] controllerArray2;
            int num;
            int num2;
            if (this.Mode != null)
            {
                goto Label_0042;
            }
            if (this.FadeOut == null)
            {
                goto Label_0033;
            }
            FadeController.Instance.FadeTo(this.FadeColor, 0f, 0);
            goto Label_003D;
        Label_0033:
            GameUtility.FadeIn(0f);
        Label_003D:
            goto Label_0119;
        Label_0042:
            controller2 = FadeController.Instance;
            controllerArray = null;
            controllerArray2 = null;
            if ((this.ExcludeUnits == null) || (((int) this.ExcludeUnits.Length) <= 0))
            {
                goto Label_009D;
            }
            controllerArray2 = new TacticsUnitController[(int) this.ExcludeUnits.Length];
            num = 0;
            goto Label_0093;
        Label_007B:
            controllerArray2[num] = TacticsUnitController.FindByUniqueName(this.ExcludeUnits[num]);
            num += 1;
        Label_0093:
            if (num < ((int) controllerArray2.Length))
            {
                goto Label_007B;
            }
        Label_009D:
            if ((this.IncludeUnits == null) || (((int) this.IncludeUnits.Length) <= 0))
            {
                goto Label_00EE;
            }
            controllerArray = new TacticsUnitController[(int) this.IncludeUnits.Length];
            num2 = 0;
            goto Label_00E4;
        Label_00CC:
            controllerArray[num2] = TacticsUnitController.FindByUniqueName(this.IncludeUnits[num2]);
            num2 += 1;
        Label_00E4:
            if (num2 < ((int) controllerArray.Length))
            {
                goto Label_00CC;
            }
        Label_00EE:
            controller2.BeginSceneFade(this.FadeColor, 0f, (controllerArray2 == null) ? null : controllerArray2, (controllerArray == null) ? null : controllerArray);
        Label_0119:
            return;
        }

        public override void OnActivate()
        {
            this.StartFade();
            return;
        }

        private void StartFade()
        {
            FadeController controller;
            FadeController controller2;
            TacticsUnitController[] controllerArray;
            TacticsUnitController[] controllerArray2;
            int num;
            int num2;
            if (this.Mode != null)
            {
                goto Label_0044;
            }
            if (this.FadeOut == null)
            {
                goto Label_0034;
            }
            FadeController.Instance.FadeTo(this.FadeColor, this.FadeTime, 0);
            goto Label_003F;
        Label_0034:
            GameUtility.FadeIn(this.FadeTime);
        Label_003F:
            goto Label_011C;
        Label_0044:
            controller2 = FadeController.Instance;
            controllerArray = null;
            controllerArray2 = null;
            if ((this.ExcludeUnits == null) || (((int) this.ExcludeUnits.Length) <= 0))
            {
                goto Label_009F;
            }
            controllerArray2 = new TacticsUnitController[(int) this.ExcludeUnits.Length];
            num = 0;
            goto Label_0095;
        Label_007D:
            controllerArray2[num] = TacticsUnitController.FindByUniqueName(this.ExcludeUnits[num]);
            num += 1;
        Label_0095:
            if (num < ((int) controllerArray2.Length))
            {
                goto Label_007D;
            }
        Label_009F:
            if ((this.IncludeUnits == null) || (((int) this.IncludeUnits.Length) <= 0))
            {
                goto Label_00F0;
            }
            controllerArray = new TacticsUnitController[(int) this.IncludeUnits.Length];
            num2 = 0;
            goto Label_00E6;
        Label_00CE:
            controllerArray[num2] = TacticsUnitController.FindByUniqueName(this.IncludeUnits[num2]);
            num2 += 1;
        Label_00E6:
            if (num2 < ((int) controllerArray.Length))
            {
                goto Label_00CE;
            }
        Label_00F0:
            controller2.BeginSceneFade(this.FadeColor, this.FadeTime, (controllerArray2 == null) ? null : controllerArray2, (controllerArray == null) ? null : controllerArray);
        Label_011C:
            if (this.Async == null)
            {
                goto Label_012E;
            }
            base.ActivateNext();
            return;
        Label_012E:
            return;
        }

        public override void Update()
        {
            if (this.Mode != null)
            {
                goto Label_001B;
            }
            if (GameUtility.IsScreenFading == null)
            {
                goto Label_002B;
            }
            return;
            goto Label_002B;
        Label_001B:
            if (FadeController.Instance.IsSceneFading == null)
            {
                goto Label_002B;
            }
            return;
        Label_002B:
            base.ActivateNext();
            return;
        }

        public enum FadeModes
        {
            Screen,
            Scene
        }
    }
}

