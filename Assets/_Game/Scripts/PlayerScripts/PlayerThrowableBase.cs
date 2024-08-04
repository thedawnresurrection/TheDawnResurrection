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

        // Önizleme nesnesini oyuncunun yönüne göre güncelleyin
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        previewObject.transform.position = mousePosition;
        previewObject.transform.localScale = new Vector3(throwablePrefab.radius, throwablePrefab.radius, throwablePrefab.radius);
    }

    void ThrowObject()
    {
        if (previewObject != null)
        {
            // Fýrlatýlacak nesneyi oluþturun
            BaseBomb throwableObject = Instantiate(throwablePrefab, transform.position, Quaternion.identity);

            // Ses efekti çal
            if (throwSound != null)
            {
                audioSource.PlayOneShot(throwSound);
            }

            // Parçacýk efekti oluþtur
            if (throwEffect != null)
            {
                Instantiate(throwEffect, transform.position, Quaternion.identity);
            }

            // Yukarýya doðru ve sonra yere doðru parabolik hareket için DoTween kullanýn
            Vector3 targetPosition = previewObject.transform.position;
            Vector3 peakPosition = (transform.position + targetPosition) / 2 + Vector3.up * throwHeight;

            Sequence throwSequence = DOTween.Sequence();
            throwSequence.Append(throwableObject.transform.DOJump(targetPosition, throwHeight, 1, throwDuration).SetEase(Ease.Linear));
            throwSequence.Join(throwableObject.transform.DORotate(new Vector3(0, 0, rotationSpeed), throwDuration, RotateMode.FastBeyond360));

            // Önizleme nesnesini yok edin
            Destroy(previewObject);
            previewObject = null;
        }
    }
}
