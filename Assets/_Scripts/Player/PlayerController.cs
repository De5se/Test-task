using System;
using _Scripts;
using _Scripts.Player;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private DirectionArrow directionArrow;
    [SerializeField] private float motionDuration;

    private Vector3 _startPosition;
    private Tween _playerMotion;
    private bool _isControlEnabled;

    public static Action WinEvent;
    public static Action LoseEvent;
    private bool _isDead;

    #region Player Control
    private void Update()
    {
        if (!_isControlEnabled)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            UpdateStartPosition();
        }

        if (Input.GetMouseButton(0))
        {
            Aim();
        }
        if (Input.GetMouseButtonUp(0))
        {
            SetMotionToPlayer();
        }
    }

    private void UpdateStartPosition()
    {
        _startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        directionArrow.gameObject.SetActive(true);
    }

    private void Aim()
    {
        directionArrow.Aim(GetAimPosition());
    }
    
    private void SetMotionToPlayer()
    {
        directionArrow.gameObject.SetActive(false);
        _playerMotion.Kill();
        
        _playerMotion = transform.DOMove(GetAimPosition(), motionDuration);
    }

    private Vector3 GetAimPosition()
    {
        var offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _startPosition;
        var targetPosition = transform.position + offset;
        targetPosition.z = transform.position.z;
        return targetPosition;
    }
    
    private void DisablePlayerControl()
    {
        _isControlEnabled = false;
        _playerMotion.Kill();
        directionArrow.gameObject.SetActive(false);
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
    
    
    void OnDrawGizmosSelected()
    {
        if (directionArrow.gameObject.activeSelf == false)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(GetAimPosition(), 0.5f);
    }
}
