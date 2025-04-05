using UnityEngine;

public class CoinsQuest : MonoBehaviour
{
    private QuestManager QM;
    QuestObject quest;
    private Collector collector;

    public int questNum;
   
    void Start()
    {
        QM = FindFirstObjectByType<QuestManager>();
        collector = FindFirstObjectByType<Collector>();
        quest = QM.quests[questNum];
    }

    void Update()
    {
        CollectedCoins();
    }

    void CollectedCoins(){
        if(collector.coins >= 11 && quest.isAccepted && !QM.questCompleted[questNum]){
            collector.coins = collector.coins - 11;
            quest.EndQuest();
        }
    }
}
