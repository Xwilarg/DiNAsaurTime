using UnityEngine;

namespace GamedevGBG.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/MachineInfo", fileName = "MachineInfo")]
    public class MachineInfo : ScriptableObject
    {
        public float ProcessTime;
    }
}