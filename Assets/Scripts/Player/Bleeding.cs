using System.Collections;
using UnityEngine;

public class Bleeding : MonoBehaviour
{
    [SerializeField] GameObject[] blood = new GameObject[3];
    public bool bleeding = false;
    private Coroutine _bleedCoroutine;

    #region InitData

    private void Start()
    {
        SubscribeToDelegatesAndUpdateValues();
    }
    
    private void SubscribeToDelegatesAndUpdateValues()
    {
        DisplaySettings.Instance.OnBloodToogle += OnBloodEvent;
    }

    #endregion
    
    private void OnBloodEvent(bool isBleeding)
    {
        Debug.Log("Blood event received" + isBleeding);
        if (isBleeding)
        {
            StartBleed();
        }
        else
        {
            StopBleed();
        }
    }
    
    public void StartBleed()
    {
        bleeding = true;
        _bleedCoroutine = StartCoroutine(BloodDrops());
    }
    
    public void StopBleed()
    {
        bleeding = false;
        if (_bleedCoroutine != null)
        {
            StopCoroutine(_bleedCoroutine);
        }
    }
    
    IEnumerator BloodDrops()
    {
        yield return new WaitForSeconds(1);
        Instantiate(blood[Random.Range(0, 3)], new Vector3(transform.position.x, transform.position.y - 4, transform.position.z), Quaternion.identity);
        if (bleeding)
        {
            StartCoroutine(BloodDrops());
        }
    }
}
