using UnityEngine;

namespace Assets.Game.Code.Test.Initialize
{
    public class Crystal : MonoBehaviour
    {
        // Декларация пустых переменных
        private SpriteRenderer spriteRenderer;
        private Rigidbody2D rb;
        private BoxCollider2D col;

        // Декларация пустых приватных переменных с возможностью инициализации из инспектора
        [Header("Init with SerializeField variables")]
        [SerializeField]
        private SpriteRenderer spriteRendererSerialize;
        [SerializeField]
        private Rigidbody2D rbSerialize;
        [SerializeField]
        private BoxCollider2D colSerialize;

        // Декларация пустых публичных переменных с возможностью инициализации из инспектора
        [Header("Init with public variables")]
        public SpriteRenderer spriteRendererPublic;
        public Rigidbody2D rbPublic;
        public BoxCollider2D colPublic;

        private DI.Game game;

        // Инициализация с помощью метода. Инициализация происходит в стороннем классе (Game.cs)
        public void Construct(DI.Game game)
        {
            this.game = game;
        }

        private void Awake()
        {
            // Инициализация на старте жизненного цикла объекта
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<BoxCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Destroy(spriteRendererPublic);
                Destroy(rbPublic);
                Destroy(colPublic);

                game.SpawnEnemys();
            }
        }
    }
}