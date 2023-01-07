using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.Core;
using Code.Scripts.Units;
using UnityEngine;
using Object = System.Object;
using Random = System.Random;

namespace Code.Scripts.Services
{
    public class BattleService
    {
        public static IEnumerator StartBattle(Player player, Spot spot)
        {
            SetBattleMode(player, spot);
            Debug.Log("Battle started!");

            MatchUnits(player, spot);

            yield return new WaitForSeconds(2.0f);
            
            Debug.Log("Battle ended!");
            
            RemoveBattleMode(player, spot);

            MismatchUnits(player, spot);
        }

        private static void MismatchUnits(Player player, Spot spot)
        {
            var playerUnits = player.Group.GetUnits;
            var spotUnits = spot.Group.GetUnits;
            
            foreach (var playerUnit in playerUnits)
                playerUnit.RemoveBattleState();
            
            foreach (var groupUnit in spotUnits)
                groupUnit.RemoveBattleState();
        }

        private static void RemoveBattleMode(Player player, Spot spot)
        {
            player.state = Player.State.Move;
            spot.state = Spot.State.Wait;
        }

        private static void SetBattleMode(Player player, Spot spot)
        {
            player.state = Player.State.Battle;
            spot.state = Spot.State.Battle;
        }

        private static void MatchUnits(Player player, Spot spot)
        {
            var playerUnits = ShuffleUnits(player.Group.GetUnits);
            var spotUnits = ShuffleUnits(spot.Group.GetUnits);

            for (int i = 0; i < playerUnits.Count; i++)
            {
                var spotUnitIndex = i % spotUnits.Count;
                var spotUnit = spotUnits[spotUnitIndex];
                var playerUnit = playerUnits[i];
                
                playerUnit.SetBattleState(spotUnit.objectTransform, spotUnit.settings.triggerRadius);
                spotUnit.SetBattleState(playerUnit.objectTransform, playerUnit.settings.triggerRadius);
            }
        }

        private static List<GroupUnit> ShuffleUnits(List<GroupUnit> units)
        {
            var random = new Random();
            var randomized = units.OrderBy(item => random.Next());

            return randomized.ToList();
        }
    }
}