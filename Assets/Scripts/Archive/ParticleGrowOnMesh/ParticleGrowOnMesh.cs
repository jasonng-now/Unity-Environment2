using UnityEngine;
using VRStandardAssets.Utils;

namespace VRStandardAssets.Examples
{
    public class ParticleGrowOnMesh : MonoBehaviour
    {
        [SerializeField]
        private VRInteractiveItem m_InteractiveItem;
        [SerializeField]
        private float emissionRate = 10F;
        [SerializeField]
        private GameObject parent;

        ParticleSystem[] psArray;

        // Use this for initialization
        void Start()
        {
            psArray = gameObject.GetComponentsInChildren<ParticleSystem>();

            foreach (ParticleSystem ps in psArray)
            {
                float psScale = (parent == null) ? 1F : parent.transform.localScale.x;
                float parentScale = ps.transform.parent.localScale.x;
                Vector3 psScaleVector = new Vector3(gameObject.transform.localScale.x * psScale * parentScale, gameObject.transform.localScale.y * psScale * parentScale, gameObject.transform.localScale.z * psScale * parentScale);
                ps.transform.localScale = psScaleVector;
            }
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

            foreach(ParticleSystem ps in psArray)
            {
                var emission = ps.emission;
                var rate = emission.rate;
                rate.constantMax = Random.Range(emissionRate-10, emissionRate+10);
                emission.rate = rate;
            }
        }

        //Handle the Out event
        private void HandleOut()
        {
            Debug.Log("Show out state");

            foreach (ParticleSystem ps in psArray)
            {
                var emission = ps.emission;
                var rate = emission.rate;
                rate.constantMax = 0F;
                emission.rate = rate;
            }
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
