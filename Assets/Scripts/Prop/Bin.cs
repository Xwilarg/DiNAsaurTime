using UnityEngine;

namespace GamedevGBG.Prop
{
    public class Bin : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Draggable"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
