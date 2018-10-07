using UnityEngine;
using System.Collections;

public class Trajectory : MonoBehaviour
{
    // launch variables
    [SerializeField] private Transform TargetObject;
    [Range(1.0f, 6.0f)] public float TargetRadius;
    [Range(20.0f, 75.0f)] public float LaunchAngle;

    // state
    private bool bTargetReady;

    // cache
    public Rigidbody2D rigid;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        bTargetReady = true;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }


    // Sets a random target around the object based on the TargetRadius
    void SetNewTarget() {
        bTargetReady = true;
    }

    // resets the projectile to its initial position
    void ResetToInitialState()
    {
        rigid.velocity = Vector3.zero;
        this.transform.SetPositionAndRotation(initialPosition, initialRotation);
        bTargetReady = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (bTargetReady)
            {
                Launch();
            }
            else
            {
                ResetToInitialState();
                SetNewTarget();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetToInitialState();
        }
    }

    void Launch()
    {
        float s = 300f;
        float G = Physics2D.gravity.y;
        Debug.Log(G);

        var targetPosition = TargetObject.transform.position;
        var myPosition = transform.position;
        var x = (targetPosition - myPosition).magnitude;
        var y = targetPosition.y - myPosition.y;
        var v = s;
        var sqrt = (v * v * v * v) - (G * (G * (x * x) + 2 * y * (v * v)));

        if (sqrt < 0) {
            Debug.Log("No Solution");
            return;
        }

        sqrt = Mathf.Sqrt(sqrt);
        var calculatedAnglePos = Mathf.Atan(((v * v) + sqrt) / (G * x));
        var calculatedAngleNeg = Mathf.Atan(((v * v) - sqrt) / (G * x));


        Debug.Log(calculatedAnglePos * Mathf.Rad2Deg);

        bTargetReady = false;
    }
}