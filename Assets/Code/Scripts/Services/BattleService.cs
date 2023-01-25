using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Code.Scripts.Core;
using Code.Scripts.Core.Player;
using Code.Scripts.Units;
using UnityEngine;
using Object = System.Object;
using Random = System.Random;

namespace Code.Scripts.Services
{
    public class BattleService : MonoBehaviour
    {
        [SerializeField] private float _despawnDelay;
        [SerializeField] private CinemachineVirtualCamera _vCam;
        [SerializeField] private GameObject _smokePrefab;
        private GameObject _tempObj;

        private Player Player => GameCore.GetPlayer;
        private int PlayerPoints => (int) Player.PointsStat.GetValue;
        private int SpotPoints => (int) _spot.PointsStat.GetValue;
        private List<GroupUnit> PlayerUnits => Player.Group.Units;
        private List<GroupUnit> SpotUnits => _spot.Group.Units;

        private Vector3 GetFightPosition => _spot.transform.position - Player.transform.position;

        private Spot _spot;
        private ParticleSystem _smoke;

        private ParticleSystem GetSmokeObject
        {
            get
            {
                if (_smoke == null)
                {
                    GameObject smoke = Instantiate(_smokePrefab, Vector3.zero, Quaternion.identity);
                    _smoke = smoke.GetComponent<ParticleSystem>();
                }

                return _smoke;
            }
        }

        private List<BattleUnit> _battleUnits;

        private void Awake()
        {
            GameState.CrrGameState.OnStateChanged += OnGameStateChanged;
            
            _tempObj = new GameObject("Battle Position");
            _tempObj.transform.parent = transform;
            _vCam.Follow = _tempObj.transform;
            _vCam.LookAt = _tempObj.transform;
        }

        private void OnGameStateChanged(GameState.GameStateValue state)
        {
            enabled = state == GameState.GameStateValue.Battle;
        }

        private void OnDestroy()
        {
            GameState.CrrGameState.OnStateChanged -= OnGameStateChanged;
        }

        private void Update()
        {
            foreach (var battleUnit in _battleUnits)
            {
                battleUnit.Update();
            }
        }

        public void InvokeBattle(Spot spot)
        {
            Player.GetComponent<Rigidbody>().velocity = Vector3.zero;

            _spot = spot;
            
            var minority = ShuffleLists(PlayerUnits);
            var majority = ShuffleLists(SpotUnits);

            if (minority.Count > majority.Count)
                SwapLists(minority, majority);

            _battleUnits = new List<BattleUnit>(minority.Count);

            for (var i = 0; i < minority.Count; i++)
            {
                _battleUnits.Add(new BattleUnit(minority[i]));
            }

            for (int i = 0; i < majority.Count; i++)
            {
                _battleUnits[i % minority.Count].AddAttacker(majority[i]);
            }

            GameState.CrrGameState.ActiveStateValue = GameState.GameStateValue.Battle;
            SpawnSmoke();

            StartCoroutine(WaitForReady());
        }

        private IEnumerator WaitForReady()
        {
            SetupCamera();
            
            yield return new WaitWhile(() =>
            {
                foreach (var battleUnit in _battleUnits)
                {
                    if (!battleUnit.IsReady())
                        return true;
                }

                return false;
            });

            StartCoroutine(ProcessLogics());
        }

        private void SetupCamera()
        {
            _vCam.Priority = 101;
            _tempObj.transform.position = Player.transform.position + GetFightPosition / 2.0f;
        }

        private IEnumerator ProcessLogics()
        {
            // Getting target points for the player, to know point where we need to stop disposing units
            var targetPoints = PlayerPoints - SpotPoints;

            // Disposing player unit while player group points after changes doesn`t touch target points AND
            // while player have any unit in his group
            while (PlayerUnits.Count != 0 && 
                   PlayerPoints - PlayerUnits[^1].Preset.points >= targetPoints)
            {
                // Waiting for some time, to make "realtime" battle
                yield return new WaitForSeconds(_despawnDelay);
                
                // Removing last (the lowest by points) unit in player`s and spot`s groups
                if(SpotUnits.Count != 0)
                    _spot.Group.Remove(SpotUnits[^1].Preset.points);

                if (SpotUnits.Count == 0 && !Player.PointsStat.IsVulnerable)
                    break;
                
                if(Player.PointsStat.IsVulnerable)
                    Player.Group.Remove(PlayerUnits[^1].Preset.points);
            }
            
            
            if(targetPoints <= 0 && Player.PointsStat.IsVulnerable)
                Player.ChangeState(Player.DeadState);
            else 
                _spot.ChangeState(_spot.DeadState);

            CompleteBattle();
        }

        private void SpawnSmoke()
        {
            var playerPosition = Player.transform.position;
            var spotPosition = _spot.transform.position;
            var distance = spotPosition - playerPosition;
            var smokePosition = playerPosition + (distance / 2.0f) + Vector3.up * 3;

            GetSmokeObject.transform.position = smokePosition;
            GetSmokeObject.Play();
        }
        
        private void CompleteBattle()
        {
            GetSmokeObject.Stop();
            GetSmokeObject.transform.position = Vector3.zero;

            foreach (var battleUnit in _battleUnits)
                battleUnit.ResetUnits();
            _battleUnits.Clear();

            _vCam.Priority = 0;
            GameState.CrrGameState.ActiveStateValue = GameState.GameStateValue.Gameplay;
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
        
        private List<GroupUnit> ShuffleLists(List<GroupUnit> units)
        {
            var random = new Random();
            var randomized = units.OrderBy(item => random.Next());

            return randomized.ToList();
        }
    }
}