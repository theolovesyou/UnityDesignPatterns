using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FactoryMethodPattern
{
    public class FactoryMethodPattern : MonoBehaviour
    {
        public enum UnitType
        {
            Marine,
            Firebat,
            Vulture,
            Tank,
        }

        private List<Unit> units = new List<Unit>();

        public abstract class UnitFactory
        {
            protected abstract Unit CreateUnit(UnitType type);

            public Unit GetUnit(UnitType type)
            {
                Unit unit = CreateUnit(type);

                return unit;
            }
        }

        public class Barrack : UnitFactory
        {
            protected override Unit CreateUnit(UnitType type)
            {
                Unit unit = null;

                if (type == UnitType.Marine)
                    unit = new Marine();
                else if (type == UnitType.Firebat)
                    unit = new Firebat();

                return unit;
            }
        }

        public class Factory : UnitFactory
        {
            protected override Unit CreateUnit(UnitType type)
            {
                Unit unit = null;

                if (type == UnitType.Vulture)
                    unit = new Vulture();
                else if (type == UnitType.Tank)
                    unit = new Tank();

                return unit;
            }
        }

        public abstract class Unit
        {
            protected string name;
            protected IMoveBehavior moveBehavior;
            protected IAttackBehavior attackBehavior;

            public void Move()
            {
                moveBehavior.Move();
            }

            public void Attack()
            {
                attackBehavior.Attack();
            }
        }

        public class Marine : Unit
        {
            public Marine()
            {
                name = "Marine";
                moveBehavior = new OnFoot();
                attackBehavior = new Melee();

                Debug.Log(string.Format("{0} is created.", name));
            }
        }

        public class Firebat : Unit
        {
            public Firebat()
            {
                name = "Firebat";
                moveBehavior = new OnFoot();
                attackBehavior = new Melee();

                Debug.Log(string.Format("{0} is created.", name));
            }
        }

        public class Vulture : Unit
        {
            public Vulture()
            {
                name = "Vulture";
                moveBehavior = new OnVehicle();
                attackBehavior = new Ranged();

                Debug.Log(string.Format("{0} is created.", name));
            }
        }

        public class Tank : Unit
        {
            public Tank()
            {
                name = "Tank";
                moveBehavior = new OnVehicle();
                attackBehavior = new Ranged();

                Debug.Log(string.Format("{0} is created.", name));
            }
        }

        public interface IMoveBehavior
        {
            void Move();
        }

        public interface IAttackBehavior
        {
            void Attack();
        }

        public class OnFoot : IMoveBehavior
        {
            public void Move()
            {
                Debug.Log("OnFoot.");
            }
        }

        public class OnVehicle : IMoveBehavior
        {
            public void Move()
            {
                Debug.Log("OnVehicle.");
            }
        }

        public class OnPlane : IMoveBehavior
        {
            public void Move()
            {
                Debug.Log("OnPlane.");
            }
        }

        public class Melee : IAttackBehavior
        {
            public void Attack()
            {
                Debug.Log("Melee.");
            }
        }

        public class Ranged : IAttackBehavior
        {
            public void Attack()
            {
                Debug.Log("Ranged.");
            }
        }

        public class Magic : IAttackBehavior
        {
            public void Attack()
            {
                Debug.Log("Magic.");
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            UnitFactory unitFactory = null;

            unitFactory = new Barrack();
            units.Add(unitFactory.GetUnit(UnitType.Marine));
            units.Add(unitFactory.GetUnit(UnitType.Firebat));

            unitFactory = new Factory();
            units.Add(unitFactory.GetUnit(UnitType.Vulture));
            units.Add(unitFactory.GetUnit(UnitType.Tank));

            foreach (var unit in units)
            {
                unit.Move();
                unit.Attack();
            }
        }
    }
}