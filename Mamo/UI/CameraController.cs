using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    [SerializeField] private JoyPadInputManager joy;
    [System.Serializable]
    public class positionSettings
    {
        public Vector3 offset = new Vector3(0, 3.4f, 0);
        public float lookSmoth = 100f;
        public float distanceFromTarget=-8f;
        public float zoomSmooth = 10f;
        public float maxZoom = -2f, minZoon = -15f;
    }

    [System.Serializable]

    public class orbitSettings
    {
        public float xRotation=-20f;
        public float yRotation=-180f;
        public float maxXrot = -5, minXrot = -85f;
        public float vOrbitSmooth = 150, hOrbitSmooth = 150;
    }
    [System.Serializable]

    public class inputSettings
    {
        public string horSnap="HorSnap", orbitHorizontal="OrbitHorizontal", orbiTVertical="OrbitVertical", zoom="Zoom";
    }

    public positionSettings position = new positionSettings();
    public orbitSettings orbit = new orbitSettings();
    public inputSettings _input = new inputSettings();
    Vector3 destination = Vector3.zero;
    Vector3 targetPos = Vector3.zero; 
    public PlayerController playerController;
    float vInput, hInput, zoomInput, snapInput;

    
  private void Start()
    {
        setCameraTarget(target);
        targetPos = target.position + position.offset;
        destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation, 0) * -Vector3.forward * position.distanceFromTarget;
        destination += targetPos;
        transform.position = destination;
        
    }
    private void Update()
    {
        getInput();
        zoomIn();
        orbitTARGET();
    }
    private void LateUpdate()
    {
        moveToTarget();
        lookAtTarget();
    }
    void setCameraTarget(Transform _target)
    {
        target = _target;
        if (target != null)
        {
            target.GetComponent<PlayerController>();
        }
        else
        {
            Debug.Log("target don't have playerController");
        } 
        
    }
    void getInput() { vInput = Input.GetAxisRaw(_input.orbiTVertical);
        hInput = Input.GetAxisRaw(_input.orbitHorizontal);
        zoomInput = Input.GetAxisRaw(_input.zoom);
        snapInput = Input.GetAxisRaw(_input.horSnap);
    }
    void zoomIn()
    {
        position.distanceFromTarget += zoomInput * position.zoomSmooth*Time.deltaTime;
        if (position.distanceFromTarget > position.maxZoom)
        {
            position.distanceFromTarget = position.maxZoom;
        }
        if (position.distanceFromTarget < position.minZoon)
        {
            position.distanceFromTarget = position.minZoon;
        }
    }
    void orbitTARGET()
    {
        if (snapInput > 0)
        {
            orbit.yRotation = -180;
        }
        orbit.xRotation += -vInput * orbit.vOrbitSmooth * Time.deltaTime;
        orbit.yRotation += -hInput * orbit.hOrbitSmooth * Time.deltaTime;
        if (orbit.xRotation > orbit.maxXrot)
        {
            orbit.xRotation = orbit.maxXrot;
        }
        if (orbit.xRotation < orbit.minXrot) 
        {
            orbit.xRotation = orbit.minXrot;
        }
    }
    void moveToTarget()
    {
        targetPos = target.position + position.offset;
        destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation+target.eulerAngles.y, 0) * -Vector3.forward * position.distanceFromTarget;
        destination += targetPos;
        transform.position = destination;
    }
    void lookAtTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, position.lookSmoth*Time.deltaTime);


    }

    //assomiglia a slerp
    // float eulerYangle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y,ref rotateVelocity,lookSmoth);
    //transform.rotation = Quaternion.Euler(transform.eulerAngles.x, eulerYangle, 0);
}
