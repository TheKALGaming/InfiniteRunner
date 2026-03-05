using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCollisionController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private Vector3 _sphereCenter;
    [SerializeField] private float _sphereRadius;

    private bool _isHit;

    private Vector3 _playerSpherePosition => transform.position + _sphereCenter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventSystem.EventSystem.OnPlayerSlideDown += ShrinkCollider;
    }

    // Update is called once per frame
    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(_playerSpherePosition, _sphereRadius);

        if (hitColliders.Length > 0 && !_isHit)
        {
            Debug.Log("Player hit something");
            _isHit = true;
        }

        if (hitColliders.Length == 0)
        {
            _isHit = false;
        }
    }

    public void ShrinkCollider(bool isSlidingDown)
    {

    }

    // Créer un gizmo
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_playerSpherePosition, _sphereRadius);
    }
}
