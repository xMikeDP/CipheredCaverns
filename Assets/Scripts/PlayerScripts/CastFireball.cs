using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CastFireball : MonoBehaviour
{
    public GameObject fireballPrefab;
    private Vector3 destination;

    private PlayerController pc;
    //public float castCooldown = 0.5f;

    private float timer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > timer && pc.canCastFireballs) {
            timer = Time.time + pc.GetCastCooldown();
            Cast();
        }    
    }

    private void Cast() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            destination = hit.point;
        }
        else {
            destination = ray.GetPoint(100);
        }

        CreateFireball();
    }

    private void CreateFireball() {
        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        fireball.GetComponent<Rigidbody>().AddForce((destination - transform.position).normalized * 50, ForceMode.Impulse);
    }
}
