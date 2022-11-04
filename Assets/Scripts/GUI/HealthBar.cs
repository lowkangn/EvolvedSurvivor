using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private string sortingLayerName;
    // Start is called before the first frame update
    void Start()
    {
        Canvas canvas = this.GetComponent<Canvas>();

        canvas.sortingLayerName = sortingLayerName;
    }
}
