using UnityEngine;

namespace GamedevGBG.Prop
{
    public class VialManager : MonoBehaviour
    {
        public static VialManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
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
    }
}
