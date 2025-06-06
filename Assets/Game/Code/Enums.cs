namespace Assets.Game.Code
{
    public enum Loot
    {
        Coin,
        Key
    }

    public enum Direction
    {
        Left = -1,
        Right = 1
    }

    public enum BehaviourAI
    {
        Patrolling,
        Attack,
        Die
    }

    public enum BehaviourBossAI
    {
        Patrolling,
        Waiting,
        Attack,
        Die
    }

    public enum DefeatedObject
    {
        None,
        Enemy,
        Prop
    }

    public enum HeartType
    {
        Single,
        Multiple
    }

    public enum ControllerHudButton
    {
        Left,
        Right,
        Jump,
        Attack
    }
}