using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Sprite icon;
    public GameObject bullet;
    public float ammu = 30;

    [HideInInspector]
    public float ammuMax = 30;
    public float interval = 0.5f;

    float fireTime = 0;

    GameObject point;

    bool gunLoading;

    Vector3 initpos;

    AudioSource audio;
    void Awake()
    {
        ammuMax = ammu;
        point = this.transform.Find("GunMuzzle").gameObject;
        initpos = this.transform.localPosition;
        audio = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        this.fireTime -= Time.deltaTime;
        if( Input.GetMouseButton(0) && this.ammu > 0 && gunLoading == false && fireTime <=0  )
        {
            this.transform.Translate(new Vector3(0,0,-0.1f),Space.Self);
            this.fireTime = interval;
            this.ammu--;
            GameObject newbullet = Instantiate(bullet,point.transform.position,point.transform.rotation);
            if( this.ammu <= 0 )
            {
                this.gunLoading = true;
            }
            this.audio.PlayOneShot(newbullet.GetComponent<AudioSource>().clip);
        }

        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition,initpos,Time.deltaTime * 10);

        if( this.gunLoading )
        {
            this.ammu += this.ammuMax * Time.deltaTime;
            if( this.ammu >= this.ammuMax )
            {
                this.ammu = this.ammuMax;
                this.gunLoading = false;
            }
        }
    }
}
