using UnityEngine;

namespace GamedevGBG.Prop
{
    internal class PotionSpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _slots;
        [SerializeField]
        private GameObject _prefab;

        [SerializeField]
        private Material[] _mats;

        private void Start()
        {
            foreach (var s in _slots)
            {
                var go = Instantiate(_prefab, s.transform.position, _prefab.transform.rotation);
                var mesh = go.GetComponent<MeshRenderer>();
                Material[] matArray = mesh.materials;
                matArray[1] = _mats[Random.Range(0, _mats.Length)];
                mesh.materials = matArray;
            }
        }
    }
}
