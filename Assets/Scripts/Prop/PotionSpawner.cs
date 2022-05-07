using System.Collections;
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

        [SerializeField]
        private string[] _ids;

        public override int TargetCount => _slots.Length;

        public override Vector3 GetPosition(int index)
        {
            return _slots[index].position;
        }

        private void Start()
        {
            Init();
            for (int i = 0; i < _baseSpawnCount; i++)
            {
                var go = SpawnAndAdd(i);
                if (_ids != null && i < _ids.Length)
                {
                    go.GetComponent<PropInfo>().ID = _ids[i];
                }
            }
        }

        private GameObject SpawnAndAdd(int index)
        {
            var go = Instantiate(_prefab, Vector3.zero, _prefab.transform.rotation);
            if (_mats.Any())
            {
                var mesh = go.GetComponent<MeshRenderer>();
                Material[] matArray = mesh.materials;
                matArray[1] = _mats[Random.Range(0, _mats.Length)];
                mesh.materials = matArray;
            }
            AddAtPosition(go, index);
            return go;
        }

        public override int Remove(GameObject go)
        {
            var index = base.Remove(go);
            if (index != -1)
            {
                StartCoroutine(WaitAndRespawn(index));
            }
            return index;
        }

        private IEnumerator WaitAndRespawn(int index)
        {
            yield return new WaitForSeconds(5f);
            SpawnAndAdd(index);
        }
    }
}
