namespace GenericLooterShooterRPG.Models
{
    public class ResourceHarvesterModel
    {
        public int Speed;
        public float Range;
        public float Cooldown;
        public int Damage;

        public ResourceHarvesterModel(int speed, float range, float cooldown, int damage)
        {
            Speed = speed;
            Range = range;
            Cooldown = cooldown;
            Damage = damage;
        }

        public void Upgrade()
        {
            Speed += 1;
            Range += 10f;
            Cooldown -= 25f;
            Damage += 1;

        }

        public bool CanUpgrade()
        {
            return Speed < 10;
        }
    }
}
