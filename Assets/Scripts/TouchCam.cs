using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCam : MonoBehaviour
{
    private Vector3 pos;
    public float panSpeed = 30f;
    public float minY = 10f;
    public float maxY = 30f;
    public float minX = -5f;
    public float maxX = 50f;
    public float minZ = -30f;
    public float maxZ = 25f;
    // 모바일
    public float moveSpeed;
    public Transform cam;

    float preveDistance = 0f;
    Vector2 prevPos = Vector2.zero;
    
    void Start()
    {
        cam = Camera.main.transform;
        pos = cam.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag()
    {
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        cam.position = pos;
        //모바일
        if (Input.touchCount == 1)
        {
            if (prevPos == Vector2.zero)
            {
                prevPos = Input.GetTouch(0).position;
                return;
            }
            Vector2 dir = (Input.GetTouch(0).position - prevPos).normalized;
            Vector3 vec = new Vector3(dir.x, 0, dir.y);

            pos -= vec * panSpeed * Time.deltaTime;
            prevPos = Input.GetTouch(0).position;
        }
        else if (Input.touchCount == 2)
        {
            if (preveDistance == 0)
            {
                preveDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                return;
            }
            float curDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            float move = preveDistance - curDistance;

            pos = cam.position;

            if (move < 0) pos.y -= panSpeed * Time.deltaTime;
            else if (move > 0) pos.y += panSpeed * Time.deltaTime;

            cam.position = pos;
            preveDistance = curDistance;
        }
    }
    public void ExitDrag()
    {
        prevPos = Vector2.zero;
        preveDistance = 0f;
    }
}
