using System;

namespace routing.slip
{
    internal class RoutingSlip
    {
        private readonly string _name;
        private List<Func<Task>> _activities;
        private List<(Func<Task>, Func<Task>)> _activitiesCompensate;
        private List<(Func<Task>, Func<Task>)> _executedActivities;

        public RoutingSlip(string name)
        {
            _name = name;

            _activities = [];
            _activitiesCompensate = [];
            _executedActivities = [];
        }

        public void AddActivity(Func<Task> action)
        {
            _activities.Add(action);
        }
        public void AddActivity((Func<Task>, Func<Task>) action)
        {
            _activitiesCompensate.Add(action);
        }
        public async Task Execute()
        {
            try
            {
                foreach (var activity in _activities)
                {
                    await activity();
                }

                foreach (var activity in _activitiesCompensate)
                {
                    _executedActivities.Add(activity);

                    await activity.Item1();
                }
            }
            catch (Exception ex)
            {
                await Compensate(); // Realiza o rollback
            }
        }
        private async Task Compensate()
        {
            foreach (var activity in _executedActivities)
            {
                await activity.Item2();
            }
        }
    }
}
