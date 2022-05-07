using System.Linq;
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

        [SerializeField]
        private int _baseSpawnCount;

        public override int TargetCount =>_slots.Length;

        public override Vector3 GetPosition(int index)
        {
            return _slots[index].position;
        }

        private void Start()
        {
            Init();
            for (int i = 0; i < _baseSpawnCount; i++)
            {
                var go = Instantiate(_prefab, Vector3.zero, _prefab.transform.rotation);
                if (_mats.Any())
                {
                    var mesh = go.GetComponent<MeshRenderer>();
                    Material[] matArray = mesh.materials;
                    matArray[1] = _mats[Random.Range(0, _mats.Length)];
                    mesh.materials = matArray;
                }
                Add(go);
            }
        }
    }
}
