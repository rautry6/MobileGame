using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class SlimeThrowUI : MonoBehaviour
    {
        [SerializeField] Transform slimeUIHolder;
        [SerializeField] GameObject SlimeUIParentPrefab;
        [SerializeField] Mesh currentSlimeMesh;
        [SerializeField] Material currentSlimeMaterial;
        [SerializeField] Material transparentMaterial;

        public int numberOfThrows;
        private List<GameObject> throwUis;

        // Start is called before the first frame update
        void Start()
        {
            //Test
            throwUis = new List<GameObject>();

           
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void SetSlimeModel(Mesh slime, Material mat)
        {
            currentSlimeMesh = slime;

            PopulateUI();
        }

        public void PopulateUI()
        {
            for(int i = 0; i < numberOfThrows;  i++)
            {
                GameObject g = Instantiate(SlimeUIParentPrefab, slimeUIHolder);
                GameObject gChild = g.transform.GetChild(0).gameObject;

                gChild.GetComponent<MeshFilter>().mesh = currentSlimeMesh;
                gChild.GetComponent<MeshRenderer>().material = (Material) Resources.Load("Tranparent", typeof(Material));

                throwUis.Add(g);
            }
        }

        public void Thrown(int currentThrow)
        {
            throwUis[numberOfThrows - currentThrow].SetActive(false);
        }

        public void ResetLastThrow()
        {
            for(int i = 0; i < throwUis.Count; i++)
            {
                if (!throwUis[i].activeInHierarchy)
                {
                    throwUis[i].SetActive(true);
                    return;
                }
            }
        }

        public void ShowSlimes()
        {
            SetSlimeModel(currentSlimeMesh, currentSlimeMaterial);
        }
    }
}
