using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Rigidbody2D bulletRB;
    
    // Start is called before the first frame update
    void Start()
    {
         StartCoroutine("DestroyGo");
         //coroutine starts when it is instantiated and will be set inactive within 3 seconds
    }
    void Update()
    {//move forward in its local up direction (will move different directions when rotated)
        transform.Translate(Vector2.up * Time.deltaTime * 5f, Space.Self);
    }

     IEnumerator DestroyGo()
    {    
        yield return new WaitForSeconds(3f);
        this.gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
            this.gameObject.SetActive(false);
        }
    }
}
