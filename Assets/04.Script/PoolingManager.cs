using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    [SerializeField] private GameObject smoke;
    [SerializeField] float smokeValue;
    int index = 0;

    private void Awake()
    {
        for (int i = 0; i < smokeValue; i++)
        {
            GameObject sm = Instantiate(smoke, gameObject.transform);
            sm.SetActive(false);
        }
    }

    public void PopSmoke(Vector3 point, Quaternion quaternion)
    {
        if (index + 1 >= smokeValue)
            index = 0;
        StartCoroutine(Pop(point, quaternion, index));
        index++;
    }

    IEnumerator Pop(Vector3 point, Quaternion quaternion, int ind)
    {
        gameObject.transform.GetChild(ind).gameObject.SetActive(true);
        gameObject.transform.GetChild(ind).transform.position = point;
        gameObject.transform.GetChild(ind).transform.rotation = quaternion;

        yield return new WaitForSeconds(0.3f);

        gameObject.transform.GetChild(ind).gameObject.SetActive(false);
    }
}
