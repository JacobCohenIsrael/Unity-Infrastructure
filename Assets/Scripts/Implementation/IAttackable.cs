using UnityEngine;
using System.Collections;

namespace CWO
{
    public interface IAttackable
    {
        void TakeDamage(int damageTaken);
        bool isDestroyed();
    }
}

