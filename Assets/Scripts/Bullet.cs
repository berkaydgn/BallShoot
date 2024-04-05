using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameManager _gameManager;
    private Rigidbody rb;
    private Renderer color; 

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        color = GetComponent<Renderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bucket"))
        {
            TechnicalOperations();
            _gameManager.ValidBalls();
        }
        else if (other.CompareTag("BottomObject"))
        {
            TechnicalOperations();
            _gameManager.InvalidBalls();
        }
    }
    public void TechnicalOperations()
    {
        _gameManager.ParcEffects(gameObject.transform.position, color.material.color);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
