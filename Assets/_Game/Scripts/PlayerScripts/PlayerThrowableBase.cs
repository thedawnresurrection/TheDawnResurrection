using UnityEngine;
using DG.Tweening;

public class PlayerThrowableBase : MonoBehaviour
{
    public BaseBomb grandePrefab,molotovPrefab,flashBombPrefab; 
    public GameObject previewPrefab; 
    public float throwHeight = 2f; 
    public float throwDuration = 1f; 
    public float rotationSpeed = 360f; 
    public AudioClip throwSound; 
    public ParticleSystem throwEffect; 

    private GameObject previewObject; 
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
            ShowPreview(grandePrefab);
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            ThrowObject(grandePrefab);
        }

        if (Input.GetKey(KeyCode.H))
        {
            ShowPreview(molotovPrefab);
        }
        if (Input.GetKeyUp(KeyCode.H))
        {
            ThrowObject(molotovPrefab);
        }

        if (Input.GetKey(KeyCode.J))
        {
            ShowPreview(flashBombPrefab);
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            ThrowObject(flashBombPrefab);
        }
    }

    void ShowPreview(BaseBomb baseBombPrefab)
    {
        if (previewObject == null)
        {
            previewObject = Instantiate(previewPrefab, transform.position, Quaternion.identity);
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        previewObject.transform.position = mousePosition;
        previewObject.transform.localScale = new Vector3(baseBombPrefab.radius, baseBombPrefab.radius, baseBombPrefab.radius);
    }

    void ThrowObject(BaseBomb baseBomb)
    {
        if (previewObject != null)
        {
            BaseBomb throwableObject = Instantiate(baseBomb, transform.position, Quaternion.identity);
            if (throwSound != null)
            {
                audioSource.PlayOneShot(throwSound);
            }

            if (throwEffect != null)
            {
                Instantiate(throwEffect, transform.position, Quaternion.identity);
            }

            Vector3 targetPosition = previewObject.transform.position;
            Vector3 peakPosition = (transform.position + targetPosition) / 2 + Vector3.up * throwHeight;

            Sequence throwSequence = DOTween.Sequence();
            throwSequence.Append(throwableObject.transform.DOJump(targetPosition, throwHeight, 1, throwDuration).SetEase(Ease.Linear));
            throwSequence.Join(throwableObject.transform.DORotate(new Vector3(0, 0, rotationSpeed), throwDuration, RotateMode.FastBeyond360));

            Destroy(previewObject);
            previewObject = null;
        }
    }
}
