using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSo;
    public KitchenObjectSO KitchenObjectSo => kitchenObjectSo;
    [SerializeField] private IKitchenObjectParent kitchenObjectParent;
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenParent)
    {
        if(kitchenObjectParent != null)
        {
            kitchenObjectParent.ClearKitchenObject();
        }
        kitchenObjectParent = kitchenParent;

        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("Counter already has a KitchenObjectParent");
        }
        kitchenObjectParent.SetKitchenObject(this);
        transform.parent = this.kitchenObjectParent.GetKitchenObjectFollowTranform();
        transform.localPosition = Vector3.zero;
    }



    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
        
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSo, IKitchenObjectParent parent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSo.prefab);
        KitchenObject kitchen = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchen.SetKitchenObjectParent(parent);
        return kitchen;
    }
}
