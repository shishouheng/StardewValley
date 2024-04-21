namespace ProjectIndieFarm
{
    public abstract class Challenge
    {
        public enum States
        {
            NotStart,
            Started,
            Finished
        }

        public States state = States.NotStart;
        public abstract string Name { get; }

        // 挑战开始时的逻辑
        public abstract void OnStart();

        //检查挑战是否完成
        public abstract bool CheckFinish();

        // 挑战结束时的逻辑
        public abstract void OnFinish();
    }
}