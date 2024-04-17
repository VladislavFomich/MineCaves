using DG.Tweening;
using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class ResourceAnimationManager : Singleton<ResourceAnimationManager>
{
    [SerializeField] private GameObject sprite;
    [SerializeField] private Transform spriteParent;
    [SerializeField] private ResourceCanvas resourceCanvas;
    [SerializeField] private float spawnTime = 0.1f;
    [SerializeField] private float animationDuration = 2;
    [SerializeField] private int particleCountMultiplayer = 4;
    [SerializeField] private ParticleSystem stoneHitParticle;
    [SerializeField] private ParticleSystem stoneDestroyParticle;
    [SerializeField] private ParticleSystem treeHitParticle;
    [SerializeField] private ParticleSystem treeDestroyParticle;
    private Camera _camera;

    public enum ParticleType
    {
        Hit,
        Destroy
    }

    private void Awake()
    {
        _camera = Camera.main;
        stoneHitParticle.maxParticles *= particleCountMultiplayer;
        stoneDestroyParticle.maxParticles *= particleCountMultiplayer;
        treeHitParticle.maxParticles *= particleCountMultiplayer;
        treeDestroyParticle.maxParticles *= particleCountMultiplayer;
    }


    public void TakeResource(int resourceCount, ResourceManager.ResourceType type, Vector3 spawnTransform)
    {
        StartCoroutine(TakeResourceCourutine(resourceCount, type, spawnTransform));
    }

    private IEnumerator TakeResourceCourutine(int resourceCount, ResourceManager.ResourceType type, Vector3 spawnTransform)
    {
        while (resourceCount > 0)
        {
            resourceCount--;
            var obj = Instantiate(sprite, spriteParent);
            obj.transform.position = _camera.WorldToScreenPoint(spawnTransform);
            obj.GetComponent<Image>().sprite = ResourceManager.Instance.GetScriptableResource(type).Icon;
            obj.transform.DOMove(resourceCanvas.GetImage(type).transform.position, animationDuration).OnComplete(() =>
            {
                Destroy(obj);
            });
            yield return new WaitForSeconds(spawnTime);
        }
    }

    public void SpendResource(int resourceCount, ResourceManager.ResourceType type, Vector3 spawnTransform)
    {
        StartCoroutine(SpendResourceCourutine(resourceCount, type, spawnTransform));
    }

    private IEnumerator SpendResourceCourutine(int resourceCount, ResourceManager.ResourceType type, Vector3 spawnTransform)
    {
        while (resourceCount > 0)
        {
            resourceCount--;
            var obj = Instantiate(sprite, spriteParent);
            obj.transform.position = resourceCanvas.GetImage(type).transform.position;
            obj.GetComponent<Image>().sprite = ResourceManager.Instance.GetScriptableResource(type).Icon;
            // Используем DOTween для анимации перемещения и изменения размера
            obj.transform.DOMove(_camera.WorldToScreenPoint(spawnTransform), animationDuration).OnComplete(() =>
            {
                // После завершения анимации перемещения, уничтожаем объект
                Destroy(obj);
            });
            yield return new WaitForSeconds(spawnTime);
        }
    }

    public void PlayParticle(Vector3 position, ParticleType particleType, ResourceManager.ResourceType resourceType)
    {
        if (resourceType != ResourceManager.ResourceType.Tree)
        {
            if (particleType == ParticleType.Hit)
            {
                stoneHitParticle.transform.position = new Vector3(position.x, position.y + 3, position.z);
                stoneHitParticle.gameObject.SetActive(true);
                stoneHitParticle.Emit(stoneHitParticle.maxParticles / particleCountMultiplayer);
            }
            else if (particleType == ParticleType.Destroy)
            {

                stoneDestroyParticle.transform.position = new Vector3(position.x, position.y + 3, position.z);
                stoneDestroyParticle.gameObject.SetActive(true);
                stoneDestroyParticle.Emit(stoneDestroyParticle.maxParticles / particleCountMultiplayer);
            }
        }


        if (resourceType == ResourceManager.ResourceType.Tree)
        {
            if (particleType == ParticleType.Hit)
            {
                treeHitParticle.transform.position = position;
                treeHitParticle.gameObject.SetActive(true);
                treeHitParticle.Emit(treeHitParticle.maxParticles / particleCountMultiplayer);
            }
            else if (particleType == ParticleType.Destroy)
            {

                treeDestroyParticle.transform.position = position;
                treeDestroyParticle.gameObject.SetActive(true);
                treeDestroyParticle.Emit(treeDestroyParticle.maxParticles / particleCountMultiplayer);
            }
        }
    }
}
