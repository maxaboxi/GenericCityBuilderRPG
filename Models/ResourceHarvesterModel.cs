namespace GenericLooterShooterRPG.Models
{
    public class ResourceHarvesterModel
    {
        public int Speed;
        public float Range;
        public float Cooldown;

        public ResourceHarvesterModel(int speed, float range, float cooldown)
        {
            Speed = speed;
            Range = range;
            Cooldown = cooldown;
        }
    }
}
