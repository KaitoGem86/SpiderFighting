using MyTools.ScreenSystem;
using MyTools.Sound;
using UnityEngine;

namespace Core.Manager
{
    public class LoadingSceneContext : MonoBehaviour
    {
        private void Start()
        {
            _ScreenManager.Instance.ShowScreen(_ScreenTypeEnum.Loading);
        }
    }
}