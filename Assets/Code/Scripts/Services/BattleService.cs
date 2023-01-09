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
    [Serializable]
    public class BattleService
    {
        private Player _player;
        private Spot _spot;
        private GameObject particle;
        
        private int GetPlayerPoints => _player.Group.GroupService.GetPoints;
        private List<GroupUnit> GetPlayerUnits => _player.Group.GroupService.units;
        private int GetSpotPoints => _spot.Group.GroupService.GetPoints;
        private List<GroupUnit> GetSpotUnits => _spot.Group.GroupService.units;


        public void Initialize(Player player)
        {
            _player = player;
        }

        public void InvokeBattle(Spot spot)
        {
            _spot = spot;
            _player.StartCoroutine(StartBattle());
        }
        
        private IEnumerator StartBattle()
        {
            FocusBattle();
            MatchUnits();
            AddParticles();

            yield return _player.StartCoroutine(ProcessBattle());
            
            MismatchUnits();
            RemoveParticles();
            UnfocusBattle();
        }

        private IEnumerator ProcessBattle()
        {
            bool isSpotWins = GetSpotPoints > GetPlayerPoints;
            
            yield return _player.StartCoroutine(ProcessLogics());
            
            if (isSpotWins)
                _player.Kill();
            else
                _spot.Kill();
        }

        private IEnumerator ProcessLogics()
        {
            // Getting target points for the player, to know point where we need to stop disposing units
            var targetPoints = GetPlayerPoints - GetSpotPoints;

            // Disposing player unit while player group points after changes doesn`t touch target points AND
            // while player have any unit in his group
            while (GetPlayerUnits.Count != 0 && 
                   GetPlayerPoints - GetPlayerUnits[^1].settings.points >= targetPoints)
            {
                // Removing last (the lowest by points) unit in player`s and spot`s groups
                if(GetSpotUnits.Count != 0)
                    _spot.Group.GroupService.Remove(GetSpotUnits[^1].settings.points);
                _player.Group.GroupService.Remove(GetPlayerUnits[^1].settings.points);

                // Waiting for some time, to make "realtime" battle
                yield return new WaitForSeconds(0.75f);
            }
        }
        
        private void MatchUnits()
        {
            var attackers = ShuffleUnits(GetPlayerUnits);
            var defenders = ShuffleUnits(GetSpotUnits);

            if (attackers.Count < defenders.Count)
                SwapLists(attackers, defenders);

            PerformMatching(attackers, defenders);
        }

        private void PerformMatching(List<GroupUnit> attackers, List<GroupUnit> defenders)
        {
            for (int i = 0; i < attackers.Count; i++)
            {
                var spotUnitIndex = i % defenders.Count;
                var spotUnit = defenders[spotUnitIndex];
                var playerUnit = attackers[i];

                playerUnit.SetBattleState(spotUnit.objectTransform, spotUnit.settings.triggerRadius);
                spotUnit.SetBattleState(playerUnit.objectTransform, playerUnit.settings.triggerRadius);
            }
        }

        private void MismatchUnits()
        {
            var playerUnits = GetPlayerUnits;
            var spotUnits = GetSpotUnits;
            
            if(!ReferenceEquals(_player, null))
                foreach (var playerUnit in playerUnits)
                    playerUnit.RemoveBattleState();
            
            if(!ReferenceEquals(_spot, null))
                foreach (var groupUnit in spotUnits)
                    groupUnit.RemoveBattleState();
        }

        private List<GroupUnit> ShuffleUnits(List<GroupUnit> units)
        {
            var random = new Random();
            var randomized = units.OrderBy(item => random.Next());

            return randomized.ToList();
        }

        private void SwapLists(List<GroupUnit> first, List<GroupUnit> second)
        {
            List<GroupUnit> temp = new List<GroupUnit>();
            temp.AddRange(first);
                
            first.Clear();
            first.AddRange(second);
                
            second.Clear();
            second.AddRange(temp);
        }
        
        private void AddParticles()
        {
            var position = _player.transform.position + (_spot.transform.position - _player.transform.position) / 2;
            var rotation = Quaternion.Euler(-90, 0, 0);
            var prefab = GameCore.Instance.smoke;
            
            GameObject smoke = GameObject.Instantiate(prefab, position, rotation);
            smoke.SetActive(true);

            particle = smoke;
        }
        
        private void RemoveParticles()
        {
            var particleSystem = particle.GetComponent<ParticleSystem>();
            particleSystem.Stop();
        }
        
        private void UnfocusBattle()
        {
            _player.state = Player.State.Free;
            _spot.state = Spot.State.Free;
        }

        private void FocusBattle()
        {
            _player.state = Player.State.Battle;
            _spot.state = Spot.State.Battle;
        }
    }
}