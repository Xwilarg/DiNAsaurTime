using UnityEngine;

namespace GamedevGBG.Prop
{
    internal class PotionSpawner : AContainer
    {
        [SerializeField]
        private Transform[] _slots;
        [SerializeField]
        private GameObject _prefab;

        [SerializeField]
        private Material[] _mats;

        public override int TargetCount =>_slots.Length;

        public override Vector3 GetPosition(int index)
        {
            return _slots[index].position;
        }

        private void Start()
        {
            Init();
            foreach (var s in _slots)
            {
                var go = Instantiate(_prefab, s.transform.position, _prefab.transform.rotation);
                var mesh = go.GetComponent<MeshRenderer>();
                Material[] matArray = mesh.materials;
                matArray[1] = _mats[Random.Range(0, _mats.Length)];
                mesh.materials = matArray;
                Add(go);
            }
        }
    }
}
