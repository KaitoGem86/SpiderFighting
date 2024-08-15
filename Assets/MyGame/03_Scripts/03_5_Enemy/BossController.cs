namespace Core.GamePlay.Enemy{
    public class BossController : BaseEnemyController<BossBlackBoard>{
        private BossSO _bossSO;

        public void Init(BossSO bossSO){
            _initData = bossSO.bossData;
            _bossSO = bossSO;
            _runtimeData = new BossData(bossSO.bossData);
            IsIgnore = false;
            _hpBarController.SetHP(_runtimeData.HP, bossSO.bossData.HP);
            ChangeAction(_startState);
        }

    }
}