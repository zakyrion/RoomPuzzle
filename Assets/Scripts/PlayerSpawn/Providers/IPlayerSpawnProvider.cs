using UnityEngine;

namespace PlayerSpawn.Providers
{
    public interface IPlayerSpawnProvider
    {
        public Transform GetPlayerSpawnPoint();
    }
}
