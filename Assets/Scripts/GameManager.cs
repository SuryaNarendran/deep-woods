using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] HUD hud;
    [SerializeField] TextBox _textBox;
    [SerializeField] ObjectiveText objectiveText;
    [SerializeField] FadeAll keyTutorial;
    [SerializeField] int _totalChildren;
    [SerializeField] LightTrigger playerLightTrigger;
    [SerializeField] LightTrigger enemyLightTrigger;
    [SerializeField] SimpleMovement playerController;
    [SerializeField] GameObject spiritCircle;
    [SerializeField] AudioFade BG;

    [SerializeField] List<ChildBehaviour> children;
    [SerializeField] List<SpiritBehaviour> spirits;

    private static int childrenSpawned = 0;
    private static int spiritsSpawned = 0;

    Timer childNotFound;

    private static GameManager _instance;
    private static GameManager instance
    {
        get
        {
            if (_instance != null) return _instance;
            else return FindObjectOfType<GameManager>();
        }
    }

    private void Awake()
    {
        totalChildren = _totalChildren;
        hud.UpdateUI();
    }

    public static int childrenFound { get; private set; }
    public static int childrenLost { get; private set; }
    public static int totalChildren { get; private set; }

    public static void PlayerChildFound(ChildBehaviour childBehaviour)
    {
        childrenFound++;
        instance.hud.UpdateUI();
    }

    public void DialogueChildFind()
    {
        childrenFound++;
        hud.UpdateUI();
        hud.PlayUISound();
        if(childrenFound < totalChildren)
        {
            SpawnChildren(1);
            SpawnSpirits(1);

            if(childNotFound != null) childNotFound.Cancel();
            childNotFound = Timer.Register(60f, OnChildNotFound);
        }
        else
        {
            spiritCircle.transform.position = player.transform.position;
            spiritCircle.SetActive(true);
            SpiritBehaviour[] spirits = spiritCircle.GetComponentsInChildren<SpiritBehaviour>();
            foreach(SpiritBehaviour spiritBehaviour in spirits)
            {
                spiritBehaviour.Descend();
            }
            
            StartCoroutine(RotateSpirits());
        }

    }

    public IEnumerator RotateSpirits()
    {
        while(true)
        {
            foreach(SpiritBehaviour spirit in spiritCircle.GetComponentsInChildren<SpiritBehaviour>())
            {
                Vector3 distance = spirit.transform.position - player.transform.position;
                float angleToRotate = 80 * Time.deltaTime;
                var rotation = Quaternion.Euler(0, angleToRotate, 0);
                Vector3 targetPosition = rotation * distance + player.transform.position;
                spirit.ChasePosition(targetPosition);
                spirit.FollowClosely();
            }
            yield return null;
        }
    }

    public void OnChildNotFound()
    {
        SpawnSpirits(1);
    }

    public static void EnemyChildFound(ChildBehaviour childBehaviour)
    {
        childrenLost++;
        instance.hud.UpdateUI();
    }

    public static List<ChildBehaviour> Children { get => instance.children; }

    public static List<SpiritBehaviour> Spirits { get => instance.spirits; }

    public static TextBox textBox { get => instance._textBox; }

    public static void SetPlayerMovement(bool state)
    {
        instance.playerController.movementEnabled = state;
    }

    public static SimpleMovement player { get => instance.playerController; }

    public void SpawnChildren (int count)
    {
        int i;
        for(i =0; i < count; i++)
        {
            if (childrenSpawned + i >= Children.Count) break;

            Children[childrenSpawned + i].gameObject.SetActive(true);
        }
        childrenSpawned += i;
    }

    public static void SpawnChildrenStatic( int count)
    {
        instance.SpawnChildren(count);
    }

    public void SpawnSpirits(int count)
    {
        int i;
        for (i = 0; i < count; i++)
        {
            if (spiritsSpawned + i >= Spirits.Count) break;

            Spirits[spiritsSpawned + i].gameObject.SetActive(true);
        }
        spiritsSpawned += i;
    }
}
