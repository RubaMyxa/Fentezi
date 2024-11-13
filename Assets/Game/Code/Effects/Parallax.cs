using UnityEngine;

namespace Assets.Game.Code.Effects
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField]
        private float parallaxIntencity;

        private Camera cam;
        private float startPos, spriteLength;

        private void Awake()
        {
            cam = Camera.main;

            startPos = transform.localPosition.x;
            spriteLength = GetComponent<SpriteRenderer>().bounds.size.x;
            print(spriteLength);
        }

        private void Update()
        {
            Scroll();
        }

        private void Scroll()
        {
            Vector3 currentCamPos = cam.transform.position;
            float distance = currentCamPos.x * (1 - parallaxIntencity);

            transform.localPosition = new Vector3(startPos + distance, transform.localPosition.y, transform.localPosition.z);
        }
    }
}