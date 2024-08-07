using System;
using System.Collections.Generic;
using Core.SystemGame.Factory;
using UnityEngine;

namespace Core.GamePlay.MyPlayer{
    [Serializable]
    public class GadgetData {
        public Sprite icon;
        public int id;
        public string name;
    }

    [CreateAssetMenu(fileName = "GadgetDataSO", menuName = "GameElements/GadgetDataSO", order = 0)]
    public class GadgetDataSO : BaseSOWithPool{
        public List<GadgetData> gadgets;

        public GameObject Spawn(GadgetData data){
            var go = SpawnObject();
            go.GetComponent<GadgetElement>().Init(data);
            return go;
        }
    }
}