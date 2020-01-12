using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSelf : MonoBehaviour
{
    private float time = 0;
    [SerializeField] private float deathtime = 3;

    void Update()
    {
        time += 1 * Time.deltaTime;
        if (time >= deathtime)
        {
            Destroy(this.gameObject);
        }
    }
}
