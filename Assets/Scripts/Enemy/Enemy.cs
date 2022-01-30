
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}

public class Enemy : MonoBehaviour
{
    public float Enemy_name;
    public float id;


    protected private NavMeshAgent agent;
    protected private EnemyState enemy_State;
 

    [SerializeField] private bool hasMultipleAttack;

    public float walk_Speed = 0.5f;
    public float run_Speed = 4.5f;
    public float chase_Distance = 7f;
    private float current_Chase_Distance;
    public float attack_Distance = 1.8f;
    public float chase_After_Attack_Distance = 2f;
    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;
    public float patrol_For_This_Time = 15f;
    private float patrol_Time;

    public float damage;

    public float wait_Before_Attack = 2f;
    private float attack_Timer;

    public GameObject attackPoint;
    private Transform target;
    Animator enemy_Anim;
    // Start is called before the first frame update
     protected virtual void Awake()
    {
        enemy_Anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
     void Start()
    {
        enemy_State = EnemyState.PATROL;
        patrol_Time = patrol_For_This_Time;

        //first hit witout CD
        attack_Timer = wait_Before_Attack;

        //Memorize the initial value
        current_Chase_Distance = chase_Distance;

    }
    // Update is called once per frame
    void  Update()
    {
        //Check attack, patrol, run
        Check_States();
    }

     void Check_States()
    {
        if (enemy_State == EnemyState.PATROL)
        {
            Patrol();
        }
        if (enemy_State == EnemyState.CHASE)
        {
            Chase();
        }
        if (enemy_State == EnemyState.ATTACK)
        {
            Attack();
        }
    }
    protected virtual void Patrol()
    {
        agent.isStopped = false;
        agent.speed = walk_Speed;

        patrol_Time += Time.deltaTime;

        if (patrol_Time > patrol_For_This_Time)
        {
            //Go to new place
            NewPatrolPlace();
            patrol_Time = 0;

        }
        if (agent.velocity.sqrMagnitude > 0) enemy_Anim.SetBool("walk",true);
        else enemy_Anim.SetBool("walk", false);


        //Notice player
        if (Vector3.Distance(transform.position,target.position)<= chase_Distance)
        {
            enemy_State = EnemyState.CHASE;

            //Notice sound
        }



    }
    protected virtual void Chase()
    {
        
        agent.isStopped = false;
        agent.speed = run_Speed;

        //move to player
        agent.SetDestination(target.position);

        //walk if moving
        if (agent.velocity.sqrMagnitude > 0) ChaseAnim();
        else StopAnims();


        //Attack Distance
        if (Vector3.Distance(transform.position, target.position) <= attack_Distance)
        {
            StopAnims();
            enemy_State = EnemyState.ATTACK;
            attack_Timer = wait_Before_Attack;
            //reset chase distance
            if (chase_Distance != current_Chase_Distance) chase_Distance = current_Chase_Distance;
        
        }
        //if Gone out of range
        else if(Vector3.Distance(transform.position, target.position)>= 2 * chase_Distance)
        {
            StopAnims();//if runs stop
            enemy_State = EnemyState.PATROL;
            //reset timer
            patrol_Time = patrol_For_This_Time;
            if (chase_Distance != current_Chase_Distance) chase_Distance = current_Chase_Distance;
        }

    }

    void Attack()
    {
        agent.velocity = Vector3.zero;// stop for hit
        agent.isStopped = true;
        attack_Timer += Time.deltaTime;
        //look at player
        LookToPlayer();
        if (attack_Timer > wait_Before_Attack)
        {
            Hit();
            attack_Timer = 0;

            //play Sound

        
        }
        if (Vector3.Distance(transform.position, target.position) >= attack_Distance)
        {
            enemy_State = EnemyState.CHASE;
        }
    }

   //set new Random patrol destination

    void NewPatrolPlace()
    {
        float ptr_Radius = Random.Range(patrol_Radius_Min, patrol_Radius_Max);

        Vector3 random_Dir= Random.insideUnitSphere * ptr_Radius;

        random_Dir += transform.position;

        NavMeshHit navhit;
        NavMesh.SamplePosition(random_Dir, out navhit, ptr_Radius, -1);

        agent.SetDestination(navhit.position);
    }
    void LookToPlayer()
    {
       
            Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation,1);
      
    }
    protected virtual void StopAnims()
    {
        enemy_Anim.SetBool("walk", false);
    }
    protected virtual void ChaseAnim()
    {
        enemy_Anim.SetBool("walk", true);
    }


    protected virtual void Hit()
    {

        enemy_Anim.SetTrigger("Attack");


    }
    //if hit with player
   
    void Turn_On_AttackPoint()
    {
        attackPoint.SetActive(true);
    }
    
    void Turn_Off_AttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }
}
