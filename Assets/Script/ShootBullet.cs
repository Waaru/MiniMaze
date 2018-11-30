using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour {

    GameObject prefab;
    public Transform BulletSpawn;
    bool CanFire = true;

    // Use this for initialization
    void Start () {
        prefab = Resources.Load("Bullet") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1") && CanFire) {
            CanFire = false;
            Fire();
            StartCoroutine(WaitForNext(0.5f));
        }
	}
    
    void Fire()
    {
        var projectile = Instantiate(prefab, BulletSpawn.position, BulletSpawn.rotation) as GameObject;
        //projectile.velocity = Camera.main.transform.forward * 40;
        projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * 15;
        Destroy(projectile, 6.0f);
    }

    public IEnumerator WaitForNext(float delayInSecs)
    {
        yield return new WaitForSeconds(delayInSecs);
        CanFire = true;
    }
}
