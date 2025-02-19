using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_manager : MonoBehaviour
{
    protected const int monsterNum = 10; // �� ���� ���� (���)

    private static Stack<GameObject>[] monsterDeadList; // ���� ���͸� �����ϱ� ���� ������ ��Ȱ��ȭ �� ����
    private static bool deadListInit = false;

    private static List<int[]> monsterWave = new List<int[]>(); // �� ���̺� �� ���� ���͵� ����Ʈ

    protected GameObject liveMonster, deadMonster; // �ڵ� ���� �� �ǹ̴� ������ ������ ���� �������� empty ������Ʈ
    public GameObject[] monsterPrefab = new GameObject[monsterNum]; // �� ���� ������ {����, �̶�, ó��ͽ�, ����, ... �߰�����}

    private bool callWave;

    protected private Animator animator;

    protected float houseX = 4f;
    protected private int monsterIndex;
    protected private int health;
    protected private float attackDamage;
    protected private float attackSpeed;
    protected private float attackDelay;
    protected private float moveSpeed;

    protected bool isMove = false;
    protected bool isAttack = false;

    private void Awake()
    {
        liveMonster = GameObject.Find("live_monster");
        deadMonster = GameObject.Find("dead_monster");
        
        animator = GetComponent<Animator>();
        animator.SetInteger("state", 1);

        if (!deadListInit)
        {
            deadListInit = true;
            monsterDeadList = new Stack<GameObject>[monsterNum];

            for (int i = 0; i < monsterDeadList.Length; i++)
            {
                monsterDeadList[i] = new Stack<GameObject>();
            }
            //monsterWave.Add(new int[] {�̹� ���̺� �� ���� ����, ����, �̶�, ó��ͽ�, ����, ... }):
            monsterWave.Add(new int[] { 15, 1, 0, 14, 0, 0, 0, 0, 0, 0, 0 }); // wave 1
            monsterWave.Add(new int[] { 15, 2, 13, 0, 0, 0, 0, 0, 0, 0, 0 }); // wave 2
        }
    }

    public void damaged(int n) 
    {
        Debug.Log(this.gameObject.name + " damaged : " + n + " now HP : " + health);
        health -= n;
    }

    protected IEnumerator attack()
    {
        animator.SetInteger("state", 2);
        yield return new WaitForSeconds(attackDelay);
        house_manager.attackHouse(attackDamage);
        yield return new WaitForSeconds(attackSpeed);
        startAttack();
        
    }

    protected virtual IEnumerator move()
    {
        if (isMove) yield break;

        isMove = true;
        while (this.transform.position.x > houseX)
        {
            this.transform.Translate(Vector3.left * moveSpeed * Time.fixedDeltaTime * 0.2f);
            yield return new WaitForSeconds(0.1f);
        }

        startAttack();
        isMove = false;
    }

    protected void startAttack() => StartCoroutine(attack());

    protected void startMove() => StartCoroutine(move());

    protected int returnMonsterIndex() => monsterIndex;

    public int returnHealth() => health;

    protected void commonUpdate() 
    {
        if (!attack_manager.selectTarget) 
        {
            if (attack_manager.targetMonster != this.gameObject) 
            {
                if (attack_manager.targetMonster.transform.position.x > this.gameObject.transform.position.x) 
                {
                    attack_manager.findTarget();
                }
            }
        }
        if (health <= 0 || !time_manager.returnNight)
        {
            dead(returnMonsterIndex());
        }
    }

    protected void dead(int n)
    {
        this.gameObject.SetActive(false);
        attack_manager.liveMonsterList.Remove(this.gameObject);
        attack_manager.findTarget();
        animator.SetInteger("state", 1);
        StopCoroutine(move());
        StopCoroutine(attack());
        isMove = false;
        isAttack = false;
        monsterDeadList[n].Push(this.gameObject);
        this.gameObject.transform.SetParent(deadMonster.transform);
    }

    private void createMonster(int n)
    {
        GameObject newMonster;
        if (monsterDeadList[n].Count == 0)
        {
            newMonster = Instantiate(monsterPrefab[n]);
        }
        else
        {
            newMonster = monsterDeadList[n].Pop();
        }
        newMonster.transform.position = new Vector3(15f, -1f, -4f);
        newMonster.transform.SetParent(liveMonster.transform);
        attack_manager.liveMonsterList.Add(newMonster);
        attack_manager.findTarget();
        newMonster.SetActive(true);
    }

    void Update()
    {
        if (time_manager.returnNight)
        {
            if (callWave)
            {
                callWave = false;
                StartCoroutine(waveStart(time_manager.returnDay));
            }
        }
        else
        {
            callWave = true;
            StopCoroutine(waveStart(time_manager.returnDay - 1));
        }
    }

    IEnumerator waveStart(int n)
    {
        float summonCycle = time_manager.returnDayCycle / monsterWave[n][0];

        for (int i = 1; i <= monsterNum; i++)
        {
            for (int j = 0; j < monsterWave[n][i]; j++)
            {
                createMonster(i - 1);
                yield return new WaitForSeconds(summonCycle);
            }
        }
    }
}
