using System.Collections;
using Code.Scripts.Core;
using UnityEngine;

namespace Code.Scripts.Services
{
    public class BattleService
    {
        public static IEnumerator StartBattle(Player player, Spot spot)
        {
            player.state = Player.State.Battle;
            spot.state = Spot.State.Battle;
            
            Debug.Log("Battle started!");

            yield return new WaitForSeconds(2.0f);
            
            Debug.Log("Battle ended!");
            
            player.state = Player.State.Move;
            spot.state = Spot.State.Wait;
        }
    }
}