using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TargetingLaser : MonoBehaviour
{
    // The laser travels in an elliptical path. A represents horizontal radius while B represents vertical radius.
    [SerializeField] private float angularDisplacement;
    [SerializeField] private float startA = 2f;
    [SerializeField] private float startB = 4f;
    [SerializeField] private float angularSpeed;

    private bool isArmed = false;
    private float timeToCharge;
    private float laserCycleTime;

    private float currentA;
    private float currentB;
    private float currentAngle;

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void OnEnable()
    {
        this.currentA = startA;
        this.currentB = startB;
        this.currentAngle = angularDisplacement;

        UpdatePosition();
    }

    private void Update()
    {
        if (isArmed)
        {
            SetNextPosition();
            UpdatePosition();
        }
    }

    public void ArmLaser(float timeToCharge)
    {
        this.isArmed = true;
        this.timeToCharge = timeToCharge;
        this.laserCycleTime = timeToCharge - 1f;

        StartCoroutine(SwitchOffLaser());
    }

    private void UpdatePosition()
    {
        float x = currentB * Mathf.Sin(Mathf.Deg2Rad * currentAngle);
        float y = currentA * Mathf.Cos(Mathf.Deg2Rad * currentAngle);

        transform.localPosition = new Vector3(x, y, 0);
    }

    private void SetNextPosition()
    {
        currentAngle = (currentAngle + angularSpeed * Time.deltaTime) % 360f;
        currentA = Mathf.Max(0f, currentA - (startA / laserCycleTime) * Time.deltaTime);
        currentB = Mathf.Max(0f, currentB - (startB / laserCycleTime) * Time.deltaTime);
    }

    private IEnumerator SwitchOffLaser()
    {
        yield return new WaitForSeconds(timeToCharge);

        gameObject.SetActive(false);
    }
}
