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
                if (_enabled)
                {
                    GameLoopController.Instance.gameMode ??= _gameMode;
                }
                GameLoopController.Instance.enabled = _enabled;
            }
        }

        public IGameMode GameMode
        {
            get => _gameMode;
            set
            {
                _gameMode = value;
                GameLoopController.Instance.gameMode = _gameMode;
            }
        }

        void Awake()
        {
            GameLoopController.Instance.gameMode = _gameMode;
            GameLoopController.Instance.Init(_entityBufferSize, _componentBufferSize);

            for (int i = 0; i < _entities.Length; i++)
            {
                RegisterEntityController(_entities[i]);
            }

            GameLoopController.Instance.enabled = _enabled;
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
                        var gameManager = new GameObject(nameof(GameLoopController));
                        _instance = gameManager.AddComponent<GameLoopController>();
                        _instance.enabled = false;
                    }
                    return _instance;
                }
            }
            public ECSManager ecsManager;
            public IGameMode gameMode;

            public void RegisterEntityController(EntityPrototypeController entityController)
            {
                if (gameMode != null)
                {
                    ecsManager.AddEntityController(entityController);
                }
            }

            public void Init(int entityBufferSize, int componentsBufferSize)
            {
                ecsManager = new(entityBufferSize, componentsBufferSize, EventBus, MessageBus);
            }

            void OnEnable()
            {
                gameMode?.OnEnable(ecsManager);
            }

            void OnDisable()
            {
                gameMode?.OnDisable(ecsManager);
            }

            void Start()
            {
                gameMode?.Start(ecsManager);
            }

            void Update()
            {
                gameMode.Update(ecsManager);
            }

            void FixedUpdate()
            {
                gameMode.FixedUpdate(ecsManager);
            }

            void LateUpdate()
            {
                gameMode.LateUpdate(ecsManager);
                EventBus.FlushAll();
                MessageBus.FlushAll();
            }


        }
    }

}