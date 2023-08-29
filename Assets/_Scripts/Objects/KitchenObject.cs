using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public KitchenObjectSO KitchenObjectSO => kitchenObjectSO;
    [SerializeField] private IKitchenObjectParent kitchenObjectParent;
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if(this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        this.kitchenObjectParent = kitchenObjectParent;

        if (this.kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("Counter already has a KitchenObjectParent");
        }
        this.kitchenObjectParent.SetKitchenObject(this);
        transform.parent = this.kitchenObjectParent.GetKitchenObjectFollowTranform();
        transform.localPosition = Vector3.zero;
    }



    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
        
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent parent)
    {
        Transform KitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchen = KitchenObjectTransform.GetComponent<KitchenObject>();
        kitchen.SetKitchenObjectParent(parent);
        return kitchen;
    }
}
