using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text counter;

    private void Update()
    {
        counter.text = MMTime.FloatToTimeString(Time.time);
    }
}
