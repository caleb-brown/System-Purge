using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public float movmentAmount = 10.0f;
    public float movementSpeed = 10.0f;
    bool dirRight = true;
    Vector3 initPos;

    void Awake()
    {
        initPos = transform.position;
    }

	// Update is called once per frame
	void Update ()
    {
        if (dirRight)
            transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
        else
            transform.Translate(-Vector2.right * movementSpeed * Time.deltaTime);

        if (transform.position.x >= initPos.x + movmentAmount)
            dirRight = false;
        if (transform.position.x <= initPos.x + -movmentAmount)
            dirRight = true;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            other.transform.parent = transform;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            other.transform.parent = null;
    }
}
