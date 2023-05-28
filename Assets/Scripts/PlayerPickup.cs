using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickup : MonoBehaviour
{
    // Update is called once per frame
    public float pickupDistance = 2f;
    public Transform camera;
    public Transform pickPoint;
    public LayerMask layer;
    private StarterAssetsInputs _input;
    private PlayerInput _playerInput;

    private PickableObject obj;

    private void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {

        if (_input.pickup){
            if(obj == null)
            {
                if(Physics.Raycast(camera.transform.position, camera.forward, out RaycastHit hit, pickupDistance)){
                    if(hit.transform.TryGetComponent(out obj)){
                        obj.Pick(pickPoint);
                        
                    }
                }

            }
            else
            {
                obj.Drop();
                obj = null;
            }

        }
        

    }
}
