namespace Core.GamePlay.Mission
{
    [System.Serializable]
    public struct QuestInfor
    {
        public int id;
        public string QuestName;
        public string QuestDescription;
    }

    [System.Serializable]
    public struct RewardInfor{
        public int exp;
        public int currency;
    }
}