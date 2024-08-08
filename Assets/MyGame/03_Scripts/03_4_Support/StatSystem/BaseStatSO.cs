using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Data.Stat{
    public class BaseStatSO<T> : ScriptableObject where T : struct 
    {
        public SerializedDictionary<T, float> instancesStats;
        public SerializedDictionary<T, float> stats;

        public float GetGlobalStat(T key){
            if (stats.ContainsKey(key)){
                return stats[key];
            }
            throw new System.Exception("Stat not found");
        }

        public Dictionary<T, float> GetInstancesStats(){
            return new Dictionary<T, float>(instancesStats);
        }
    }
}