using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Jump Parameters")]
    [SerializeField] private float _jumpDuration = 1f;
    [SerializeField] private float _jumpHeight = 2f;
    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private AnimationCurve _fallCurve;

    [Header("Slide Parameters")]
    [SerializeField] private float _slideDuration = 1f;
    [SerializeField] private Transform[] _slideTarget;

    [Header("Components")]
    [SerializeField] private Animator _animator;

    [Header("Debugs")]
    [SerializeField] private int _currentLaneIndex = 1;
    [SerializeField] private bool _isSliding;
    [SerializeField] private bool _isJumping;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Jump (saut)
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            if(_isJumping)
            {
                return;
            }

            StartCoroutine(JumpCoroutine());
        }
        // Slide left (gauche)
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            if(_isSliding)
            {
                return;
            }

            if(_currentLaneIndex == 0)
            {
                return;
            }

            _currentLaneIndex --;
            StartCoroutine(SlideCoroutine(_slideTarget[_currentLaneIndex]));
        }
        // Slide right (droite)
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            if (_isSliding)
            {
                return;
            }

            if (_currentLaneIndex == _slideTarget.Length - 1)
            {
                return;
            }

            _currentLaneIndex++;
            StartCoroutine(SlideCoroutine(_slideTarget[_currentLaneIndex]));
        }
    }

    private IEnumerator JumpCoroutine()
    {
        _isJumping = true;
        _animator.SetBool("IsJumping", true);
        var jumpTimer = 0f; // var = float
        var halfJumpDuration = _jumpDuration / 2f;

        // -- JUMP -- //
        while (jumpTimer < halfJumpDuration)
        {
            jumpTimer += Time.deltaTime;
            var normalizedTime = jumpTimer / halfJumpDuration; // Durée de l'animation du saut

            var targetHeight = _jumpCurve.Evaluate(normalizedTime) * _jumpHeight;
            var targetPosition = new Vector3(transform.position.x, targetHeight, transform.position.z);

            transform.position = targetPosition;

            yield return null;
        }

        // -- FALL -- //
        _animator.SetTrigger("Falling");
        jumpTimer = 0f;

        while (jumpTimer < halfJumpDuration)
        {
            jumpTimer += Time.deltaTime;
            var normalizedTime = jumpTimer / halfJumpDuration; // Durée de l'animation de chute

            var targetHeight = _fallCurve.Evaluate(normalizedTime) * _jumpHeight;
            var targetPosition = new Vector3(transform.position.x, targetHeight, transform.position.z);

            transform.position = targetPosition;

            yield return null;
        }

        _isJumping = false;
        _animator.SetBool("IsJumping", false);

    }

    private IEnumerator SlideCoroutine(Transform target)
    {
        _isSliding = true;

        var slideTimer = 0f;
        while (slideTimer < _slideDuration)
        {
            slideTimer += Time.deltaTime;

            var normalizedTime = slideTimer / _slideDuration;
            var targetPosition = new Vector3(target.position.x,transform.position.y , target.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPosition, normalizedTime);

            yield return null;
        }

        _isSliding = false;
    }
}
