using System.Linq;
using UnityEngine;

namespace MiniECS
{
    public sealed class LevelController : MiniECSBehaviour
    {
        [SerializeReference, ReferencePicker] private IGameMode _gameMode;
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
            public Game game;
            public IGameMode gameMode = new SilentMode();

            public void RegisterEntityController(EntityController entityController)
            {
                if (gameMode != null)
                {
                    Entity entity = game.EntityManager.AddEntityController(entityController);
                    game.ComponentsManager.AddComponentPrototype(in entity, entityController.Components);
                }
            }

            public void Init(int entityBufferSize, int componentsBufferSize)
            {
                game = new(entityBufferSize, componentsBufferSize);
            }

            void Start()
            {
                gameMode.Start(game);
            }

            void Update()
            {
                gameMode.Update(game);
            }

            void FixedUpdate()
            {
                gameMode.FixedUpdate(game);
            }

            void LateUpdate()
            {
                gameMode.LateUpdate(game);
            }

            public class SilentMode : IGameMode
            {
                public void FixedUpdate(Game game) { }
                public void LateUpdate(Game game) { }
                public void Start(Game game) { }
                public void Update(Game game) { }
            }

        }
    }

}