namespace DefaultNamespace.Tricks
{
    public interface ISpecialTrick : ITrick
    {
        void SetSuccess(bool success);
        float GetSpecialTrickMultiplier();
    }
}