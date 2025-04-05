using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    private QuestManager QM;
    
    public int questNum;
    public bool endQuest;

    void Start()
    {
        QM = FindFirstObjectByType<QuestManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //qhen the player arrives at the designated zone it checks if the quest 
        //was accepted and not done already it ends it
        if(collision.CompareTag("Player")){
            QuestObject quest = QM.quests[questNum];
            if(endQuest && quest.isAccepted && !QM.questCompleted[questNum]){
                quest.EndQuest();
            }
        }
    }
}
