using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MiniECS
{
    [DefaultExecutionOrder(-50)]
    [MovedFrom(true, sourceClassName: "LevelController")]
    public sealed class ECSController : MiniECSBehaviour
    {
        [SerializeReference, ReferencePicker] private IGameMode _gameMode;
        [SerializeField] private bool _enabled;
        [SerializeField] private int _entityBufferSize = 100;
        [SerializeField] private int _componentBufferSize = 10;

        [SerializeField] private EntityPrototypeController[] _entities;

        public ECSManager ECSManager => GameLoopController.Instance.ecsManager;



        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                GameLoopController.Instance.enabled = _enabled;

            }
        }

        public IGameMode GameMode
        {
            get => _gameMode;
            set
            {
                _gameMode = value;
                GameLoopController.Instance.GameMode = _gameMode;
            }
        }

        void Awake()
        {

            GameLoopController.Instance.Init(_entityBufferSize, _componentBufferSize);
            
            for (int i = 0; i < _entities.Length; i++)
            {
                RegisterEntityController(_entities[i]);
            }

            Enabled = _enabled;
            GameMode = _gameMode;
        }

        public void RegisterEntityController(EntityPrototypeController entityController)
        {
            GameLoopController.Instance.RegisterEntityController(entityController);
        }

        void OnValidate()
        {
            _entities = FindObjectsByType<EntityPrototypeController>(FindObjectsSortMode.InstanceID);
        }

        private sealed class GameLoopController : MiniECSBehaviour
        {
            public static GameLoopController _instance;
            public static GameLoopController Instance
            {
                get
                {
                    if (_instance == null)
                    {
                        GameObject gameManager = new(nameof(GameLoopController))
                        {
                            hideFlags = HideFlags.HideInInspector
                        };

                        _instance = gameManager.AddComponent<GameLoopController>();
                        _instance.enabled = false;
                    }
                    return _instance;
                }
            }

            public IGameMode GameMode
            {
                get => _gameMode;
                set
                {
                    _gameMode = value;

                    if (ecsManager != null && enabled)
                    {
                        _gameMode?.Start(ecsManager);
                        _gameMode?.OnEnable(ecsManager);
                    }
                }
            }

            public ECSManager ecsManager;
            private IGameMode _gameMode;

            public void RegisterEntityController(EntityPrototypeController entityController)
            {
                ecsManager.AddEntityController(entityController);
            }

            public void Init(int entityBufferSize, int componentsBufferSize)
            {
                ecsManager = new(entityBufferSize, componentsBufferSize, EventBus, MessageBus);
            }

            void OnEnable()
            {
                GameMode?.OnEnable(ecsManager);
            }

            void OnDisable()
            {
                GameMode?.OnDisable(ecsManager);
            }

            void OnDestroy()
            {
                GameMode?.OnDestroy(ecsManager);
            }

            void Update()
            {
                GameMode.Update(ecsManager);
            }

            void FixedUpdate()
            {
                GameMode.FixedUpdate(ecsManager);
            }

            void LateUpdate()
            {
                GameMode.LateUpdate(ecsManager);
                EventBus.FlushAll();
                MessageBus.FlushAll();
            }
        }
    }

}