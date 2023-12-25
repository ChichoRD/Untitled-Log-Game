using System.Threading.Tasks;
using UnityEngine;

namespace AttackSystem.Attack.Implementations.Throwable
{
    internal class TransformableThrowableAttack : MonoBehaviour, IAttack<IVelocityAttackData>
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private Matrix4x4 _throwingTransformation = Matrix4x4.identity;


        public Task TryAttack<UAttackData>(UAttackData attackData) where UAttackData : IVelocityAttackData
        {
            return Throw(attackData.VelocityIncrement);
        }

        private async Task Throw(Vector3 throwVelocityIncrement)
        {
            Vector3 transformedThrowVelocity = _throwingTransformation.MultiplyVector(throwVelocityIncrement);
            Vector3 transformedThrowForce = transformedThrowVelocity * _rigidbody.mass / Time.fixedDeltaTime;
            _rigidbody.AddForce(transformedThrowForce, ForceMode2D.Force);

            Vector3 transformedGravity = _throwingTransformation.MultiplyVector(Physics2D.gravity);
            Vector3 transformedGravityForce = transformedGravity * _rigidbody.mass;

            float velocityReductionTime = -Vector3.Dot(Physics2D.gravity, throwVelocityIncrement * 2.0f) / Vector3.Dot(Physics2D.gravity, Physics2D.gravity);

            const int MILISECONDS_FROM_SECONDS = 1000;
            float deltaTime = Time.fixedDeltaTime;
            int milisecondsDeltaTime = (int)(deltaTime * MILISECONDS_FROM_SECONDS);

            for (float t = 0.0f; t < velocityReductionTime; t += deltaTime)
            {
                _rigidbody.AddForce(transformedGravityForce, ForceMode2D.Force);
                await Task.Delay(milisecondsDeltaTime);
            }
        }
    }
}
