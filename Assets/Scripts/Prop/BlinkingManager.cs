using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamedevGBG.Prop
{
    public class BlinkingManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _lights;

        private float _timer = 2f;
        private float _globalTimer = 0f;

        private void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer < 0)
            {
                foreach (var light in _lights)
                {
                    light.SetActive(!light.activeInHierarchy);
                }
                if (_lights[0].activeInHierarchy)
                {
                    _timer = Random.Range(1f, 2f);
                }
                else
                {
                    _timer = Random.Range(.1f, .5f);
                }
            }

            _globalTimer += Time.deltaTime;

            if (_globalTimer >= 7f)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
