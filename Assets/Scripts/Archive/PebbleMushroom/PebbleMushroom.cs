using UnityEngine;
using VRStandardAssets.Utils;

namespace VRStandardAssets.Examples
{
    public class PebbleMushroom : MonoBehaviour
    {
        [SerializeField]
        private VRInteractiveItem m_InteractiveItem;
        [SerializeField]
        private float emissionRate = 10F;
        [SerializeField]
        private GameObject parent;

        ParticleSystem ps;
        Light lt;

        // Use this for initialization
        void Start()
        {
            ps = gameObject.GetComponentInChildren<ParticleSystem>();
            lt = gameObject.GetComponentInChildren<Light>();

            float psScale = (parent == null) ? 1F : parent.transform.localScale.x;
            Vector3 psScaleVector = new Vector3(gameObject.transform.localScale.x * psScale, gameObject.transform.localScale.y * psScale, gameObject.transform.localScale.z * psScale);
            ps.transform.localScale = psScaleVector;
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void OnEnable()
        {
            m_InteractiveItem.OnOver += HandleOver;
            m_InteractiveItem.OnOut += HandleOut;
            m_InteractiveItem.OnClick += HandleClick;
            m_InteractiveItem.OnDoubleClick += HandleDoubleClick;
        }

        private void OnDisable()
        {
            m_InteractiveItem.OnOver -= HandleOver;
            m_InteractiveItem.OnOut -= HandleOut;
            m_InteractiveItem.OnClick -= HandleClick;
            m_InteractiveItem.OnDoubleClick -= HandleDoubleClick;
        }

        //Handle the Over event
        private void HandleOver()
        {
            Debug.Log("Show over state");

            var emission = ps.emission;
            var rate = emission.rate;
            rate.constantMax = emissionRate;
            emission.rate = rate;

            //lt.intensity = 5F;
        }

        //Handle the Out event
        private void HandleOut()
        {
            Debug.Log("Show out state");

            var emission = ps.emission;
            var rate = emission.rate;
            rate.constantMax = 0F;
            emission.rate = rate;

            //lt.intensity = 0;
        }

        //Handle the Click event
        private void HandleClick()
        {
            Debug.Log("Show click state");
        }

        //Handle the DoubleClick event
        private void HandleDoubleClick()
        {
            Debug.Log("Show double click");
        }
    }
}