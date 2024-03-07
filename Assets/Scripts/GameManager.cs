using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] Bullets;
    public GameObject FirePoint;
    public float BulletPower;
    int activeBulletIndex;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Bullets[activeBulletIndex].transform.SetPositionAndRotation(FirePoint.transform.position, FirePoint.transform.rotation);
            Bullets[activeBulletIndex].SetActive(true);
            Bullets[activeBulletIndex].GetComponent<Rigidbody>().AddForce(Bullets[activeBulletIndex].transform.TransformDirection(90, 90, 0) * BulletPower, ForceMode.Force);

            if (Bullets.Length - 1 == activeBulletIndex)
                activeBulletIndex = 0;
            else 
                activeBulletIndex++;

        }

    }
}
