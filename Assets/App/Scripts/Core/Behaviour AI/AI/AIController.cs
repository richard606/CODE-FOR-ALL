using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using AI.BT;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AIController : LivingEntity
{
    [Tooltip("debug the status of the requirements in real time with the OnGUI method.")]
    public bool debugNeeds;

    public Transform playerRoot;

    private Behavior _root;    

    private PlayerHUD _playerHUD;
    public PlayerHUD PlayerHUD 
    { 
        get => _playerHUD; 
        set => _playerHUD = value;
    }

    private Animator _animator;
    public Animator Animator
    {
        get => _animator; 
        set => _animator = value;
    }
    
    [SerializeField]
    [Tooltip("The player's default animator controller, since each object has an animator controller that overrides it.")]
    private RuntimeAnimatorController _animatorRuntimeController;
    public RuntimeAnimatorController AnimatorRuntimeController 
    { 
        get => _animatorRuntimeController; 
        set => _animatorRuntimeController = value; 
    }
       
    private NavMeshAgent _agent;
    public NavMeshAgent Agent
    {
        get => _agent;        
    }

    private bool _isWorkingOnNeed;
    public bool IsWorkingOnNeed 
    { 
        get => _isWorkingOnNeed; 
        set => _isWorkingOnNeed = value; 
    }    

    private List<NeedsData> _needsToSupplies = new List<NeedsData>();
    public List<NeedsData> NeedsToSupplies 
    { 
        get => _needsToSupplies; 
        set => _needsToSupplies = value;
    }
    

    [SerializeField]
    private List<Need> _needs = new List<Need>();
    public List<Need> Needs 
    { 
        get => _needs; 
        set => _needs = value;
    }

   
    #if UNITY_EDITOR
    private GUIStyle _green;
    private GUIStyle _yellow;
    private GUIStyle _red;
    private float _x = 20f;
    private float _y = 20f;
    
    void OnEnable()
    {
        
       
        _green = new GUIStyle(EditorStyles.label);
        _green.normal.textColor = Color.green;
        _green.fontSize = 20;
        _yellow = new GUIStyle(EditorStyles.label);
        _yellow.normal.textColor = Color.yellow;
        _yellow.fontSize = 20;

        _red = new GUIStyle(EditorStyles.label);
        _red.normal.textColor = Color.red;
        _red.fontSize = 20;
       
       
    }

    private void OnGUI()
    {
        if (!debugNeeds) return;

        for (int i = 0; i < _needs.Count; i++)
        {
            NeedsData needB = _needs[i].NeedsData;
            string needText = $"{needB.name} need (Priority '{needB.needPriority}') : {needB.CurrentNeedLevel.ToString()}";

            if (needB.CurrentNeedLevel >= needB.percentageOnMediumNeedLevel)
                GUI.Label(new Rect(_x, i * _y, 1000f, 20f), "GOOD " + needText, _green);
            else if (needB.CurrentNeedLevel >= needB.percentageOnLowNeedLevel)
                GUI.Label(new Rect(_x, i * _y, 1000f, 20f), "CAUTION " + needText, _yellow);
            else if (needB.CurrentNeedLevel < needB.percentageOnLowNeedLevel)
                GUI.Label(new Rect(_x, i * _y, 1000f, 20f), "DANGER " + needText, _red);
        }

    }
    #endif

    private void Start ()
    {
        InitializeVars();
	}

    private void InitializeVars()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = playerRoot.GetComponent<Animator>();        
        _playerHUD = playerRoot.Find("HUD").GetComponent<PlayerHUD>();




        _root = new Parallel(Parallel.Policy.RequireAll, Parallel.Policy.RequireAll,
                new ANeeds(this),
                new Repeat(80,
                new Sequence(new CIsNeedSupply(this), new AMoveTo(this), new ASupplyNeed(this)))); ;
        //root = new ActiveSelector(
        //            new Sequence(
        //                new CImInDanger(this),
        //                new AEscape(this),
        //                new APray(this)
        //                ),
        //            new Sequence(
        //                new CIsTargetVisible(this),
        //                new Monitor(
        //                    new CIsTargetVisible(this),
        //                    new Parallel(Parallel.Policy.RequireAll, Parallel.Policy.RequireAll,
        //                        new UntilSuccess(0,
        //                            new Sequence(
        //                                new CIsInRange(this),
        //                                new AWait(0.5f),
        //                                new AFire(this))),
        //                        new UntilSuccess(0,
        //                            new Sequence(
        //                                new Inverter(new CIsInRange(this)),
        //                                new AFollowTarget(this)))))),
        //            new APatrol(this));
    }

    private void Update ()
    {    
        _root.Tick();
	}
    
   

    public override void ReceiveDamage(int damageAmount, RaycastHit hit)
    {
        base.ReceiveDamage(damageAmount, hit);
    }
   
    public override void Die()
    {
        Destroy(gameObject);
    }


    private void OnApplicationQuit()
    {
        foreach (Need need in _needs)
        {
            need.NeedsData.isNeedAlarm = false;
        }
    }




}
