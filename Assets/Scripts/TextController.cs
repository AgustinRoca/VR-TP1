using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TextController : MonoBehaviour
{
    public GameObject cube;
    private GameObject[] rings;
    private bool[] passedRings;
    public TextMesh text;
    private int points;
    private const float DISTANCE_FROM_CAMERA = 10f;
    private const float WIDTH = 3f;
    private const float DEPTH = 0.2f;
    private const float LEFT = (-WIDTH + DEPTH)/2;
    private const float DOWN = (-WIDTH/2 + DEPTH)/2;
    private const float UP = (WIDTH - DEPTH)/2;
    private const float RIGHT = (WIDTH - DEPTH)/2;
    private const float EPSILON = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        rings = GameObject.FindGameObjectsWithTag("Ring");
        passedRings = new bool[rings.Length];
        points = 0;
        text.text = "Points: " + points;

    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i<rings.Length; i++){
            if(!passedRings[i] && passThroughRing(cube, rings[i])){
                points++;
                passedRings[i] = true;
                rings[i].GetComponent<MeshRenderer>().material.color = Color.blue;
                foreach(var child in rings[i].GetComponentsInChildren<Renderer>()){
                    child.material.color = Color.blue;
                }
            }
        }
        transform.position = Camera.main.transform.position + new Vector3(DISTANCE_FROM_CAMERA*Camera.main.transform.forward.x, DISTANCE_FROM_CAMERA*Camera.main.transform.forward.y, DISTANCE_FROM_CAMERA*Camera.main.transform.forward.z);
        transform.LookAt(Camera.main.transform);
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);        
        text.text = "Points: " + points;
        
    }

    static bool passThroughRing(GameObject cube, GameObject ring){
        float left = -(WIDTH - DEPTH)/2;
        float right = (WIDTH - DEPTH)/2;
        float up = DEPTH + WIDTH;
        float down = DEPTH;
        bool isInsideX = (ring.transform.position.x + left < cube.transform.position.x) && (ring.transform.position.x + right > cube.transform.position.x);
        bool isInsideY = (ring.transform.position.y + down < cube.transform.position.y) && (ring.transform.position.y + up > cube.transform.position.y);
        bool isInsideZ = Mathf.Abs(ring.transform.position.z - cube.transform.position.z) < EPSILON;
        return  isInsideX && isInsideY && isInsideZ;
    }
}
