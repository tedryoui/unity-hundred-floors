using System.Collections;
using UnityEngine;

namespace Code.Scripts.Units
{
    public class QueueUnit
    {
        public enum QueueUnitStatus
        {
            Wait,
            Move
        }
        
        public Transform Transform;
        public Vector3 Placement;
        public QueueUnitStatus Status;
        public float Speed;
        public AnimationCurve SpeedCurve;

        public void Update()
        {
            switch (Status)
            {
                case QueueUnitStatus.Wait:
                    break;
                case QueueUnitStatus.Move:
                    Move();
                    break;
            }
            
        }

        private void Move()
        {    
            Transform.position = Vector3.MoveTowards(
                Transform.position, 
                Placement,
                Speed * Time.deltaTime * SpeedCurve.Evaluate(Time.time));

            if (Transform.position.Equals(Placement))
            {
                Status = QueueUnitStatus.Wait;
            }
        }
    }
}