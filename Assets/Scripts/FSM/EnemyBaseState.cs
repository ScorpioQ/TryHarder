public abstract class EnemyBaseState
{
    public abstract void EnterState(CombatUnit enemy);
    public abstract void OnUpdate(CombatUnit enemy);
}
