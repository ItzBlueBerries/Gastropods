using static Il2CppMono.Math.BigInteger;
using UnityEngine;
using Il2Cpp;
using UnityEngine.Playables;

namespace Gastropods.Components
{
    public class GastropodRandomMove : MonoBehaviour
    {
        private TimeDirector timeDir;
        private Vector3 startPosition;
        private Vector3 targetPosition;
        private Quaternion targetRotation;
        private double timer;

        public float movementRadius = 1.5f;
        public float gastroSpeed = 1;
        public float hoursTillMove = 1;

        void Start()
        {
            timeDir = SceneContext.Instance.TimeDirector;
            timer = timeDir.HoursFromNowOrStart(hoursTillMove);
            startPosition = transform.position;
            MoveGastropod();
        }

        void Update()
        {
            if (timeDir.HasReached(timer))
            {
                MoveGastropod();
                timer = timeDir.HoursFromNowOrStart(hoursTillMove);
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, gastroSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        }

        void MoveGastropod()
        {
            Vector2 randomCircle = UnityEngine.Random.insideUnitCircle * movementRadius;
            targetPosition = startPosition + new Vector3(randomCircle.x, 0f, randomCircle.y);
            targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        }
    }

    public class GastropodRandomMoveV2 : MonoBehaviour
    {
        private enum Mode { MOVE, IDLE }

        private TimeDirector timeDir;
        private Rigidbody gastroBody;
        private Animator animator;
        private Mode mode;
        private double time;

        public float movingForce = 1;
        public float hoursTillMove = 0.1f;

        void Awake()
        {
            timeDir = SceneContext.Instance.TimeDirector;
            gastroBody = GetComponent<Rigidbody>();
            animator = GetComponentInChildren<Animator>();

            time = timeDir.HoursFromNowOrStart(hoursTillMove);
        }

        void FixedUpdate()
        {
            if (timeDir.HasReached(time))
            {
                mode = Mode.MOVE;
                animator.SetBool("shouldMove", mode == Mode.MOVE);

                float num = 1f;
                gastroBody.AddForce(transform.forward * (movingForce * gastroBody.mass * num), ForceMode.Impulse);
                Vector3 position = transform.position + Vector3.down * (0.5f * transform.localScale.y);
                gastroBody.AddForceAtPosition(transform.forward * (2f * movingForce * gastroBody.mass * num), position, ForceMode.Impulse);

                time = timeDir.HoursFromNowOrStart(hoursTillMove);
                /*mode = Mode.IDLE;
                animator.SetBool("shouldMove", mode == Mode.MOVE);*/
            }
        }
    }

    public class GastropodRandomMoveV3 : MonoBehaviour
    {
        private TimeDirector timeDir;
        private Rigidbody gastroBody; // The rigidbody component of the object
        private Vector3 targetPosition; // The target position for the next movement
        private double lastMovement; // The time since the last movement

        public float movementSpeed = 1; // The movement speed of the object
        public float secondsTillMovement = 3; // The time between each movement

        void Start()
        {
            timeDir = SceneContext.Instance.TimeDirector;

            // Get the rigidbody component
            gastroBody = GetComponent<Rigidbody>();

            // Set the initial target position
            targetPosition = transform.position;
        }

        void FixedUpdate()
        {
            // If enough time has passed since the last movement, choose a new random target position
            if (timeDir.GetTime() - lastMovement > secondsTillMovement)
            {
                // Choose a random target position within a range
                float range = 5f;
                targetPosition = new Vector3(
                    transform.position.x + UnityEngine.Random.Range(-range, range),
                    transform.position.y + UnityEngine.Random.Range(-range, range),
                    transform.position.z + UnityEngine.Random.Range(-range, range)
                );

                // Reset the time since the last movement
                lastMovement = timeDir.GetTime();
            }

            gastroBody.MovePosition(Vector3.Lerp(
                transform.position,
                targetPosition,
                (float)timeDir.GetTime() * movementSpeed
            ));
        }
    }
}