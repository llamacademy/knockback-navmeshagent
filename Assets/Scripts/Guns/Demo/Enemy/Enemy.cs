using System;
using UnityEngine;

namespace LlamAcademy.Guns.Demo.Enemy
{
    [DisallowMultipleComponent]
    public class Enemy : MonoBehaviour
    {
        public EnemyHealth Health;
        public EnemyMovement Movement;
        public EnemyPainResponse PainResponse;
        public EnemyPlayerSensor PlayerSensor;

        private void Start()
        {
            Health.OnTakeDamage += PainResponse.HandlePain;
            Health.OnDeath += Die;
            PlayerSensor.OnPlayerEnter += Movement.OnSeePlayer;
            PlayerSensor.OnPlayerExit += Movement.OnLosePlayer;
        }

        private void Die(Vector3 Position)
        {
            Movement.StopMoving();
            PainResponse.HandleDeath();
        }
    }
}