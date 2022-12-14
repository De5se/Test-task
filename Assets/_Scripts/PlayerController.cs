using System;
using _Scripts;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float motionDuration;

    private Vector2 _motionOffset;
    private Tween _playerMotion;
    private bool _isControlEnabled;

    public static Action WinEvent;
    public static Action LoseEvent;
    private bool _isDead;

    #region Player Cotrol
    private void Update()
    {
        if (!_isControlEnabled)
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            UpdateStartPosition();
        }
        if (Input.GetMouseButtonUp(0))
        {
            SetMotionToPlayer();
        }
    }


    private void UpdateStartPosition()
    {
    }

    private void SetMotionToPlayer()
    {
        _playerMotion.Kill();

        var targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z;
        
        _playerMotion = transform.DOMove(targetPosition, motionDuration);
    }
    
    private void DisablePlayerControl()
    {
        _isControlEnabled = false;
        _playerMotion.Kill();
    }
    
    #endregion
    
    private void OnEnable()
    {
        LoseEvent += DisablePlayerControl;
        WinEvent += DisablePlayerControl;

        _isControlEnabled = true;
    }

    private void OnDisable()
    {
        LoseEvent -= DisablePlayerControl;
        WinEvent -= DisablePlayerControl;
    }
    
    #region End Game
    private void OnCollisionEnter(Collision collision)
    {
        var collisionObject = collision.gameObject;
        if (collisionObject.TryGetComponent(out FinishObject finishObject))
        {
            Win();
            return;
        }
            
        if (collisionObject.TryGetComponent(out Obstacle obstacleObject))
        {
            Die();
        }
    }

    private static void Win()
    {
        WinEvent?.Invoke();
    }
    
    private void Die()
    {
        if (_isDead)
        {
            return;
        }
        _isDead = true;
        LoseEvent?.Invoke();
    }
    #endregion
}
