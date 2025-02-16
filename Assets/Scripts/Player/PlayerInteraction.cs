using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private ShelfSpawn shelves;

    private CameraLookCheck lookCheck;
    private Camera _camera;
    private PlayerInput _input;
    private GameObject _tankView;
    private Vector2 press;
    private bool _pressed;

    // Start is called before the first frame update
    void Start()
    {
        lookCheck = GetComponentInChildren<CameraLookCheck>();
        _camera = GetComponentInChildren<Camera>();
        _input = GetComponent<PlayerInput>();
    }

    /// <summary>
    /// Called by the input manager
    /// Can only be called from the move action map, used to interact with tanks
    /// Future functionality that requires the player click on something should take place
    /// here as well, although lookCheck should be refactored to allow more options
    /// if that happens.
    /// </summary>
    public void OnPlayerClick()
    {
        GameObject target = lookCheck.LookCheck(5, "Tanks");

        if (target != null)
        {
            SetTankFocus(target.GetComponent<TankController>());
        }
    }

    public void SetTankFocus(TankController tankController)
    {
        _camera.transform.position = tankController.GetCam().transform.position;
        _camera.transform.rotation = tankController.GetCam().transform.rotation;

        _input.SwitchCurrentActionMap("TankView");

        tankController.SetTankView();
        _tankView = tankController.gameObject;
    }

    /// <summary>
    /// Can only be called from tank view action map
    /// Will switch the focus from one tank to the next in the direction clicked
    /// </summary>
    /// <param name="Key"></param>
    public void OnSwitchTank(InputValue Key)
    {
        Debug.Log(Key.Get<Vector2>());
        if (Key.Get<Vector2>().normalized != press)
        {
            press = Key.Get<Vector2>().normalized;
            if (press != Vector2.zero || press != null)
            {
                RaycastHit hit;
                Debug.DrawRay(_tankView.GetComponent<Collider>().bounds.center, _tankView.transform.TransformDirection(new Vector3(-press.x, press.y, 0) * 1), Color.red, 10000f);
                if (Physics.Raycast(_tankView.GetComponent<Collider>().bounds.center, _tankView.transform.TransformDirection(new Vector3(-press.x, press.y, 0)), out hit, 1f, layerMask: LayerMask.GetMask("Tanks")))
                {
                    //Debug.Log("YEah");
                    SetTankFocus(hit.transform.GetComponent<TankController>());
                }
            }
            else
            {
                //Debug.Log("HJEHFSDHFSD");
            }
        }
    }

    /// <summary>
    /// Can only be called from the tank view action map
    /// Will leave tank view
    /// </summary>
    public void OnExitView()
    {
        _camera.transform.localPosition = Vector3.up / 2;
        _input.SwitchCurrentActionMap("Move");
        UIManager.instance.ChangeFocus();
    }
}
