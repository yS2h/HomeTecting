using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_manager : MonoBehaviour
{
    protected const int monsterNum = 10; // 총 몬스터 개수 (상수)

    private static Stack<GameObject>[] monsterLiveList = new Stack<GameObject>[monsterNum]; // 현재 살아있는 몬스터들
    private static Stack<GameObject>[] monsterDeadList = new Stack<GameObject>[monsterNum]; // 죽은 몬스터를 재사용하기 위해 죽으면 비활성화 후 저장
    private static List<int[]> monsterWave = new List<int[]>(); // 각 웨이브 별 나올 몬스터들 리스트

    protected GameObject liveMonster, deadMonster; // 코드 진행 상 의미는 없지만 구분을 위해 만들어놓은 empty 오브젝트
    public GameObject[] monsterPrefab = new GameObject[monsterNum]; // 각 몬스터 프리팹 {유령, 미라, 처녀귀신, 강시, ... 추가예정}

    private bool callWave;

    protected private int health;
    protected private float attackDamage;
    protected private float attackSpeed;
    protected private float moveSpeed;

    private void Awake()
    {
        liveMonster = GameObject.Find("live_monster");
        deadMonster = GameObject.Find("dead_monster");
        
        for (int i = 0; i < monsterDeadList.Length; i++)
        {
            monsterDeadList[i] = new Stack<GameObject>();
        }
        //monsterWave.Add(new int[] {이번 웨이브 총 몬스터 숫자, 유령, 미라, 처녀귀신, 강시, ... }):
        monsterWave.Add(new int[] { 10, 7, 3, 0, 0, 0, 0, 0, 0, 0, 0 }); // wave 1
        monsterWave.Add(new int[] { 15, 2, 13, 0, 0, 0, 0, 0, 0, 0, 0 }); // wave 2
    }

    protected void dead(int n)
    {
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
        newMonster.transform.SetParent(liveMonster.transform);
        newMonster.SetActive(true);
    }

    private void Start()
    {

    }

    void Update()
    {
        if (time_manager.instance.night)
        {
            if (callWave)
            {
                callWave = false;
                StartCoroutine(waveStart(time_manager.instance.day));
            }
        }
        else
        {
            callWave = true;
            StopCoroutine(waveStart(time_manager.instance.day - 1));
        }
    }

    IEnumerator waveStart(int n)
    {
        float summonCycle = time_manager.instance.dayCycle / monsterWave[n][0];

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
