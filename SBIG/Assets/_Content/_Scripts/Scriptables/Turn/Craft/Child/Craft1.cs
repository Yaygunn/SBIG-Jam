using Controller.Player;
using Manager.Enemy;
using System.Collections;
using Manager.Task;
using UnityEngine;

namespace Scriptables.Turn.Craft.Child
{
    [CreateAssetMenu(fileName = "CraftTest", menuName = "Scriptables/Turn/Craft/Test")]
    public class Craft1 : BaseCraftTurn
    {
        [SerializeField] private string _text;
        public bool _continue;
        public static Craft1 Instance;
        float timer;
        float necesseryTime = 20;
        public override IEnumerator TurnOperations()
        {
            Instance = this;
            Debug.Log("CraftStarter");
            PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            player.ChangeState(player.StateCraft);
            _continue = true;
            timer = 0;
            while (_continue)
            {
                yield return null;
                EnemyManager.Instance.EndAndFlee();
                timer += Time.deltaTime;
                if (timer > necesseryTime)
                    break;
            }
            
            if (TaskManager.Instance.IsCurrentTask("Pick and throw 3 plants into the cauldron"))
            {
                TaskManager.Instance.CompleteTask("Pick and throw 3 plants into the cauldron");   
            }

            EndTurn();
        }
    }
}