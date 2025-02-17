namespace Assets.Game.Code.Interfaces
{
    public interface IDamageble
    {
        void TakeDamage(int damage);
        void Die();
    }
}