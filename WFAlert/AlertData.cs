using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFAlert
{
    class AlertData
    {
        private string id;
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private string credits;
        public string Credits
        {
            get { return credits; }
            set { credits = value; }
        }

        private string reward;
        public string Reward
        {
            get { return reward; }
            set { reward = value; }
        }

        private int timeleft;
        public int Timeleft
        {
            get { return timeleft; }
            set
            {
                try
                {
                    timeleft = Convert.ToInt32(value);
                }
                catch
                {
                    timeleft = -1;
                }
            }
        }

        public AlertData()
        {
            id = "";
            credits = "";
            reward = "";
            timeleft = 0;
        }

        public void reduceTime()
        {
            --timeleft;
        }

        public string checkID()
        {
            return id;
        }
    }
}
