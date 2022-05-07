using UnityEngine;

namespace GamedevGBG.Prop
{
    public class MachineManager : MonoBehaviour
    {
        public static MachineManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        [SerializeField]
        private Machine[] _machines;

        public void RemoveFromMachine(GameObject go)
        {
            foreach (var machine in _machines)
            {
                machine.Remove(go);
            }
        }
    }
}
