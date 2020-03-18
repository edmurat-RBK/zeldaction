using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourantEau : MonoBehaviour
{
    public float waterSpeed;
    public string direction;

    public Vector2 movement;
 
    void Start()
    {
        switch (direction)
        {
            case("Bas"):
                movement = Vector2.down;
                break;

            case ("Haut"):
                movement = Vector2.up;
                break;

            case ("Droite"):
                movement = Vector2.right;
                break;

            case ("Gauche"):
                movement = Vector2.left;
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 28)
        {
            if(other.GetComponent<CubeBois>().canRedirect == true)
            {
                other.GetComponent<CubeBois>().canRedirect = false;
                other.GetComponent<Rigidbody2D>().velocity = (movement.normalized * waterSpeed * Time.fixedDeltaTime);
                other.GetComponent<CubeBois>().wichDirection = direction;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        other.GetComponent<CubeBois>().canRedirect = true;
    }
}
