using System;
using System.Collections.Generic;
using Code.Scripts.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private SatelliteManager _satelliteManager;
        [SerializeField] private float _waveInterval;

        [Header("Wave")] 
        [SerializeField] private Vector2 _enemy1CountRange;
        [SerializeField] private Vector2 _enemy2CountRange;
        [SerializeField] private Vector2 _enemy3CountRange;

        private BoxCollider2D _collider;
        private Planet _planet;

        private bool _isWaveActive;
        private float _waveTimer;
        private int _waveCount;
        private int _enemyCount;

        private void Start()
        {
            _collider = GetComponent<BoxCollider2D>();
            _planet = _satelliteManager.GetPlanet();

            _enemyCount = 0;
        }

        private void Update()
        {
            if (!_isWaveActive)
            {
                _waveTimer += Time.deltaTime;
            }
            else if (_enemyCount == 0)
            {
                _isWaveActive = false;
            }
            if (_waveTimer >= _waveInterval)
            {
                int enemyCount = 0;
                if (_waveCount == 0)
                {
                    enemyCount = Random.Range((int)_enemy1CountRange.x, (int)_enemy1CountRange.y + 1);
                }
                else if (_waveCount == 1)
                {
                    enemyCount = Random.Range((int)_enemy2CountRange.x, (int)_enemy2CountRange.y + 1);
                }
                else if (_waveCount == 2)
                {
                    enemyCount = Random.Range((int)_enemy3CountRange.x, (int)_enemy3CountRange.y + 1);
                }

                for (int i = 0; i < enemyCount; ++i)
                {
                    var enemy = Instantiate(_planet.enemies[_waveCount].enemyPrefab,
                        transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0f) + 
                        new Vector3(Random.Range(-_collider.size.x / 2f, _collider.size.x / 2f), 
                            Random.Range(-_collider.size.y / 2f, _collider.size.y / 2f)),
                        Quaternion.identity); 
                    var enemyComponent = enemy.GetComponent<Enemy>();
                    enemyComponent.moveTarget = _satelliteManager.GetController().transform;
                    enemyComponent.enemySpawner = this;
                    _enemyCount += 1;
                }

                _waveCount = Math.Min(_waveCount + 1, 2);
                _isWaveActive = true;
                _waveTimer = 0f;
            }
        }

        public void EnemyDied()
        {
            _enemyCount -= 1;
        }
    }
}
