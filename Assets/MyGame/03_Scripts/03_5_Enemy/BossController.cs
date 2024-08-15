namespace Core.GamePlay.Enemy{
    public class BossController : BaseEnemyController<BossBlackBoard>{
        private BossSO _bossSO;

        public void Init(BossSO bossSO){
            _initData = bossSO.initData;
            _bossSO = bossSO;
            _runtimeData = new BossData();
            IsIgnore = false;
            _hpBarController.SetHP(_runtimeData.HP, bossSO.initData.HP);
            ChangeAction(_startState);
        }

    }
}