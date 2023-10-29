using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleeding : MonoBehaviour
{
    [SerializeField] GameObject[] blood = new GameObject[3];
    private void Start()
    {
        StartCoroutine(BloodDrops());
    }
    IEnumerator BloodDrops()
    {
        yield return new WaitForSeconds(1);
        Instantiate(blood[Random.Range(0, 3)], new Vector3(transform.position.x, transform.position.y - 4, transform.position.z), Quaternion.identity);
        StartCoroutine(BloodDrops());
    }
}
