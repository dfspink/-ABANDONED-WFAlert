using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Media;
using System.Windows.Forms;
using LinqToTwitter;

using System.Diagnostics;   // debug

namespace WFAlert
{
    public partial class MainGUI : Form
    {
        Timer timer_data = new Timer();
        Timer timer_time = new Timer();

        List<AlertData> DataList = new List<AlertData>(0);

        System.Linq.IQueryable<Status> statusTweets;
        string sinceid="";
        const int count = 2;
        const string twittername = "WarframeAlerts";

        public MainGUI()
        {
            InitializeComponent();
            InitData();
            StartTimer();
        }

        private void InitData()
        {
            if (PullData())
            {
                ParseData();
                UpdateRTB();
            }
        }

        /// <summary>
        /// Starts the timers for pulling tweets and updating timers
        /// </summary>
        private void StartTimer()
        {
            timer_data.Tick += new EventHandler(CheckTweets);
            timer_time.Tick += new EventHandler(UpdateTime);

            timer_data.Interval = (1000) * (120);   // Timer will tick every 2 minutes
            timer_data.Enabled = true;              // Enable the timer
            timer_data.Start();                     // Start the timer

            timer_time.Interval = (1000) * (60);    // Timer will tick every minute
            timer_time.Enabled = true;              // Enable the timer
            timer_time.Start();                     // Start the timer
        }

        /// <summary>
        /// Updates rich text boxes.
        /// </summary>
        private void UpdateRTB()
        {

            if (DataList.Count == 0)
            {
                rtb_reward1.Text = "";
                rtb_cred1.Text = "";
                rtb_time1.Text = "";

                rtb_reward2.Text = "";
                rtb_cred2.Text = "";
                rtb_time2.Text = "";

                rtb_reward3.Text = "";
                rtb_cred3.Text = "";
                rtb_time3.Text = "";
            }
            else if (DataList.Count == 1)
            {
                rtb_reward1.Text = DataList.ElementAt(0).Reward;
                rtb_cred1.Text = DataList.ElementAt(0).Credits;
                rtb_time1.Text = DataList.ElementAt(0).Timeleft.ToString();

                rtb_reward2.Text = "";
                rtb_cred2.Text = "";
                rtb_time2.Text = "";

                rtb_reward3.Text = "";
                rtb_cred3.Text = "";
                rtb_time3.Text = "";
            }
            else if (DataList.Count == 2)
            {
                rtb_reward1.Text = DataList.ElementAt(0).Reward;
                rtb_cred1.Text = DataList.ElementAt(0).Credits;
                rtb_time1.Text = DataList.ElementAt(0).Timeleft.ToString();

                rtb_reward2.Text = DataList.ElementAt(1).Reward;
                rtb_cred2.Text = DataList.ElementAt(1).Credits;
                rtb_time2.Text = DataList.ElementAt(1).Timeleft.ToString();

                rtb_reward3.Text = "";
                rtb_cred3.Text = "";
                rtb_time3.Text = "";
            }
            else
            {
                rtb_reward1.Text = DataList.ElementAt(0).Reward;
                rtb_cred1.Text = DataList.ElementAt(0).Credits;
                rtb_time1.Text = DataList.ElementAt(0).Timeleft.ToString();

                rtb_reward2.Text = DataList.ElementAt(1).Reward;
                rtb_cred2.Text = DataList.ElementAt(1).Credits;
                rtb_time2.Text = DataList.ElementAt(1).Timeleft.ToString();

                rtb_reward3.Text = DataList.ElementAt(2).Reward;
                rtb_cred3.Text = DataList.ElementAt(2).Credits;
                rtb_time3.Text = DataList.ElementAt(2).Timeleft.ToString();
            }
        }

        /// <summary>
        /// Removes any entries that have timed out and updates any that are still alive. After doing that it updates the GUI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateTime(object sender, EventArgs e)     // Could use refactoring, its ugly
        {
            int i = 0;
            int size = DataList.Count();
            do
            {
                for (i = 0; i < size; ++i)
                {
                    if (DataList.ElementAt(i).Timeleft == 0)
                    {
                        DataList.RemoveAt(i);
                        --size;
                        --i;
                    }
                    else
                        --DataList.ElementAt(i).Timeleft;
                }
            } while (i < size);
            UpdateRTB();
        }

        /// <summary>
        /// Runs functions to get data from twitter, parse it, check for keywords, and add it to the DataList.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckTweets(object sender, EventArgs e)
        {
            if (PullData())
            {
                ParseData();
                UpdateRTB();
            }
        }

        private bool CheckLimit()
        {
            return (WFAlert.TM.GetCtx().RateLimitRemaining >= 2);
        }

        /// <summary>
        /// Gets data from twitter.
        /// </summary>
        private bool PullData()
        {
            statusTweets =
                from tweet in WFAlert.TM.GetCtx().Status
                where tweet.Type == StatusType.User
                        && tweet.ScreenName == twittername
                        && tweet.ExcludeReplies == true
                        && tweet.Count == count
                select tweet;

            if (statusTweets.Count() != 0)
                sinceid = statusTweets.Max(status => status.StatusID);

            if (CheckLimit())
                return true;
            return false;
        }

        /// <summary>
        /// Goes through all tweets recieved and parses the data. If the data contains a keyword it adds it to the DataList.
        /// </summary>
        private void ParseData()
        {
            int[] location;
            AlertData DataObj;

            
            for (int k=0; k < statusTweets.Count(); ++k)
            {
                if (NotInList(statusTweets.ElementAt(k).StatusID))
                {
                    location = new int[3];
                    DataObj = new AlertData();
                    int i = 0, j = 0;

                    // get locations of dashes
                    while ((i = statusTweets.ElementAt(k).Text.IndexOf('-', i)) != -1)
                    {
                        location[j] = i;
                        ++j;
                        ++i;
                    }

                    // the +/- numbers in the substring function are to get rid of whitespace, dashes, and/or "m"/"cr"
                    DataObj.Timeleft = Convert.ToInt32(statusTweets.ElementAt(k).Text.Substring(location[0] + 2, location[1] - location[0] - 4));
                    DateTime t1 = DateTime.Now;
                    DateTime t2 = statusTweets.ElementAt(k).CreatedAt.ToLocalTime().AddMinutes(DataObj.Timeleft);

                    if (DateTime.Compare(t1, t2) < 0)
                    {
                        TimeSpan result = t2 - t1;
                        DataObj.Timeleft = result.Minutes;
                    }
                    else
                        DataObj.Timeleft = 0;

                    if (location[2] != 0)
                    {
                        DataObj.Credits = statusTweets.ElementAt(k).Text.Substring(location[1] + 2, location[2] - location[1] - 5);
                        DataObj.Reward = statusTweets.ElementAt(k).Text.Substring(location[2] + 2);
                    }
                    else
                    {
                        DataObj.Credits = statusTweets.ElementAt(k).Text.Substring(location[1] + 2, statusTweets.ElementAt(k).Text.Length - location[1] - 4);
                        DataObj.Reward = "";
                    }
                    DataObj.ID = statusTweets.ElementAt(k).StatusID;

                    if ((DataObj.Timeleft != 0) && ((DataObj.Reward != "") || (Convert.ToInt32(DataObj.Credits) >= 400)))   // If still available and has a bp/artifact/lots of credits add it
                    {
                        DataList.Add(DataObj);
                        PlaySound();
                    }
                }
            }
        }

        private bool NotInList(string input)
        {
            foreach (AlertData ad in DataList)
                if (ad.ID == input)
                    return false;
            return true;
        }

        private void PlaySound()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"C:\alertsound.wav");
            simpleSound.Play();
        }
    }
}