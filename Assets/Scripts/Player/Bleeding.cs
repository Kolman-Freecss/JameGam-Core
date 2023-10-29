using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Bleeding : MonoBehaviour
{
    [SerializeField] GameObject[] blood = new GameObject[3];
    public bool bleeding = false;
    private Coroutine _bleedCoroutine;
    
    public void StartBleed()
    {
        bleeding = true;
        _bleedCoroutine = StartCoroutine(BloodDrops());
    }
    
    public void StopBleed()
    {
        bleeding = false;
        StopCoroutine(_bleedCoroutine);
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
