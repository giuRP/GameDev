using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PI4.BulletSystem
{
    public interface IHittable
    {
        void GetHit(GameObject sender, int damage);
    }
}