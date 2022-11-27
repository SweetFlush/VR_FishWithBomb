using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public FishManager fishManager;
    public Fish fish;

    public int catchedMonsterFish = 0;

    public OVRInput.Button castingButton;

    public AudioClip castingSound;
    public AudioClip hitSound;
    public AudioClip fishHitSound;
    public AudioClip catchingSound;
    public AudioClip failSound;

    public GameObject rodString;
    public Transform rodStringEnd;
    public MeshRenderer rodMesh;
    public TextMeshPro healthText;

    public int health = 100;


    private OVRGrabbable grabbable;
    private AudioSource audioSource;

    private bool isFishing = false;
    private bool isHit = false;
    private bool isHooked = false;
    private bool isFighting = false;


    // Start is called before the first frame update
    void Start()
    {
        grabbable = GetComponent<OVRGrabbable>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(grabbable.isGrabbed && OVRInput.GetDown(castingButton, grabbable.grabbedBy.GetController()) && !isFishing)
        {
            Cast();
        }
        float red = 1f - (health / 100f);   //피가 0에 가까울수록 씨뻘개짐
        float green = health / 100f; //피가 만땅일수록 초록색

        rodMesh.material.color = new Color(red, green, 0.5f);
        healthText.text = health.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "HitCollider" && isHit && !isFighting)
        {
            Hook();
        }
    }

    //캐스팅
    private void Cast()
    {
        isFishing = true;
        audioSource.PlayOneShot(castingSound);
        StartCoroutine(Fishing());
    }


    //이거 뭐였더라 위로 드는거
    private void Hook()
    {
        isHit = false;
        isFighting = true;
        StartCoroutine(VibrateController(0.1f, 1f, 0.2f, grabbable.grabbedBy.GetController()));
        audioSource.PlayOneShot(hitSound);
        audioSource.PlayOneShot(fishHitSound);
        fish = fishManager.SpawnFish();
        StartCoroutine(Fight());
    }

    //릴 감기
    public void WindReel()
    {
        if(isFighting && fish)
        {
            StartCoroutine(VibrateController(0.05f, 0.3f, 0.2f, grabbable.grabbedBy.GetController()));
            fish.health -= 5;
            health += 2;
            if (fish.health <= 0)
                FishingSuccess();
        }

    }

    //지속뎀
    private IEnumerator Fight()
    {
        while(isFighting)
        {
            if (health <= 0)
                FishingFailed();

            health -= fish.damage;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator Fishing()
    {
        float r = Random.Range(5f, 30f);
        yield return new WaitForSeconds(r);

        isHit = true;
        StartCoroutine(VibrateController(0.5f, 0.8f, 0.5f, grabbable.grabbedBy.GetController()));
    }

    public void FishingSuccess()
    {
        audioSource.PlayOneShot(catchingSound);
        if (fish.isMonster)
            catchedMonsterFish += 1;
        fish.Fished();
        FinishFishing();
    }

    public void FishingFailed()
    {
        audioSource.PlayOneShot(failSound);
        FinishFishing();
        fish.Run();
    }

    private void FinishFishing()
    {
        isFighting = false;
        isFishing = false;
        isHit = false;
        isHooked = false;
        health = 100;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="waitTime">진동 지속시간</param>
    /// <param name="frequency">(0 ~ 1) 진동의 주파수</param>
    /// <param name="amplitude">(0 ~ 1) 진동의 진폭</param>
    /// <param name="controller">대상 컨트롤러</param>
    /// <returns></returns>
    private IEnumerator VibrateController(float waitTime, float frequency, float amplitude, OVRInput.Controller controller)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, controller);
        yield return new WaitForSeconds(waitTime);
        OVRInput.SetControllerVibration(0, 0, controller);
    }
}
