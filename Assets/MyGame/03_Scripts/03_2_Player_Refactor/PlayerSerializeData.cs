namespace Core.GamePlay.MyPlayer{
    [System.Serializable]
    public class PlayerSerializeData {
        public int Exp;
        public int Level;
        public int Currency;
        public int skinIndex;
        public int gadgetIndex;

        public void InitData(){
            Exp = 0;
            Level = 1;
            Currency = 0;
            skinIndex = 5;
            gadgetIndex = 0;
        }
    }
}