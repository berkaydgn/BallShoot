using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bucket"))
        {
            Renderer color = GetComponent<Renderer>();
            _gameManager.ParcEffects(gameObject.transform.position, color.material.color);

            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            gameObject.SetActive(false);
            _gameManager.ValidBalls();
        }

        else if (other.CompareTag("BottomObject"))
        {
            Renderer color = GetComponent<Renderer>();
            _gameManager.ParcEffects(gameObject.transform.position, color.material.color);

            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            gameObject.SetActive(false);
            _gameManager.InvalidBalls();
        }
    }


}
