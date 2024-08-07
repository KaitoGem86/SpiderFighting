using System.Collections.Generic;
using Core.SystemGame.Factory;
using UnityEngine;

namespace Core.GamePlay.Support{
    [CreateAssetMenu(fileName = nameof(SkinElementSO), menuName = ("GameElements/") + nameof(SkinElementSO))]
    public class SkinElementSO : BaseSOWithPool{
        public List<SkinData> SkinDatas;
        public List<SkinElement> SkinElements = new List<SkinElement>();

        public List<SkinElement> Spawn(){
            foreach (var element in SkinElements){
                if(element == null) continue;
                DespawnObject(element.gameObject);
            }
            SkinElements.Clear();
            foreach (var skinData in SkinDatas){
                var skinElement = SpawnObject().GetComponent<SkinElement>();
                skinElement.Init(skinData);
                SkinElements.Add(skinElement);
            }
            return SkinElements;
        }
    }
}