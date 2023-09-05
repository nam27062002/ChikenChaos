using System;
using _Scripts.Objects;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Counter
{
    public class PlatesCounter : BaseCounter
    {
        private float spawnPlateTimer;
        private readonly float spawnPlateTimerMax = 1f;
        private int platesSpawnedAmount;
        private readonly int platesSpawnedAmountMax = 4;
        public event EventHandler OnPlateSpawned;
        public event EventHandler OnPlateRemoved;
        [SerializeField] private KitchenObjectSO kitchenObjectSo;
        private void Update()
        {
            if (platesSpawnedAmount > platesSpawnedAmountMax) return;
            spawnPlateTimer += Time.deltaTime;
            if (spawnPlateTimer < spawnPlateTimerMax) return;
            platesSpawnedAmount++;
            spawnPlateTimer = 0; 
            OnPlateSpawned?.Invoke(this,EventArgs.Empty);
        }

        public override void Interact(PlayerMovement player)
        {
            if (player.HasKitchenObject()) return;
            if(platesSpawnedAmount == 0) return;
            platesSpawnedAmount--;
            KitchenObject.SpawnKitchenObject(kitchenObjectSo,player);
            OnPlateRemoved?.Invoke(this,EventArgs.Empty);
        }
    }
}
