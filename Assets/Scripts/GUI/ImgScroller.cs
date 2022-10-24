using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImgScroller : MonoBehaviour
{
    [SerializeField] private RawImage bgImg; 
    [SerializeField] private float speed_x, speed_y;

    // Update is called once per frame
    void Update()
    {
        bgImg.uvRect = new Rect(bgImg.uvRect.position + new Vector2(speed_x, speed_y) * Time.deltaTime, bgImg.uvRect.size);
    }
}
