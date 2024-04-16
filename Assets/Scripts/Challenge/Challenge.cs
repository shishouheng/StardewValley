namespace ProjectIndieFarm
{
    public abstract class Challenge
    {
        public enum States
        {
            NotStart,Started,Finished
        }

        public States state = States.NotStart;
        public string name;
        /// <summary>
        /// 挑战开始时的逻辑
        /// </summary>
        public abstract void OnStart();
        public abstract bool CheckFinish();
        /// <summary>
        /// 挑战结束时的逻辑
        /// </summary>
        public abstract void OnFinish();
    }
}