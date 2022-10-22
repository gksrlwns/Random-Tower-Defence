using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool doMovement = true;
    private Vector3 pos;
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 30f;
    public float minX = -5f;
    public float maxX = 50f;
    public float minZ = -30f;
    public float maxZ = 25f;
    
    

    private void Start()
    {
        pos = transform.position;
        
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //    doMovement = !doMovement;

        //if (!doMovement)
        //    return;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        transform.position = pos;

        //if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        //{
        //    //transform.Translate(Vector3.forward * panSpeed * Time.deltaTime,Space.World);
        //    pos.z += panSpeed * Time.deltaTime;
        //}
        //if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        //{
        //    //transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        //    pos.z -= panSpeed * Time.deltaTime;
        //}
        //if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        //{
        //    //transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        //    pos.x += panSpeed * Time.deltaTime;
        //}
        //if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        //{
        //    //transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        //    pos.x -= panSpeed * Time.deltaTime;
        //}

        //float scroll = Input.GetAxis("Mouse ScrollWheel");
        //pos.y -= scroll * 100 * scrollSpeed * Time.deltaTime;

        
    }
}
