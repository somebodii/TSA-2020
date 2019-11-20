using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{

    public float movementSpeed = 0;
    public float rotationSpeed = 0;
    public bool useInputAxis = false;
    public string axisName = "";
    public LayerMask ground;


    public bool rightFace = true;
    private SpriteRenderer mySpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake() 
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float translation = 0;
        if(useInputAxis) {
            translation = Input.GetAxis(axisName) * movementSpeed * Time.deltaTime;
            transform.Translate(translation,0,0);
        }

        TurnIf(translation);
        if(!rightFace) mySpriteRenderer.flipX = true;
        else mySpriteRenderer.flipX = false; 
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
