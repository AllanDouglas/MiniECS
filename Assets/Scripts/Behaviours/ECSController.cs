using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MiniECS
{
    [MovedFrom(true, sourceClassName: "LevelController")]
    public sealed class ECSController : MiniECSBehaviour
    {
        [SerializeReference, ReferencePicker] private IGameMode _gameMode;
        [SerializeField] private bool _enabled;
        [SerializeField] private int _entityBufferSize = 100;
        [SerializeField] private int _componentBufferSize = 10;

        [SerializeField, HideInInspector] private EntityController[] _entities;

        void Awake()
        {
            GameLoopController.Instance.gameMode = _gameMode;
            GameLoopController.Instance.Init(_entityBufferSize, _componentBufferSize);

            for (int i = 0; i < _entities.Length; i++)
            {
                GameLoopController.Instance.RegisterEntityController(_entities[i]);
            }

            GameLoopController.Instance.enabled = _enabled;
        }

        void OnValidate()
        {
            _entities = FindObjectsByType<EntityController>(FindObjectsSortMode.InstanceID);
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

            public void RegisterEntityController(EntityController entityController)
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
                gameMode.Start(ecsManager);
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