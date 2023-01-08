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
        
        private bool _isBattleCompleted;
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
            _isBattleCompleted = false;
            _spot = spot;

            _player.StartCoroutine(StartBattle());
        }
        
        private IEnumerator StartBattle()
        {
            SetBattleMode();

            MatchUnits();

            AddParticles();

            Coroutine battleProcessor = _player.StartCoroutine(ProcessBattle());
            yield return new WaitWhile(() => !_isBattleCompleted);
            
            MismatchUnits();

            RemoveParticles();
            
            RemoveBattleMode();
        }

        private void RemoveParticles()
        {
            var particleSystem = particle.GetComponent<ParticleSystem>();
            particleSystem.Stop();
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

        private IEnumerator ProcessBattle()
        {
            yield return _player.StartCoroutine(StartDisposing());
            
            if (GetSpotPoints > GetPlayerPoints)
                _player.state = Player.State.Dead;
            else
                _spot.state = Spot.State.Dead;

            _isBattleCompleted = true;
        }

        private IEnumerator StartDisposing()
        {
            var targetPoints = GetPlayerPoints - GetSpotPoints;

            while (GetPlayerPoints >= targetPoints)
            {
                if (GetPlayerUnits.Count == 0) break;
                
                var playerUnit = GetPlayerUnits[^1];
                if (GetPlayerPoints - playerUnit.settings.points < targetPoints) break;
                
                var spotUnit = GetSpotUnits[^1];
                
                _player.Group.GroupService.Remove(playerUnit.settings.points);
                _spot.Group.GroupService.Remove(spotUnit.settings.points);

                yield return new WaitForSeconds(0.75f);
            }
        }

        private void MismatchUnits()
        {
            var playerUnits = GetPlayerUnits;
            var spotUnits = GetSpotUnits;
            
            if(ReferenceEquals(_player, null))
                foreach (var playerUnit in playerUnits)
                    playerUnit.RemoveBattleState();
            
            if(ReferenceEquals(_spot, null))
                foreach (var groupUnit in spotUnits)
                    groupUnit.RemoveBattleState();
        }

        private void RemoveBattleMode()
        {
            _player.state = Player.State.Free;
            _spot.state = Spot.State.Free;
        }

        private void SetBattleMode()
        {
            _player.state = Player.State.Battle;
            _spot.state = Spot.State.Battle;
        }

        private void MatchUnits()
        {
            var attackers = ShuffleUnits(GetPlayerUnits);
            var defenders = ShuffleUnits(GetSpotUnits);

            if (attackers.Count < defenders.Count)
            {
                List<GroupUnit> temp = new List<GroupUnit>();
                temp.AddRange(defenders);
                
                defenders.Clear();
                defenders.AddRange(attackers);
                
                attackers.Clear();
                attackers.AddRange(temp);
            }

            for (int i = 0; i < attackers.Count; i++)
            {
                var spotUnitIndex = i % defenders.Count;
                var spotUnit = defenders[spotUnitIndex];
                var playerUnit = attackers[i];
                
                playerUnit.SetBattleState(spotUnit.objectTransform, spotUnit.settings.triggerRadius);
                spotUnit.SetBattleState(playerUnit.objectTransform, playerUnit.settings.triggerRadius);
            }
        }

        private List<GroupUnit> ShuffleUnits(List<GroupUnit> units)
        {
            var random = new Random();
            var randomized = units.OrderBy(item => random.Next());

            return randomized.ToList();
        }
    }
}