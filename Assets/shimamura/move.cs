using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class move : MonoBehaviour
{
    [SerializeField] float speed = 1f;//‘¬“x
    [SerializeField] PlayerType player = PlayerType.P1;
    [SerializeField] Transform _rightBounds;
    [SerializeField] Transform _leftBounds;

    float mov;

    private Vector2 Pos;
    private enum PlayerType
    {
       P1,
       P2,
    };


    void Start()
    {
        Pos = transform.position;
    }


    void Update()
    {
        mov = 0;
        if (player == PlayerType.P1)
        {
            if (Input.GetKey(KeyCode.A)) mov = -1f;
            if (Input.GetKey(KeyCode.D)) mov = 1f;
        }
        else if (player == PlayerType.P2)
        {
            if (Input.GetKey(KeyCode.LeftArrow)) mov = -1f;
            if (Input.GetKey(KeyCode.RightArrow)) mov = 1f;
        }

        mov *= (speed * Time.deltaTime);
        //var mov = Input.GetAxis("Horizontal") * speed;

        if (mov >= 0 && Pos.x <= _rightBounds.position.x)
        {
            Pos.x += mov;
            this.transform.position = Pos;
        }
        if (mov <= 0 && Pos.x >= _leftBounds.position.x)
        {
            Pos.x += mov;
            this.transform.position = Pos;
        }

    }
}
