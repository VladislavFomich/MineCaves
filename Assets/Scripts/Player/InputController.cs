using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Needs to be high up in the execution order
/// </summary>
public class InputController : MonoBehaviour
{
    public static InputController Instance { get; private set; }

    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private List<RectTransform> blockers = new List<RectTransform>();
    /// <summary>
    /// Basically if PointerDeltaPosition > swipeSensetivity then swipe detected.
    /// </summary>
    [SerializeField] private float swipeSensetivity = 10f;

    public bool GetPointerDown { get; private set; }
    public bool GetPointerDownOnScene { get; private set; }
    public bool GetPointerDownOnUI { get; private set; }

    public bool GetPointerUp { get; private set; }
    public bool GetPointerUpOnScene { get; private set; }
    public bool GetPointerUpOnUI { get; private set; }

    public bool GetPointerHeld { get; private set; }
    public bool GetPointerHeldOnScene { get; private set; }
    public bool GetPointerHeldOnUI { get; private set; }

    public bool GetTouchHeld { get; private set; }
    public bool GetTouchHeldOnScene { get; private set; }
    public bool GetTouchHeldOnUI { get; private set; }

    public bool GetMouseHeld { get; private set; }
    public bool GetMouseHeldOnScene { get; private set; }
    public bool GetMouseHeldOnUI { get; private set; }

    public bool GetSwipe { get; set; }
    public bool GetSwipeOnScene { get; set; }
    public bool GetSwipeOnUI { get; set; }

    /// <summary>
    /// Left bottom corner of the screen is 0, uppper right corner is Vector(width, height).
    /// </summary>
    public Vector3 PointerPosition => Input.touchCount > 0 ? (Vector3)Input.GetTouch(0).position : Input.mousePosition;
    /// <summary>
    /// Left bottom corner of the screen is 0, uppper right corner is 1.
    /// </summary>
    public Vector2 PointerNormalizedPosition
    {
        get
        {
            var position = PointerPosition;
            return new Vector2(position.x / Screen.width, position.y / Screen.height);
        }
    }
    public Vector2 PointerDeltaPosition => Input.touchCount > 0 ? Input.GetTouch(0).deltaPosition : mouseDeltaPosition;
    public Vector2 PointerNoralizedDeltaPosition
    {
        get
        {
            var deltaPosition = PointerDeltaPosition;
            deltaPosition.x /= Screen.width;
            deltaPosition.y /= Screen.height;
            deltaPosition.y *= Screen.height / Screen.width;
            return deltaPosition;
        }
    }
    public Vector3 ScreenToLocalCameraPosition(Vector3 screenPoint, float zDistance, Camera camera)
    {
        var worldPosition = camera.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, zDistance));
        var localCameraPosition = camera.transform.InverseTransformPoint(worldPosition);
        return localCameraPosition;
    }
    //public Vector3 TouchSpeed => PointerDeltaPosition / Time.deltaTime;
    public List<RectTransform> Blockers => blockers;

    private Vector3 lastMousePosition;
    private Vector2 mouseDeltaPosition;
    private bool isSwipePerformed;

    private void Awake()
    {
        Instance = this;
        if (eventSystem == null)
            eventSystem = EventSystem.current;
        DisableEventSystem();
    }

    private void Start()
    {
        InitializeEventSystem();
    }

    private void Update()
    {
        if (!IsBlocked())
        {
            ResetInput();
            DetectInput();
        }
        GetMouseDelta();
        DetectSwipe();
    }

    #region EventSystem
    private void InitializeEventSystem()
    {
        StartCoroutine(EnableEventSystemCoroutine());

        IEnumerator EnableEventSystemCoroutine()
        {
            yield return new WaitForSecondsRealtime(.1f);
            eventSystem.enabled = true;
        }
    }

    public void EnableEventSystem()
    {
        if (!eventSystem.enabled)
            eventSystem.enabled = true;
    }

    public void DisableEventSystem()
    {
        if (eventSystem.enabled)
            eventSystem.enabled = false;
    }
    #endregion

    #region Input
    private void DetectInput()
    {
        if (Input.touchCount > 0)
        {
            if (IsBlocked()) return;

            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                GetPointerDown = true;
                if (eventSystem.IsPointerOverGameObject())
                {
                    GetPointerDownOnUI = true;
                }
                else
                {
                    GetPointerDownOnScene = true;
                }
            }
            else if (touch.phase != TouchPhase.Ended)
            {
                GetTouchHeld = true;
                GetPointerHeld = true;
                if (eventSystem.IsPointerOverGameObject())
                {
                    GetTouchHeldOnUI = true;
                    GetPointerHeldOnUI = true;
                }
                else
                {
                    GetTouchHeldOnScene = true;
                    GetPointerHeldOnScene = true;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                GetPointerUp = true;
                if (eventSystem.IsPointerOverGameObject())
                {
                    GetPointerUpOnUI = true;
                }
                else
                {
                    GetPointerUpOnScene = true;
                }

                isSwipePerformed = true;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                GetPointerDown = true;
                if (eventSystem.IsPointerOverGameObject())
                {
                    GetPointerDownOnUI = true;
                }
                else
                {
                    GetPointerDownOnScene = true;
                }
            }
            else if (Input.GetMouseButton(0))
            {
                GetMouseHeld = true;
                GetPointerHeld = true;
                if (eventSystem.IsPointerOverGameObject())
                {
                    GetMouseHeldOnUI = true;
                    GetPointerHeldOnUI = true;
                }
                else
                {
                    GetMouseHeldOnScene = true;
                    GetPointerHeldOnScene = true;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                GetPointerUp = true;
                if (eventSystem.IsPointerOverGameObject())
                {
                    GetPointerUpOnUI = true;
                }
                else
                {
                    GetPointerUpOnScene = true;
                }

                isSwipePerformed = false;
            }
        }
    }

    private void GetMouseDelta()
    {
        var currentPosition = Input.mousePosition;
        mouseDeltaPosition = currentPosition - lastMousePosition;
        lastMousePosition = currentPosition;
    }

    private void DetectSwipe()
    {
        if (!isSwipePerformed & GetPointerHeld && PointerDeltaPosition.sqrMagnitude > swipeSensetivity * swipeSensetivity)
        {
            if (GetPointerHeldOnScene)
                GetSwipeOnScene = true;
            else if (GetPointerHeldOnUI)
                GetSwipeOnUI = true;
            //else // if (GetPointerHeld)
            GetSwipe = true;
            isSwipePerformed = true;
        }
    }

    private void ResetInput()
    {
        GetPointerDown = false;
        GetPointerDownOnUI = false;
        GetPointerDownOnScene = false;

        GetPointerUp = false;
        GetPointerUpOnUI = false;
        GetPointerUpOnScene = false;

        GetPointerHeld = false;
        GetPointerHeldOnUI = false;
        GetPointerHeldOnScene = false;

        GetTouchHeld = false;
        GetTouchHeldOnScene = false;
        GetTouchHeldOnUI = false;

        GetMouseHeld = false;
        GetMouseHeldOnScene = false;
        GetMouseHeldOnUI = false;

        GetSwipe = false;
        GetSwipeOnScene = false;
        GetSwipeOnUI = false;
    }
    #endregion

    #region Blocking
    private bool IsBlocked()
    {
        if (blockers.Count == 0)
        {
            EnableEventSystem();
            return false;
        }

        var pointer = new PointerEventData(eventSystem)
        {
            position = PointerPosition
        };
        var raycastResults = new List<RaycastResult>();
        eventSystem.RaycastAll(pointer, raycastResults);

        for (int i = 0; i < raycastResults.Count; i++)
        {
            for (int j = 0; j < blockers.Count; j++)
            {
                if (blockers[j].gameObject == raycastResults[i].gameObject)
                {
                    EnableEventSystem();
                    return false;
                }
            }
        }

        DisableEventSystem();
        return true;
    }

    /// <summary>
    /// Add element to the list of elements that you will only be able to interact with.
    /// </summary>
    /// <param name="blocker">Any UI element.</param>
    public void AddBlocker(RectTransform blocker)
    {
        blockers.Add(blocker);
    }

    /// <summary>
    /// Set element that you only will be able to interact with.
    /// </summary>
    /// <param name="blocker">Any UI element.</param>
    public void SetBlocker(RectTransform blocker)
    {
        blockers.Clear();
        blockers.Add(blocker);
    }

    /// <summary>
    /// Make all UI elements interactable.
    /// </summary>
    public void RemoveAllBlockers()
    {
        blockers.Clear();
    }
    #endregion

}
