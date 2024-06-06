using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelSelector : MonoBehaviour
{
    public Transform[] levelPositions;  // Positions des niveaux
    public GameObject cursor;  // Le curseur
    public float moveSpeed = 5f;  // Vitesse de déplacement du curseur
    public Color lockedColor = Color.red;  // Couleur des niveaux verrouillés
    public Color unlockedColor = Color.blue;  // Couleur des niveaux déverrouillés

    private int currentLevelIndex = 0;
    private bool[] levelUnlocked;  // Tableau pour savoir quels niveaux sont déverrouillés
    private LevelsUnlocked levelsUnlocked;

    private Animator animator;

    void Start()
    {
        // Trouver l'objet LevelsUnlocked dans la scène
        levelsUnlocked = FindObjectOfType<LevelsUnlocked>();
        animator = GetComponent<Animator>();

        if (levelsUnlocked == null)
        {
            Debug.LogError("LevelsUnlocked object not found in the scene.");
            return;
        }

        animator.Play("LevelSelector");

        // Mettre à jour les niveaux déverrouillés en fonction des données de LevelsUnlocked
        UpdateUnlockedLevels();

        // Positionner le curseur sur le premier niveau au début
        cursor.transform.position = new Vector3(levelPositions[currentLevelIndex].position.x, levelPositions[currentLevelIndex].position.y, cursor.transform.position.z);

        // Mettre à jour l'apparence des niveaux en fonction de leur état
        UpdateLevelVisuals();
    }

    void Update()
    {
        levelsUnlocked = FindObjectOfType<LevelsUnlocked>();
        // Déplacement du curseur avec les touches gauche et droite
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveCursor(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveCursor(-1);
        }

        // Sélection du niveau avec la touche Entrée
        if (Input.GetKeyDown(KeyCode.Return) && levelUnlocked[currentLevelIndex])
        {
            SelectLevel();
        }
    }

    void MoveCursor(int direction)
    {
        int newIndex = currentLevelIndex + direction;

        // S'assurer que le nouvel index est dans les limites
        if (newIndex >= 0 && newIndex < levelPositions.Length && levelUnlocked[newIndex])
        {
            currentLevelIndex = newIndex;
            StartCoroutine(MoveCursorSmoothly(levelPositions[currentLevelIndex].position));
        }
    }

    IEnumerator MoveCursorSmoothly(Vector3 targetPosition)
    {
        // Limiter le mouvement du curseur aux dimensions X et Y
        Vector2 targetPosition2D = new Vector2(targetPosition.x, targetPosition.y);

        while (Vector2.Distance(new Vector2(cursor.transform.position.x, cursor.transform.position.y), targetPosition2D) > 0.01f)
        {
            cursor.transform.position = Vector2.MoveTowards(new Vector2(cursor.transform.position.x, cursor.transform.position.y), targetPosition2D, moveSpeed * Time.deltaTime);
            yield return null;
        }
        cursor.transform.position = targetPosition2D;
    }

    void SelectLevel()
    {
        // Charger la scène du niveau sélectionné
        SceneManager.LoadScene("ILevel" + (currentLevelIndex + 1));
    }

    void UpdateLevelVisuals()
    {
        for (int i = 0; i < levelPositions.Length; i++)
        {
            SpriteRenderer sr = levelPositions[i].GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = levelUnlocked[i] ? unlockedColor : lockedColor;
            }
        }
    }

    void UpdateUnlockedLevels()
    {
        // Récupérer le nombre de niveaux déverrouillés à partir de LevelsUnlocked
        int levelsUnlockedCount = Mathf.Clamp((int)levelsUnlocked.unlockedLevels, 0, levelPositions.Length);
        levelUnlocked = new bool[levelPositions.Length];

        // Mettre à jour l'état des niveaux déverrouillés
        for (int i = 0; i < levelUnlocked.Length; i++)
        {
            levelUnlocked[i] = (i < levelsUnlockedCount);
        }
    }
}
