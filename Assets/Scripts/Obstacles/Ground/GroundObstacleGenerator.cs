using playerstate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundObstacleGenerator : MonoBehaviour
{
    [Header("Player Info")]
    public int playerHeight;
    public StateBase<GroundObstacleGenerator>[] stateList;
    public STATE curState;

    [Header("Gen Properties")]
    [SerializeField] public Transform[] spawnPoints;
    [SerializeField] public int spawnIndex;
    public Queue<int> fourMoves;
    [SerializeField] private float genInterval; 
    //Eachtime Initialization takes place, it depends on player's current location 

    private void Awake()
    {
        genInterval = 3f; 
        stateList = new StateBase<GroundObstacleGenerator>[(int)STATE.Size];
        stateList[(int)STATE.Start] = new JumpState(this);
        stateList[(int)STATE.Jump] = new JumpState(this);
        stateList[(int)STATE.NoJump] = new NoJumpState(this);
        fourMoves = new Queue<int>();
        //spawnPoints = new Transform[4]; 
        playerHeight = 0;
    }

    private void Start()
    {
        spawnIndex = 0;
        curState = STATE.Start;
        FourActions();
        mapGenerate = StartCoroutine(mapGen());
    }

    private void Update()
    {
        //constantly generate next blocks, but stop when its all made, resume making when trigger released
        //GenerateObs(); 
    }
    private void GenerateObs()
    {
        for (int i = 0; i < 4; i++)
        {
            spawnIndex = i;
            int nextstate = fourMoves.Dequeue();
            switch (nextstate)
            {
                case 0:
                    curState = STATE.Jump;
                    break;
                case 1:
                    curState = STATE.NoJump;
                    break;
            }
            stateList[(int)curState].Update(i, nextstate);
            stateList[(int)curState].Exit();
            if (i == 3)
            {
                FourActions();
            }
        }
    }

    private Coroutine mapGenerate;
    IEnumerator mapGen()
    {
        while (true)
        {
            //FourActions(); 
            GenerateObs();
            yield return new WaitForSeconds(genInterval);
        }
    }

    public void FourActions()
    {
        Queue<int> four = new Queue<int>();
        for (int i = 0; i < 4; i++)
        {
            int randomval = Random.Range(0, 2);
            four.Enqueue(randomval);
        }
        foreach (int i in four)
        {
            Debug.Log(four.Count);
        }
        fourMoves = four;
    }
}

namespace playerstate
{
    public enum _JumpUp // 2개의 가능성 값들 
    {
        jump_up_1,
        jump_up_2,
    }

    public enum _JumpFlat // 7 개의 가능성 값들 
    {
        spike_flat_1,
        spike_flat_2,
        spike_flat_3,
        flat_blocks_spike_1,
        flat_blocks_spike_2,
        flat_blocks_spike_3,
        flat_jump_lava,
    }
    public enum _JumpDown
    {
        jump_down // only 1
    }

    public enum NoJump // 0: up, 1: flat, 2: down 값이다. 
    {

        flat_blocks, // up
        empty, // flat 
        fall_down // down 
    }

    public enum STATE
    {
        Start, Jump, NoJump, Size
    }
    public class StartState : StateBase<GroundObstacleGenerator>
    {
        public StartState(GroundObstacleGenerator owner) : base(owner)
        {
        }

        public override void Enter()
        {
            // Initialize first block, then 
        }

        public override void Exit()
        {
        }

        public override void SetUp()
        {
            throw new System.NotImplementedException();

        }

        public override void Update(int spawnIndex, int actionVal)
        {
            GameManager.Pool.SetforRelease("empty(Clone)", owner.spawnPoints[spawnIndex]);
        }
    }
    public class JumpState : StateBase<GroundObstacleGenerator>
    {
        private string blockType;
        public JumpState(GroundObstacleGenerator owner) : base(owner)
        {
        }

        public override void Enter()
        {
            throw new System.NotImplementedException();
        }

        public override void Exit()
        {
            if (owner.spawnIndex < 3)
                return;
            //owner.FourActions();
            owner.spawnIndex = 0;
            GameManager.Pool.TriggerRelease();
        }

        public override void SetUp()
        {
            throw new System.NotImplementedException();
        }

        public override void Update(int spawnIndex, int actionVal)
        {
            int variation = Random.Range(0, 3);
            if (owner.playerHeight <= 0)
                variation = Random.Range(0, 2);
            switch (variation)
            {
                case 0:
                    JumpUp(spawnIndex);
                    break;
                case 1:
                    JumpFlat(spawnIndex);
                    break;
                case 2:
                    JumpDown(spawnIndex);
                    break;
            }
            //Exit(); 
        }

        private void JumpUp(int spawnIndex)
        {
            int nextVariation = Random.Range(0, 2);

            switch (nextVariation)
            {
                case 0:
                    blockType = _JumpUp.jump_up_1.ToString();
                    owner.playerHeight += 1;
                    break;
                case 1:
                    blockType = _JumpUp.jump_up_2.ToString();
                    owner.playerHeight += 1;
                    break;
            }
            owner.spawnPoints[spawnIndex].position = new Vector2(owner.spawnPoints[spawnIndex].position.x,
                owner.playerHeight);
            GameManager.Pool.SetforRelease(blockType, owner.spawnPoints[spawnIndex]);
        }

        private void JumpFlat(int spawnIndex)
        {
            int nextVariation = Random.Range(0, 7);
            if (owner.playerHeight <= 1)
            {
                nextVariation = Random.Range(3, 7);
            }
            switch (nextVariation)
            {
                case 0:
                    blockType = _JumpFlat.spike_flat_1.ToString();
                    break;
                case 1:
                    blockType = _JumpFlat.spike_flat_2.ToString();
                    break;
                case 2:
                    blockType = _JumpFlat.spike_flat_3.ToString();
                    break;
                case 3:
                    blockType = _JumpFlat.flat_blocks_spike_1.ToString();
                    break;
                case 4:
                    blockType = _JumpFlat.flat_blocks_spike_2.ToString();
                    break;
                case 5:
                    blockType = _JumpFlat.flat_blocks_spike_3.ToString();
                    break;
                case 6:
                    blockType = _JumpFlat.flat_jump_lava.ToString();
                    break;
            }
            GameManager.Pool.SetforRelease(blockType, owner.spawnPoints[spawnIndex]);
        }

        private void JumpDown(int spawnIndex)
        {
            blockType = _JumpDown.jump_down.ToString();
            owner.playerHeight -= 1;
            GameManager.Pool.SetforRelease(blockType, owner.spawnPoints[spawnIndex]);
        }
    }

    public class NoJumpState : StateBase<GroundObstacleGenerator>
    {
        private string blockname;
        private NoJump value;
        public NoJumpState(GroundObstacleGenerator owner) : base(owner)
        {
        }

        public override void Enter()
        {
            throw new System.NotImplementedException();
        }

        public override void Exit()
        {
            if (owner.spawnIndex < 3)
                return;
            owner.spawnIndex = 0;
            owner.FourActions();
            GameManager.Pool.TriggerRelease();
        }

        public override void SetUp()
        {
            throw new System.NotImplementedException();
        }

        public override void Update(int spawnIndex, int actionVal)
        {
            int nextVariation = Random.Range(0, 3);
            if (owner.playerHeight <= 0)
                nextVariation = Random.Range(0, 2);
            if (spawnIndex <= 1)
            {
                nextVariation = 0;
            }
            switch (nextVariation)
            {
                case 0:
                    value = NoJump.flat_blocks;
                    break;
                case 1:
                    value = NoJump.empty;
                    break;
                case 2:
                    value = NoJump.fall_down;
                    owner.playerHeight -= 1;
                    break;
            }
            owner.spawnPoints[spawnIndex].position = new Vector2(owner.spawnPoints[spawnIndex].position.x,
                owner.playerHeight);
            blockname = value.ToString();
            GameManager.Pool.SetforRelease(blockname, owner.spawnPoints[spawnIndex]);
        }
    }
}