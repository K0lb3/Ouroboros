namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "In", 0, 0), NodeType("SRPG/クエスト選択", 0x7fe5), Pin(100, "Out", 1, 100)]
    public class FlowNode_SelectLatestChapter : FlowNode
    {
        public SelectModes Selection;

        public FlowNode_SelectLatestChapter()
        {
            base..ctor();
            return;
        }

        public override unsafe void OnActivate(int pinID)
        {
            long num;
            DateTime time;
            SelectModes modes;
            DayOfWeek week;
            if (pinID != null)
            {
                goto Label_015E;
            }
            switch (this.Selection)
            {
                case 0:
                    goto Label_0028;

                case 1:
                    goto Label_0032;

                case 2:
                    goto Label_010E;

                case 3:
                    goto Label_0131;
            }
            goto Label_0154;
        Label_0028:
            SelectLatestChapter();
            goto Label_0155;
        Label_0032:
            time = TimeManager.FromUnixTime(Network.GetServerTime());
            GlobalVars.SelectedSection.Set("WD_DAILY");
            switch (&time.DayOfWeek)
            {
                case 0:
                    goto Label_007D;

                case 1:
                    goto Label_0091;

                case 2:
                    goto Label_00A5;

                case 3:
                    goto Label_00B9;

                case 4:
                    goto Label_00CD;

                case 5:
                    goto Label_00E1;

                case 6:
                    goto Label_00F5;
            }
            goto Label_0109;
        Label_007D:
            GlobalVars.SelectedChapter.Set("AR_SUN");
            goto Label_0109;
        Label_0091:
            GlobalVars.SelectedChapter.Set("AR_MON");
            goto Label_0109;
        Label_00A5:
            GlobalVars.SelectedChapter.Set("AR_TUE");
            goto Label_0109;
        Label_00B9:
            GlobalVars.SelectedChapter.Set("AR_WED");
            goto Label_0109;
        Label_00CD:
            GlobalVars.SelectedChapter.Set("AR_THU");
            goto Label_0109;
        Label_00E1:
            GlobalVars.SelectedChapter.Set("AR_FRI");
            goto Label_0109;
        Label_00F5:
            GlobalVars.SelectedChapter.Set("AR_SAT");
        Label_0109:
            goto Label_0155;
        Label_010E:
            GlobalVars.SelectedSection.Set("WD_DAILY");
            GlobalVars.SelectedChapter.Set(string.Empty);
            goto Label_0155;
        Label_0131:
            GlobalVars.SelectedSection.Set("WD_CHARA");
            GlobalVars.SelectedChapter.Set(string.Empty);
            goto Label_0155;
        Label_0154:
            return;
        Label_0155:
            base.ActivateOutputLinks(100);
        Label_015E:
            return;
        }

        public static void SelectLatestChapter()
        {
            QuestParam[] paramArray;
            string str;
            QuestParam param;
            int num;
            string str2;
            QuestParam param2;
            int num2;
            ChapterParam[] paramArray2;
            int num3;
            paramArray = MonoSingleton<GameManager>.Instance.Quests;
            str = null;
            param = null;
            num = 0;
            str2 = PlayerPrefsUtility.GetString(PlayerPrefsUtility.LAST_SELECTED_STORY_QUEST_ID, string.Empty);
            if (string.IsNullOrEmpty(str2) != null)
            {
                goto Label_0089;
            }
            param2 = MonoSingleton<GameManager>.Instance.FindQuest(str2);
            if (param2 == null)
            {
                goto Label_0089;
            }
            if (param2.Chapter == null)
            {
                goto Label_0089;
            }
            if (param2.Chapter.sectionParam == null)
            {
                goto Label_0089;
            }
            if (param2.Chapter.sectionParam.storyPart <= 0)
            {
                goto Label_0089;
            }
            num = param2.Chapter.sectionParam.storyPart;
        Label_0089:
            num2 = 0;
            goto Label_0104;
        Label_0091:
            if (paramArray[num2].IsStory == null)
            {
                goto Label_00FE;
            }
            if (num <= 0)
            {
                goto Label_00E5;
            }
            if (paramArray[num2].Chapter == null)
            {
                goto Label_00E5;
            }
            if (paramArray[num2].Chapter.sectionParam == null)
            {
                goto Label_00E5;
            }
            if (num == paramArray[num2].Chapter.sectionParam.storyPart)
            {
                goto Label_00E5;
            }
            goto Label_00FE;
        Label_00E5:
            param = paramArray[num2];
            if (paramArray[num2].state == 2)
            {
                goto Label_00FE;
            }
            goto Label_010E;
        Label_00FE:
            num2 += 1;
        Label_0104:
            if (num2 < ((int) paramArray.Length))
            {
                goto Label_0091;
            }
        Label_010E:
            if (param == null)
            {
                goto Label_01A7;
            }
            str = param.ChapterID;
            paramArray2 = MonoSingleton<GameManager>.Instance.Chapters;
            num3 = 0;
            goto Label_019C;
        Label_012F:
            if ((paramArray2[num3].iname == str) == null)
            {
                goto Label_0196;
            }
            GlobalVars.SelectedSection.Set(paramArray2[num3].section);
            GlobalVars.SelectedChapter.Set(str);
            if (paramArray2[num3].sectionParam == null)
            {
                goto Label_018B;
            }
            GlobalVars.SelectedStoryPart.Set(paramArray2[num3].sectionParam.storyPart);
        Label_018B:
            GlobalVars.SelectedQuestID = null;
            goto Label_01A7;
        Label_0196:
            num3 += 1;
        Label_019C:
            if (num3 < ((int) paramArray2.Length))
            {
                goto Label_012F;
            }
        Label_01A7:
            return;
        }

        public enum SelectModes
        {
            Latest,
            DailyChapter,
            DailySection,
            CharacterQuestSection
        }
    }
}

