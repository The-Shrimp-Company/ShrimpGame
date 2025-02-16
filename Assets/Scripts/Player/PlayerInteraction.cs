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

    // Start is called before the first frame update
    void Start()
    {
        lookCheck = GetComponentInChildren<CameraLookCheck>();
        _camera = GetComponentInChildren<Camera>();
        _input = GetComponent<PlayerInput>();
    }

    public void OnPlayerClick()
    {
        GameObject target = lookCheck.LookCheck(5, "Tanks");

        if (target != null)
        {

            TankController tankController = target.GetComponent<TankController>();


            _camera.transform.position = tankController.GetCam().transform.position;
            _camera.transform.rotation = tankController.GetCam().transform.rotation;

            _input.SwitchCurrentActionMap("TankView");

            tankController.FocusTank();
        }
    }

    public void OnExitView()
    {
        _camera.transform.localPosition = Vector3.up / 2;
        _input.SwitchCurrentActionMap("Move");
        UIManager.instance.ChangeFocus();
    }
}
