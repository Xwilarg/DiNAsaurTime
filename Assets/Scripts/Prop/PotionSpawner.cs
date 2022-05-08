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
        private MaterialArray[] _mats;

        [SerializeField]
        private bool _randomMat;

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

        private int[] _orders = new[]
        {
            1, 3, 4, 2
        };
        private GameObject SpawnAndAdd(int index)
        {
            var go = Instantiate(_prefab, Vector3.zero, _prefab.transform.rotation);
            if (_randomMat)
            {
                var mesh = go.GetComponent<MeshRenderer>();
                Material[] matArray = mesh.materials;
                var rand = VialManager.Instance.RandomVial;
                matArray[1] = rand.Mat;
                mesh.materials = matArray;
                go.GetComponent<PropInfo>().ID = rand.ID;
            }
            else if (_mats.Any())
            {
                var mesh = go.GetComponent<MeshRenderer>();
                Material[] matArray = mesh.materials;
                var i = 1;
                foreach (var m in _mats[index].Mats)
                {
                    matArray[_orders[i - 1]] = m;
                    i++;
                }                    
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
            var go = SpawnAndAdd(index);
            go.GetComponent<PropInfo>().ID = _ids[index];
        }
    }
}
