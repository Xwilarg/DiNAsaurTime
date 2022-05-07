using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GamedevGBG.Prop
{
    public class VialManager : MonoBehaviour
    {
        public static VialManager Instance { get; private set; }

        private Dictionary<string, VialInfo> _infoDict;

        private void Awake()
        {
            Instance = this;
            _infoDict = _info.ToDictionary(x => x.ID, x => x);
        }

        [SerializeField]
        private VialInfo[] _info;

        public VialInfo RandomVial
        {
            get
            {
                return _info[Random.Range(0, _info.Length)];
            }
        }

        public VialInfo GetByKey(string key)
        {
            return _infoDict[key];
        }
    }
}
