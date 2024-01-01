using UnityEngine;

namespace LlamAcademy.Guns.ImpactEffects
{
    public interface IKnockbackable
    {
        void GetKnockedBack(Vector3 force);
    }
}