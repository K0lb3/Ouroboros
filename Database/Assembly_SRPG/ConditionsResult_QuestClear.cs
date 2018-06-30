namespace SRPG
{
    using System;

    public class ConditionsResult_QuestClear : ConditionsResult
    {
        private QuestParam mQuestParam;

        public ConditionsResult_QuestClear(QuestParam questParam)
        {
            base..ctor();
            this.mQuestParam = questParam;
            base.mIsClear = questParam.state == 2;
            base.mTargetValue = 2;
            base.mCurrentValue = questParam.state;
            return;
        }

        public override string text
        {
            get
            {
                object[] objArray1;
                objArray1 = new object[] { this.mQuestParam.name };
                return LocalizedText.Get("sys.TOBIRA_CONDITIONS_QUEST_CLEAR", objArray1);
            }
        }

        public override string errorText
        {
            get
            {
                return string.Format("クエスト「{0}」をクリアしていません", this.mQuestParam.name);
            }
        }
    }
}

