using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FencingController : MonoBehaviour
{

    public float movementSpeed = 0;
    public string axisName = "";
    public LayerMask ground;
    public bool rightFace = true;
    public KeyCode left;
    public KeyCode right;
    public KeyCode hold;
    public KeyCode inch;
    private float multiplier = 0f;

    public Transform tf; 
    private int currentAction = 0; 
    public float lagTime = 0;
    public static float lean = 0;
    private bool grounded = true;
    private BoxCollider2D bc;
    private Rigidbody2D rb;
    private SpriteRenderer mySpriteRenderer;

    void Awake() 
    {
        bc = GetComponent<BoxCollider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        tf.Rotate(0,0,-tf.localEulerAngles.z + lean*10,Space.World);

        if(currentAction == 0) {
            leanControl();
        }

        lagTime--;
        if(lagTime <= 0) {
            lagTime = 0;
        }
               if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector2(Time.deltaTime * movementSpeed, 0f));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector2(-Time.deltaTime * movementSpeed, 0f));
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (grounded == true)
            {
                multiplier += 10f;
                grounded = false;
            }


        }
        if (grounded == false)
        {
            if (tf.position.y + Time.deltaTime * (float)multiplier >= -3.25f)
            {

                transform.Translate(new Vector2(0f, Time.deltaTime * (float)multiplier));
                multiplier -= 0.3f;
            }else
            {
                grounded = true;
                multiplier = 1f;
 
                tf.Translate(new Vector2(0f, -3.25f - tf.position.y));
            }
        }


   
        
    }

    public void leanControl() {
        if(lagTime < 1) {
            if(Input.GetKey(hold)) {
                if(Input.GetKey(left) || lean > 0 && !Input.GetKey(right)) {
                    lean -= .1f;
                    Debug.Log("LeanL");
                } else if(Input.GetKey(right) || lean < 0 && !Input.GetKey(left)) {
                    lean += .1f;
                    Debug.Log("LeanR");
                }
                
            } else {
                if(lean < 0) {
                    lean += .05f;
                } else if (lean > 0) {
                    lean -= .05f;
                }
            }
        if(lean < .1 && lean > -.1) {
            lean = 0f;
            Debug.Log("LeanReset 0");
        } else if(lean > 1) {
            lean = 1f;
            Debug.Log("LeanReset 1");
        } else if(lean < -1) {
            lean = -1f;
            Debug.Log("LeanReset -1");
        } 
        }
    }

    public void groundCheck() {
        if(!bc.IsTouchingLayers(ground)) grounded = false;
        else grounded = true;
    }

    public void Turn() {
        if(rightFace == true) rightFace = false;
        else rightFace = true;
    }

    public void TurnIf(float translation) {
        if(translation < 0 && rightFace) rightFace = false;
        if(translation > 0 && !rightFace) rightFace = true;
    }
}
