using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPopup : MonoBehaviour
{
    public GameObject memoryNarrationPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateMemoryNarration(GameObject meomory)
    {
        meomory.SetActive(true);
    }
}
