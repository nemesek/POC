namespace Strategy.Domain.StrategyOCP
{
    public class FoobarService
    {
        public virtual string GetAction(int cmsId)
        {
            return string.Format(cmsId == 1 ? "I am doing foo for {0}" : "I am doing bar for {0}", cmsId);
        }

        //public virtual string GetAction(int cmsId)
        //{
        //    return string.Format(cmsId == 1 ? "I am doing foo for {0}" : "I am doing bar for {0}", cmsId);
        //}
    }
}
