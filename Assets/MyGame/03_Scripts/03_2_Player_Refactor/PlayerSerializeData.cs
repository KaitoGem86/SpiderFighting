namespace Core.GamePlay.MyPlayer{
    [System.Serializable]
    public class PlayerSerializeData {
        public int Exp;
        public int Level;
        public int cash;
        public int skinIndex;
        public int gadgetIndex;
        public int skillPoint;
        public int yellowPiece;
        public int purplePiece;


        public void InitData(){
            Exp = 0;
            Level = 1;
            cash = 0;
            skinIndex = 5;
            gadgetIndex = 0;
            skillPoint = 0;
            yellowPiece = 0;
            purplePiece = 0;
        }
    }
}