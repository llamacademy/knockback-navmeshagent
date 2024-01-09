using UnityEngine;

namespace LlamAcademy.Guns
{
    [CreateAssetMenu(fileName = "Knockback Config", menuName = "Guns/Knockback Config", order = 8)]
    public class KnockbackConfigScriptableObject : ScriptableObject, System.ICloneable
    {
        public float KnockbackStrength = 25000;
        public ParticleSystem.MinMaxCurve DistanceFalloff;

        public Vector3 GetKnockbackStrength(Vector3 direction, float distance)
        {
            return KnockbackStrength * DistanceFalloff.Evaluate(distance) * direction;
        }
        
        public object Clone()
        {
            KnockbackConfigScriptableObject clone = CreateInstance<KnockbackConfigScriptableObject>();
            
            Utilities.CopyValues(this, clone);

            return clone;
        }
    }
}
