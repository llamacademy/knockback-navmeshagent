using System.Collections;
using LlamAcademy.Guns.ImpactEffects;
using UnityEngine;
using UnityEngine.AI;

namespace LlamAcademy.Guns.Demo.Enemy
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody))]
    public class EnemyMovement : MonoBehaviour, ISlowable, IKnockbackable
    {
        private Animator Animator;
        [SerializeField] private float StillDelay = 1f;
        [Range(0.001f, 0.1f)] [SerializeField] private float StillThreshold = 0.05f;
        [SerializeField] private float MaxKnockbackTime = 0.5f;
        private LookAtIK LookAt;
        private NavMeshAgent Agent;
        private Rigidbody Rigidbody;

        private Coroutine SlowCoroutine;
        private Coroutine MoveCoroutine;
        private float BaseSpeed;
        private const string IsWalking = "IsWalking";
        private Transform Player;

        private static NavMeshTriangulation Triangulation;

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            Agent = GetComponent<NavMeshAgent>();
            LookAt = GetComponent<LookAtIK>();
            Rigidbody = GetComponent<Rigidbody>();
            if (Triangulation.vertices == null || Triangulation.vertices.Length == 0)
            {
                Triangulation = NavMesh.CalculateTriangulation();
            }
            
            BaseSpeed = Agent.speed;
        }

        private void Start()
        {
            MoveCoroutine = StartCoroutine(Roam());
            BaseSpeed = Agent.speed;
        }

        private void Update()
        {
            Animator.SetBool(IsWalking, Agent.velocity.magnitude > 0.01f);
            if (LookAt != null)
            {
                LookAt.lookAtTargetPosition = Agent.steeringTarget + transform.forward;
            }
        }

        private IEnumerator Roam()
        {
            WaitForSeconds wait = new WaitForSeconds(StillDelay);

            while (enabled)
            {
                int index = Random.Range(1, Triangulation.vertices.Length);
                Agent.SetDestination(
                    Vector3.Lerp(
                        Triangulation.vertices[index - 1],
                        Triangulation.vertices[index],
                        Random.value
                    )
                );
                yield return new WaitUntil(() => Agent.remainingDistance <= Agent.stoppingDistance);
                yield return wait;
            }
        }

        public void StopMoving()
        {
            if (MoveCoroutine != null)
            {
                StopCoroutine(MoveCoroutine);
            }
            Agent.isStopped = true;
            Agent.enabled = false;
        }

        public void Slow(AnimationCurve SlowCurve)
        {
            if (SlowCoroutine != null)
            {
                StopCoroutine(SlowCoroutine);
            }
            SlowCoroutine = StartCoroutine(SlowDown(SlowCurve));
        }

        private IEnumerator SlowDown(AnimationCurve SlowCurve)
        {
            float time = 0;

            while (time < SlowCurve.keys[^1].time)
            {
                Agent.speed = BaseSpeed * SlowCurve.Evaluate(time);
                time += Time.deltaTime;
                yield return null;
            }

            Agent.speed = BaseSpeed;
        }

        public void OnSeePlayer(Transform player)
        {
            if (Agent.enabled)
            {
                StopCoroutine(MoveCoroutine);
                Player = player;
                MoveCoroutine = StartCoroutine(ChasePlayer(player));
            }
        }

        public void OnLosePlayer(Vector3 lastKnownPosition)
        {
            if (Agent.enabled)
            {
                StopCoroutine(MoveCoroutine);
                Player = null;
                MoveCoroutine = StartCoroutine(Roam());
            }
        }
        
        
        private IEnumerator ChasePlayer(Transform player)
        {
            while (true)
            {
                if (Agent.enabled)
                {
                    Agent.SetDestination(player.position);
                }

                yield return new WaitForSeconds(0.125f);
            }
        }

        public void GetKnockedBack(Vector3 force)
        {
            StopCoroutine(MoveCoroutine);
            MoveCoroutine = StartCoroutine(ApplyKnockback(force));
        }

        private IEnumerator ApplyKnockback(Vector3 force)
        {
            yield return null;
            Agent.enabled = false;
            Rigidbody.useGravity = true;
            Rigidbody.isKinematic = false;
            Rigidbody.AddForce(force);

            yield return new WaitForFixedUpdate();
            float knockbackTime = Time.time;
            yield return new WaitUntil(
                () => Rigidbody.velocity.magnitude < StillThreshold || Time.time > knockbackTime + MaxKnockbackTime
            );
            yield return new WaitForSeconds(0.25f);

            Rigidbody.velocity = Vector3.zero;
            Rigidbody.angularVelocity = Vector3.zero;
            Rigidbody.useGravity = false;
            Rigidbody.isKinematic = true;
            Agent.Warp(transform.position);
            Agent.enabled = true;

            yield return null;

            if (Player != null)
            {
                MoveCoroutine = StartCoroutine(ChasePlayer(Player));
            }
            else
            {
                MoveCoroutine = StartCoroutine(Roam());
            }
        }
    }
}