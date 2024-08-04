using UnityEngine;
using DG.Tweening;

public class PlayerThrowableBase : MonoBehaviour
{
    public BaseBomb throwablePrefab; 
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
            ShowPreview();
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            ThrowObject();
        }
    }

    void ShowPreview()
    {
        if (previewObject == null)
        {
            previewObject = Instantiate(previewPrefab, transform.position, Quaternion.identity);
        }

        // �nizleme nesnesini oyuncunun y�n�ne g�re g�ncelleyin
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        previewObject.transform.position = mousePosition;
        previewObject.transform.localScale = new Vector3(throwablePrefab.radius, throwablePrefab.radius, throwablePrefab.radius);
    }

    void ThrowObject()
    {
        if (previewObject != null)
        {
            // F�rlat�lacak nesneyi olu�turun
            BaseBomb throwableObject = Instantiate(throwablePrefab, transform.position, Quaternion.identity);

            // Ses efekti �al
            if (throwSound != null)
            {
                audioSource.PlayOneShot(throwSound);
            }

            // Par�ac�k efekti olu�tur
            if (throwEffect != null)
            {
                Instantiate(throwEffect, transform.position, Quaternion.identity);
            }

            // Yukar�ya do�ru ve sonra yere do�ru parabolik hareket i�in DoTween kullan�n
            Vector3 targetPosition = previewObject.transform.position;
            Vector3 peakPosition = (transform.position + targetPosition) / 2 + Vector3.up * throwHeight;

            Sequence throwSequence = DOTween.Sequence();
            throwSequence.Append(throwableObject.transform.DOJump(targetPosition, throwHeight, 1, throwDuration).SetEase(Ease.Linear));
            throwSequence.Join(throwableObject.transform.DORotate(new Vector3(0, 0, rotationSpeed), throwDuration, RotateMode.FastBeyond360));

            // �nizleme nesnesini yok edin
            Destroy(previewObject);
            previewObject = null;
        }
    }
}
