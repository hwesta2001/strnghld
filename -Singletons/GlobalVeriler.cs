

public static class GlobalVeriler // harflerin hepsi büyük olsun ve alt cizgi ile ayrılsın bu static veriler
{
    public static CameraState CAMERA_STATE;
    public static GameGlobalStates GAMESTATE = GameGlobalStates.WaitingForInit;
}

public enum GameGlobalStates { WaitingForInit, Start, MiddGame, LateGame, EndGame }

// tüm kullanılan enumlar burada olacak
public enum GloabalResource { Gold, Wood, Stone, Iron, Food, Weath, Flour, MaxPopulation, Population, Happines, LastEnum }

public enum UnitType { Archer, Bina, Swordsman, Pikeman, Player }

public enum IsciGorevi { Idle, Oduncu, TasUstasi, Wagoncu, Madenci, WheatFarmer, AppleFarmer, MillWorker, Bakery, Asker, NonWorker }

public enum DepoCinsi { Resource, Food, Barn, Armory }


public enum Command { Move, Stop, AttackMove, DirectAttack, StandAndAttack, SearchAndGoToAttack }

public enum AttackPhase { MovingToDestionation, Attacking, Chasing, StandAndAttack }

public enum RangeType { Attack, Aggro, GiveUp }

public enum CameraState { Fps, Top1, nullState, initState }

//public enum AnimState { Idle, Walk, Attack1, Attack2, Run, Victory, Die } //Die hep ensonda olacak burdan enum uzunlugunu kullanıyoruz

public enum IsciAnimState // GlobalVerilere tası bunu
{
    idle,
    idleNoTool,
    walkKazma,
    walk,
    run,
    attack,
    deathA,
    deathB,
    idleBag,
    walkBag,
    runBag,
    attackBag,
    idleWood,
    walkWood,
    runWood,
    attackWood,
    digPlant,
    sitting,
    toSit,
    pickFruit,
    fromSit,
    wagonWalk
}
