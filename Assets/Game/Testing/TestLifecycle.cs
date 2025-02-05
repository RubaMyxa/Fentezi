using UnityEngine;

// MonoBehaviour — это базовый класс Unity, от которого наследуются пользовательские скрипты.
// Этот класс предоставляет основные функциональные возможности для работы со сценой, объектами, событиями и игровым процессом.
// MonoBehaviour находится в пространстве имен UnityEngine и является мостом между вашим C# кодом и игровым движком Unity.
// Если скрипт не наследуется от MonoBehaviour, он не сможет быть привязан к объекту на сцене или использовать функции Unity.

public class TestLifecycle : MonoBehaviour
{
    private void Awake()
    {
        // Вызывается один раз, когда объект создается.
        // Используется для инициализации данных и зависимостей.

        MonoBehAbility(null);
    }

    private void OnEnable()
    {
        // Вызывается, когда объект или скрипт становится активным.
    }

    private void Start()
    {
        // Вызывается один раз перед первым кадром после инициализации.
        // Используется для начальной настройки объекта.
    }

    private void FixedUpdate()
    {
        // Вызывается через фиксированные интервалы времени.
        // Используется для работы с физикой (например, передвижение через Rigidbody).
    }

    private void Update()
    {
        // Вызывается каждый кадр.
        // Используется для обновления логики (например, проверки ввода, движения).
    }

    private void LateUpdate()
    {
        // Вызывается после Update() в каждом кадре.
        // Используется для обновления объектов, зависящих от других объектов (например, камеры, следящей за игроком).
    }

    private void OnDisable()
    {
        // Вызывается, когда объект или скрипт становится неактивным.
    }

    // Destroy
    private void OnDestroy()
    {
        // Вызывается, когда объект уничтожается.
    }

    private void MonoBehAbility(GameObject prefab) // Мой кастомный скрипт
    {
        print(""); // MonoBehaviour
        Debug.Log(""); // UnityEngine

        // Вызывает метод с задержкой.
        Invoke("TestMethod", 5f); // MonoBehaviour

        // Вызывает метод периодически через заданный интервал времени.
        InvokeRepeating("TestMethod", 3f, 2); // MonoBehaviour

        // Запускает корутину (метод, работающий асинхронно).
        StartCoroutine(""); // MonoBehaviour

        // Останавливает указанную корутину.
        StopCoroutine(""); // MonoBehaviour

        // Останавливает все корутины объекта.
        StopAllCoroutines(); // MonoBehaviour

        GameObject testGO = Instantiate(prefab); // Spawn gameObject | MonoBehaviour
        Destroy(testGO); // Destroy gameObject | MonoBehaviour

        TestLifecycle testLifecycle = GetComponent<TestLifecycle>(); // Получение компонента из текущего GameObject | MonoBehaviour

        GameObject g = gameObject; // GameObject к которому добавлен данный скрипт
        Transform t = transform; // Transform к которому добавлен данный скрипт

        // -----------------------------------------

        // Mathf                            | Математическая библиотека
        // Time.deltaTime                   | Время, прошедшее между кадрами. Используется для плавного движения.
        // Vector2/Vector3                  | Представляет вектор в 2D и 3D-пространстве (x, y, z).
        // Quaternion                       | Представляет вращение в Unity.
        // Raycast                          | Лучи, используемые для определения объектов на сцене.
        // Input                            | Обработка ввода пользователя.
        // Physics                          | Класс для работы с физикой.

        // gameObject.CompareTag("Player")  | Более эффективная альтернатива сравнению тегов.
        // gameObject.layer == LayerMask.NameToLayer("Player")  | Сравнение слоев.
    }

    private void TestMethod()
    {
        print("Hello!");
    }
}
