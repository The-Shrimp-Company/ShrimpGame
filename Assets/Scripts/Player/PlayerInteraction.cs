using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private ShelfSpawn shelves;

    [SerializeField]
    private GameObject inventory;

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
            if (target.GetComponent<TankController>())
            {
                SetTankFocus(target.GetComponent<TankController>());
            }
            else
            {
                GameObject invenScreen = UIManager.instance.GetCanvas().GetComponent<MainCanvas>().RaiseScreen(inventory);
                invenScreen.GetComponentInChildren<InventoryContent>().TankAssignment(target);

            }
        }
    }

    public void SetTankFocus(TankController tankController)
    {
        if (tankController != null)
        {
            _camera.transform.position = tankController.GetCam().transform.position;
            _camera.transform.rotation = tankController.GetCam().transform.rotation;

            Debug.Log("GetHere");

            _input.SwitchCurrentActionMap("TankView");

            tankController.FocusTank();
            _tankView = tankController.gameObject;
        }
        else Debug.LogError("Cannot find tank to focus");
    }

    /// <summary>
    /// Can only be called from tank view action map
    /// Will switch the focus from one tank to the next in the direction clicked
    /// </summary>
    /// <param name="Key"></param>
    public void OnSwitchTank(InputValue Key)
    {
        if (Key.Get<Vector2>().normalized != press)
        {
            press = Key.Get<Vector2>().normalized;
            if (press != Vector2.zero || press != null)
            {
                RaycastHit hit;
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
        Vector3 v3 = _tankView.GetComponent<TankController>().GetCam().transform.position;
        transform.position = new Vector3(v3.x, transform.position.y, v3.z);
        _camera.transform.localPosition = Vector3.up / 2;
        _input.SwitchCurrentActionMap("Move");
        UIManager.instance.ChangeFocus();
    }

    /// <summary>
    /// Should only be called from the tank view action map. Used to interact with shrimp.
    /// </summary>
    /// <param name="point"></param>
    public void OnTankClick(InputValue point)
    {
        UIManager.instance.GetFocus().GetComponent<TankViewScript>().MouseClick(Mouse.current.position.value, point.isPressed);
    }
}
