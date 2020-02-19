using Cinemachine;
using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        private CinemachineComposer composer;

        [SerializeField]
        private float sensitivity = 1f;

        [Range(1,8)]
        public float yMin = 3f;
        [Range(1,8)]
        public float yMax = 5f;
        [Range(-3,5)]
        public float xMin = -1f;
        [Range(-3,5)]
        public float xMax = 3f;
    

        private void Start()
        {
            composer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>();
        }

        private void Update()
        {    
            //Kameramızın hareket edeceği yönü alıyoruz ve hassaslığını ayarlıyoruz
            float vertical = Input.GetAxis("Mouse Y") * sensitivity;
            float horizontal = Input.GetAxis("Mouse X") * sensitivity;
        
        
            composer.m_TrackedObjectOffset.y += vertical; //Dikey hareket
            composer.m_TrackedObjectOffset.y = Mathf.Clamp(composer.m_TrackedObjectOffset.y, yMin, yMax);//Hareketi belirlenen aralıkta kısıtlar
        
            composer.m_TrackedObjectOffset.x += horizontal;//Yatay hareket
            composer.m_TrackedObjectOffset.x = Mathf.Clamp(composer.m_TrackedObjectOffset.x, xMin, xMax);//Hareketi belirlenen aralıkta kısıtlar
        }
    }
}
